// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Geoinformatika
{
    public class GridLayer : AbstractLayer
    {
        private GridGeometry gridGeometry;
        public GridGeometry GridGeometry => gridGeometry;

        public override GeoRect GetBounds => new GeoRect(GridGeometry.Xmin, GridGeometry.Ymin, GridGeometry.Xmax, GridGeometry.Ymax);
        double? matrixMinValue, matrixMaxValue;
        public GridLayer(GridGeometry geometry = null)
        {
            gridGeometry = geometry;
            matrix = new double?[(int)(geometry?.CountY), (int)(geometry?.CountX)];
            double A = 10;
            double B = 0.1;
            for (int i = 0; i < (int)(geometry?.CountY); i++)
            {
                for (int j = 0; j < (int)(geometry?.CountX); j++)
                {
                    double nodeValue = A * Math.Sin(B * i) * Math.Sin(B * j);
                    //if (i > 20 && i < 100 && j > 20 && j < 100) SetNode(i, j, null);
                    SetNode(i, j, nodeValue);
                }
            }
            updateMinMax();
        }
        /// <summary>
        /// Интерполяция точек
        /// </summary>
        /// <param name="points">Массив точек</param>
        /// <param name="interpalationParams">Праметры интерполяции</param>
        public void InterpolateFromPoints(List<Geopoint> points, InterpalationParams interpalationParams)
        {
            if (interpalationParams == null) throw new ArgumentException("interpolationParams null");
            double sum1, sum2;
            double searchRadius2;
            double rp;
            double r2;
            switch (interpalationParams.SearchType)
            {
                case SearchType.SearchRadius:

                    searchRadius2 = interpalationParams.SearchRadius * interpalationParams.SearchRadius;
                    for (int i = 0; i < GridGeometry.CountY; i++)
                    {

                        for (int j = 0; j < GridGeometry.CountX; j++)
                        {
                            bool f = false;
                            double x = GridGeometry.Xmin + j * GridGeometry.Cell;
                            double y = GridGeometry.Ymin + i * GridGeometry.Cell;
                            sum1 = 0;
                            sum2 = 0;

                            for (int k = 0; k < points.Count; k++)
                            {
                                r2 = (points[k].X - x) * (points[k].X - x) + (points[k].Y - y) * (points[k].Y - y);
                                if (r2 == 0)
                                {
                                    sum1 = points[k].Z;
                                    sum2 = 1;
                                    f = true;
                                    break;
                                }
                                if (r2 <= searchRadius2)
                                {
                                    f = true;

                                    if (interpalationParams.Power == 2) rp = r2;
                                    else rp = Math.Pow(Math.Sqrt(r2), interpalationParams.Power);
                                    sum1 += points[k].Z / rp;
                                    sum2 += 1 / rp;
                                }
                            }
                            if (f)
                            {
                                matrix[i, j] = sum1 / sum2;
                            }
                            else matrix[i, j] = null;

                        }
                    }
                    break;
                case SearchType.NearestCount:
                    searchRadius2 = interpalationParams.SearchRadius * interpalationParams.SearchRadius;
                    for (int i = 0; i < GridGeometry.CountY; i++)
                    {
                        for (int j = 0; j < GridGeometry.CountX; j++)
                        {
                            bool f = false;
                            double x = GridGeometry.Xmin + j * GridGeometry.Cell;
                            double y = GridGeometry.Ymin + i * GridGeometry.Cell;
                            sum1 = 0;
                            sum2 = 0;
                            List<Tuple<double, Geopoint>> tupleDist = new List<Tuple<double, Geopoint>>();
                            for (int k = 0; k < points.Count; k++)
                            {
                                r2 = (points[k].X - x) * (points[k].X - x) + (points[k].Y - y) * (points[k].Y - y);
                                if (r2 <= searchRadius2)
                                {
                                    tupleDist.Add(new Tuple<double, Geopoint>(r2, points[k]));
                                }
                            }
                            var sorted = tupleDist.OrderBy(t => t.Item1).ToList();
                            for (int k = 0; k < sorted.Count && k < interpalationParams.NearestCount; k++)
                            {
                                if (interpalationParams.Power == 2) rp = sorted[k].Item1;
                                else rp = Math.Pow(Math.Sqrt(sorted[k].Item1), interpalationParams.Power);
                                if (rp == 0)
                                {
                                    sum1 = sorted[k].Item2.Z;
                                    sum2 = 1;
                                }
                                else
                                {
                                    sum1 += sorted[k].Item2.Z / rp;
                                    sum2 += 1 / rp;
                                }
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
                    break;
            }
            updateMinMax();
        }
        public void InterpolateFromPoints(VectorLayer layer, InterpalationParams interpalationParams)
        {
            List<Geopoint> points = new List<Geopoint>();
            foreach (var obj in layer.objects)
            {
                if (obj is Point) points.Add((obj as Point).location);
            }
            InterpolateFromPoints(points, interpalationParams);
        }
        /// <summary>
        /// Рассчитать невеязку методом перекрестной проверки для каждого p 
        /// </summary>
        /// <param name="points">Набор точек</param>
        public static List<Tuple<double, double>> CalculateAccuracy(List<Geopoint> points)
        {
            double minSigma;
            int minSigmaP;
            double r;
            List<Tuple<double, double>> res = new List<Tuple<double, double>>();
            for (int p = 1; p <= 5; p++) //степень влияния
            {
                double sumDelZ = 0;
                double delZ = 0;
                minSigma = double.MaxValue;
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

                        if (r2 == 0)
                        {
                            sum1 = points[k].Z;
                            sum2 = 1;
                            break;
                        }

                        if (p == 2) //оптимизация для степени 2
                        {
                            r = r2;
                        }
                        else
                        {
                            r = Math.Pow(Math.Sqrt(r2), p);
                        }

                        sum1 += points[k].Z / r;
                        sum2 += 1 / r;
                    }

                    delZ = sum1 / sum2 - points[i].Z;
                    sumDelZ += Math.Pow(delZ, 2); //считается медленнее, чем перемножение числа на число

                }

                double sigma = Math.Sqrt(sumDelZ / points.Count);
                res.Add(new Tuple<double, double>(p, sigma));
            }
            return res;
        }
        private void updateMinMax()
        {
            for (int i = 0; i < (int)(gridGeometry?.CountY); i++)
            {
                for (int j = 0; j < (int)(gridGeometry?.CountX); j++)
                {
                    matrixMinValue = Math.Min(matrixMinValue ?? double.MaxValue, matrix[i, j] ?? double.MaxValue);
                    matrixMaxValue = Math.Max(matrixMaxValue ?? double.MinValue, matrix[i, j] ?? double.MinValue);
                }
            }
        }
        private readonly double?[,] matrix;
        public override void LoadFromFile(string fileName)
        {
        }

        public override void Draw(PaintEventArgs e)
        {
            bitmap ??= new Bitmap(gridGeometry.CountX, gridGeometry.CountY);
            recoloringBitmap();
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
            if (x < gridGeometry.Xmin || y < gridGeometry.Ymin || x > gridGeometry.Xmax || y > gridGeometry.Ymax) return null;
            int indexI = (int)((y - gridGeometry.Ymin) / gridGeometry.Cell);
            int indexJ = (int)((x - gridGeometry.Xmin) / gridGeometry.Cell);
            double? z1 = matrix[indexI, indexJ];
            double? z2 = matrix[indexI + 1, indexJ];
            double? z3 = matrix[indexI, indexJ + 1];
            double? z4 = matrix[indexI + 1, indexJ + 1];
            if (z1 == null || z2 == null || z3 == null || z4 == null) return null;
            double z5 = (double)(((y - (gridGeometry.Ymin + (gridGeometry.Cell * indexI))) * (z2 - z1) / gridGeometry.Cell) + z1);
            double z6 = (double)(((y - (gridGeometry.Ymin + (gridGeometry.Cell * indexI))) * (z4 - z3) / gridGeometry.Cell) + z3);
            return (x - (gridGeometry.Xmin + gridGeometry.Cell * indexJ)) * (z6 - z5) / gridGeometry.Cell + z5;
        }
        public double? GetNode(int i, int j) => matrix[i, j];
        public void SetNode(int i, int j, double? value) => matrix[i, j] = value;
        private Bitmap bitmap;
        private void recoloringBitmap()
        {
            for (int i = 0; i < gridGeometry.CountY; i++)
            {
                for (int j = 0; j < gridGeometry.CountX; j++)
                {
                    Color color = getColorForValue(matrix[i, j]);
                    bitmap.SetPixel(j, bitmap.Height - i - 1, color);
                }
            }
        }
        public Color colorMin { get; set; } = Color.Blue;
        public Color colorMax { get; set; } = Color.Red;
        private Color getColorForValue(double? value)
        {
            if (!value.HasValue) return Color.Transparent;
            double k = (double)((value - matrixMinValue) / (matrixMaxValue - matrixMinValue));

            int r = (int)Math.Round((colorMax.R - colorMin.R) * k + colorMin.R);
            int g = (int)Math.Round((colorMax.G - colorMin.G) * k + colorMin.G);
            int b = (int)Math.Round((colorMax.B - colorMin.B) * k + colorMin.B);

            return Color.FromArgb(r, g, b);
        }
    }

}
