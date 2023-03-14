// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Geoinformatika
{
    public class GridLayer : AbstractLayer
    {
        private readonly GridGeometry gridGeometry;
        public GridGeometry GridGeometry => gridGeometry;

        public override GeoRect GetBounds => new GeoRect(GridGeometry.Xmin, GridGeometry.Ymin, GridGeometry.Xmax, GridGeometry.Ymax);
        private double? matrixMinValue;
        private double? matrixMaxValue;
        /// <summary>
        /// Наиблоее часто используемая степень расстояния. Для нее проводятся оптимизации
        /// </summary>
        private const int DefaultPopularInterpolationParamsPower = 2;

        public GridLayer(GridGeometry geometry)
        {
            gridGeometry = geometry;
            matrix = new double?[(geometry?.CountY ?? 0), (geometry?.CountX ?? 0)];
            if (gridGeometry == null)
            {
                return;
            }
            const double A = 10;
            const double B = 0.1;
            for (int i = 0; i < geometry.CountY; i++)
            {
                for (int j = 0; j < geometry.CountX; j++)
                {
                    double nodeValue = A * Math.Sin(B * i) * Math.Sin(B * j);
                    SetNode(i, j, nodeValue);
                }
            }
            UpdateMinMax();
        }
        private void RefreshMatrixCoordinatesByPoints(int i, int j, List<Geopoint> points, InterpalationParams interpalationParams)
        {
            bool f = false;
            double x = GridGeometry.Xmin + j * GridGeometry.Cell;
            double y = GridGeometry.Ymin + i * GridGeometry.Cell;
            double sum1 = 0;
            double sum2 = 0;
            double rp;
            var searchRadius2 = interpalationParams.SearchRadius * interpalationParams.SearchRadius;
            for (int k = 0; k < points.Count; k++)
            {
                double r2 = (points[k].X - x) * (points[k].X - x) + (points[k].Y - y) * (points[k].Y - y);
                if (Math.Abs(r2) <= double.Epsilon)
                {
                    sum1 = points[k].Z;
                    sum2 = 1;
                    f = true;
#pragma warning disable S1227 // break statements should not be used except for switch cases
                    break;
#pragma warning restore S1227 // break statements should not be used except for switch cases
                }
                if (r2 <= searchRadius2)
                {
                    f = true;

                    rp =interpalationParams.Power == DefaultPopularInterpolationParamsPower ? r2 : Math.Pow(Math.Sqrt(r2), interpalationParams.Power);
                    sum1 += points[k].Z / rp;
                    sum2 += 1 / rp;
                }
            }
            if (f)
            {
                matrix[i, j] = sum1 / sum2;
            }
            else
            {
                matrix[i, j] = null;
            }
        }
        private void InterpolateFromPointsBySearchRadius(List<Geopoint> points, InterpalationParams interpalationParams)
        {
            for (int i = 0; i < GridGeometry.CountY; i++)
            {

                for (int j = 0; j < GridGeometry.CountX; j++)
                {
                    RefreshMatrixCoordinatesByPoints(i, j, points, interpalationParams);
                }
            }
        }
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high
        private void InterpolateFromPointsByNearestCount(IEnumerable<Geopoint> points, InterpalationParams interpalationParams)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
        {
            double sum1;
            double sum2;
            double searchRadius2;
            double rp;
            searchRadius2 = interpalationParams.SearchRadius * interpalationParams.SearchRadius;
            for (int i = 0; i < GridGeometry.CountY; i++)
            {
                for (int j = 0; j < GridGeometry.CountX; j++)
                {
                    double x = GridGeometry.Xmin + j * GridGeometry.Cell;
                    double y = GridGeometry.Ymin + i * GridGeometry.Cell;
                    sum1 = 0;
                    sum2 = 0;
                    List<Tuple<double, Geopoint>> tupleDist = points.Where(p => (p.X - x)*(p.X - x) + (p.Y - y) * (p.Y - y) <= searchRadius2)
                            .Select(p => new Tuple<double, Geopoint>((p.X - x)*(p.X - x) + (p.Y - y) * (p.Y - y), p)).ToList();
                    var sorted = tupleDist.OrderBy(t => t.Item1).ToList();
                    for (int k = 0; k < sorted.Count && k < interpalationParams.NearestCount; k++)
                    {
                        rp =interpalationParams.Power == DefaultPopularInterpolationParamsPower ? sorted[k].Item1 : Math.Pow(Math.Sqrt(sorted[k].Item1), interpalationParams.Power);
#pragma warning disable S134 // Control flow statements "if", "switch", "for", "foreach", "while", "do"  and "try" should not be nested too deeply
                        if (Math.Abs(rp) > double.Epsilon)
                        {
                            sum1 += sorted[k].Item2.Z / rp;
                            sum2 += 1 / rp;
                        }
                        else
                        {
                            sum1 = sorted[k].Item2.Z;
                            sum2 = 1;
                        }
#pragma warning restore S134 // Control flow statements "if", "switch", "for", "foreach", "while", "do"  and "try" should not be nested too deeply
                    }
                    if (sorted.Count > 0)
                    {
                        matrix[i, j] = sum1 / sum2;
                    }
                    else
                    {
                        matrix[i, j] = null;
                    }
                }

            }
        }
        /// <summary>
        /// Интерполяция точек
        /// </summary>
        /// <param name="points">Массив точек</param>
        /// <param name="interpalationParams">Праметры интерполяции</param>
        public void InterpolateFromPoints(List<Geopoint> points, InterpalationParams interpalationParams)
        {
            if (interpalationParams == null)
            {
                throw new ArgumentException("interpolationParams null");
            }
            switch (interpalationParams.SearchType)
            {
                case SearchType.SearchRadius:

                    InterpolateFromPointsBySearchRadius(points, interpalationParams);
                    break;
                case SearchType.NearestCount:
                    InterpolateFromPointsByNearestCount(points, interpalationParams);
                    break;
                default: throw new NotSupportedException($"Нет реализации для {interpalationParams.SearchType}");
            }
            UpdateMinMax();
        }
        public void InterpolateFromPoints(VectorLayer layer, InterpalationParams interpalationParams)
        {
            List<Geopoint> points = (from obj in layer.Objects
                                     where obj is Point
                                     select (obj as Point).Location).ToList();
            InterpolateFromPoints(points, interpalationParams);
        }
        private const int InfluenceDegreeCount = 5;
        /// <summary>
        /// Рассчитать невеязку методом перекрестной проверки для каждого p 
        /// </summary>
        /// <param name="points">Набор точек</param>
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high TODO: (26.02.2023) Исправить метод 
        public static IEnumerable<Tuple<double, double>> CalculateAccuracy(List<Geopoint> points)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
        {
            double r;
            List<Tuple<double, double>> res = new List<Tuple<double, double>>();
            for (int p = 1; p <= InfluenceDegreeCount; p++) //степень влияния
            {
                double sumDelZ = 0;
                double delZ = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    double x = points[i].X;
                    double y = points[i].Y;

                    double sum1 = 0;
                    double sum2 = 0;

                    for (int k = 0; k < points.Count; k++) // метод обратных взвеш. расст. (ОВР)
                    {
                        if (k == i)
                        {
                            continue;
                        }

                        double r2 = Math.Pow(x - points[k].X, 2) + Math.Pow(y - points[k].Y, 2); //квадрат расстояния

                        if (Math.Abs(r2) <= double.Epsilon)
                        {
                            sum1 = points[k].Z;
                            sum2 = 1;
                            break;
                        }

                        r =p == DefaultPopularInterpolationParamsPower ? r2 : Math.Pow(Math.Sqrt(r2), p); // Оптимизация степени 2

                        sum1 += points[k].Z / r;
                        sum2 += 1 / r;
                    }

                    delZ = sum1 / sum2 - points[i].Z;
                    sumDelZ += Math.Pow(delZ, DefaultPopularInterpolationParamsPower); //считается медленнее, чем перемножение числа на число

                }

                double sigma = Math.Sqrt(sumDelZ / points.Count);
                res.Add(new Tuple<double, double>(p, sigma));
            }
            return res;
        }
#pragma warning disable S1541 // Methods and properties should not be too complex
        private void UpdateMinMax()
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
            for (int i = 0; i < (gridGeometry?.CountY ?? 0); i++)
            {
                for (int j = 0; j < (gridGeometry?.CountX ?? 0); j++)
                {
                    matrixMinValue = Math.Min(matrixMinValue ?? double.MaxValue, matrix[i, j] ?? double.MaxValue);
                    matrixMaxValue = Math.Max(matrixMaxValue ?? double.MinValue, matrix[i, j] ?? double.MinValue);
                }
            }
        }
        private readonly double?[,] matrix;
        public override void LoadFromFile(string path)
        {
        }

        public override void Draw(PaintEventArgs e)
        {
            bitmap ??= new Bitmap(gridGeometry.CountX, gridGeometry.CountY);
            RecoloringBitmap();
            System.Drawing.Point point1 = Map.MapToScreen(new Geopoint(GridGeometry.Xmin - gridGeometry.Cell / 2, gridGeometry.Ymax + gridGeometry.Cell / 2));
            System.Drawing.Point point2 = Map.MapToScreen(new Geopoint(GridGeometry.Xmax + gridGeometry.Cell / 2, gridGeometry.Ymin - gridGeometry.Cell / 2));
            e.Graphics.DrawRectangle(new System.Drawing.Pen(Color.Black), point1.X, point1.Y,
                point2.X - point1.X, point2.Y - point1.Y);
            e.Graphics.DrawImage(bitmap, point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);
        }
        /// <summary>
        /// Значение геополя
        /// </summary>
        /// <param name="x">Координата X внутри прямоугольника</param>
        /// <param name="y">Координата Y внутри прямоугольника</param>
        public double? GetValue(double x, double y)
        {
            if (x < gridGeometry.Xmin || y < gridGeometry.Ymin || x > gridGeometry.Xmax || y > gridGeometry.Ymax)
            {
                return null;
            }
            int indexI = (int)((y - gridGeometry.Ymin) / gridGeometry.Cell);
            int indexJ = (int)((x - gridGeometry.Xmin) / gridGeometry.Cell);
            double? z1 = matrix[indexI, indexJ];
            double? z2 = matrix[indexI + 1, indexJ];
            double? z3 = matrix[indexI, indexJ + 1];
            double? z4 = matrix[indexI + 1, indexJ + 1];
            if (z1 == null || z2 == null || z3 == null || z4 == null) 
            { 
                return null; 
            }
            double z5 = (double)(((y - (gridGeometry.Ymin + (gridGeometry.Cell * indexI))) * (z2 - z1) / gridGeometry.Cell) + z1);
            double z6 = (double)(((y - (gridGeometry.Ymin + (gridGeometry.Cell * indexI))) * (z4 - z3) / gridGeometry.Cell) + z3);
            return (x - (gridGeometry.Xmin + gridGeometry.Cell * indexJ)) * (z6 - z5) / gridGeometry.Cell + z5;
        }
        public double? GetNode(int i, int j) => matrix[i, j];
        public void SetNode(int i, int j, double? value) => matrix[i, j] = value;
        private Bitmap bitmap;
        private void RecoloringBitmap()
        {
            for (int i = 0; i < gridGeometry.CountY; i++)
            {
                for (int j = 0; j < gridGeometry.CountX; j++)
                {
                    Color color = GetColorForValue(matrix[i, j]);
                    bitmap.SetPixel(j, bitmap.Height - i - 1, color);
                }
            }
        }
        public Color ColorMin { get; set; } = Color.Blue;
        public Color ColorMax { get; set; } = Color.Red;
        private Color GetColorForValue(double? value)
        {
            if (!value.HasValue) 
            {
                return Color.Transparent; 
            }
            double k = (double)((value - matrixMinValue) / (matrixMaxValue - matrixMinValue));

            int r = (int)Math.Round((ColorMax.R - ColorMin.R) * k + ColorMin.R);
            int g = (int)Math.Round((ColorMax.G - ColorMin.G) * k + ColorMin.G);
            int b = (int)Math.Round((ColorMax.B - ColorMin.B) * k + ColorMin.B);

            return Color.FromArgb(r, g, b);
        }
    }

}
