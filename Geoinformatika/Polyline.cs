using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Geoinformatika
{
    public class Polyline : MapObject
    {
        public List<Geopoint> Nodes { get; set; } = new List<Geopoint>();
        public LineStyle Style { get; set; }
        public Polyline(string mifString)
        {
            List<string> splited = mifString.Split('\r').ToList();
            if (string.IsNullOrEmpty(splited.Last())) splited.RemoveAt(splited.Count - 1);
            try
            {
                int n = Convert.ToInt32(splited[1]);
                for (int i = 0; i < n; i++)
                {
                    const string regexp = @"(-?\d*), ?(-?\d*) (-?\d*), ?(-?\d*)";
                    int x1 = Convert.ToInt32(Regex.Match(splited[i + 2], regexp).Groups[1].Value, CultureInfo.CurrentCulture);
                    int y1 = Convert.ToInt32(Regex.Match(splited[i + 2], regexp).Groups[2].Value, CultureInfo.CurrentCulture);
                    int x2 = Convert.ToInt32(Regex.Match(splited[i + 2], regexp).Groups[3].Value, CultureInfo.CurrentCulture);
                    int y2 = Convert.ToInt32(Regex.Match(splited[i + 2], regexp).Groups[4].Value, CultureInfo.CurrentCulture);
                    AddNode(new Geopoint(x1, y1));
                    AddNode(new Geopoint(x2, y2));
                }
            }
            catch
            {
                throw new NotSupportedException("Неверный формат");
            }
            if (splited.Last().Contains("PEN"))
            {
                const string penRegex = @"PEN\D(-?\d*), (-?\d*), (-?\d*)";
                var match = Regex.Match(mifString, penRegex);
                int width;
                int type;
                int color;
                Color rGBColor;
                try
                {
                    width = Convert.ToByte(match.Groups[1].Value, CultureInfo.CurrentCulture);
                    type = Convert.ToInt32(match.Groups[2].Value, CultureInfo.CurrentCulture);
                    color = Convert.ToInt32(match.Groups[3].Value, CultureInfo.CurrentCulture);
                    rGBColor = Color.FromArgb((color & 0xFF0000) / 65534, (color & 0xFF00) / 256, color & 0xFF);
                }
                catch
                {
                    throw new NotSupportedException("Неверная mif строка (Pen)");
                }
                Style = new LineStyle(width, type, rGBColor);
            }
        }
        public Polyline()
        {
            Style = new LineStyle();
        }
        public Polyline(List<Geopoint> list)
        {
            foreach (var i in list)
            {
                Nodes.Add(i);
            }
            Style = new LineStyle();
        }
        public void DeleteNode(int index)
        {
            Nodes.RemoveAt(index);
        }
        public void AddNode(Geopoint node)
        {
            Nodes.Add(node);
        }
        public void InsertNode(Geopoint node, int index)
        {
            Nodes.Insert(index, node);
        }

        /// <summary>
        /// Отрисовка полилинии
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(PaintEventArgs e)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(Selected ? Color.DarkRed : Style.Color, Style.Wight);
            pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)Style.Type;
            List<System.Drawing.Point> mapPoints = new List<System.Drawing.Point>();
            foreach (var item in Nodes)
            {
                mapPoints.Add(Layer.Map.MapToScreen(item));
            }
            e.Graphics.DrawLines(pen, mapPoints.ToArray());
        }
        /// <summary>
        /// Получить рамку полилинии
        /// </summary>
        /// <returns></returns>
        protected override GeoRect GetBounds()
        {
            double xMin = Nodes.Select(p => p.X).Min();
            double yMin = Nodes.Select(p => p.Y).Min();
            double xMax = Nodes.Select(p => p.X).Max();
            double yMax = Nodes.Select(p => p.Y).Max();
            return new GeoRect(xMin, yMin, xMax, yMax);
        }
        public override MapObject IsCross(GeoRect search)
        {
            int crossCount = 0;
            if (search.Ymin >= this.GetBounds().Ymin && search.Ymin <= this.GetBounds().Ymax
                && search.Xmin>= this.GetBounds().Xmin && search.Xmax <= this.GetBounds().Xmax)
            {
                Geopoint startLinePoint = new Geopoint(search.Xmin, search.Ymin);
                Geopoint endLinePoint = new Geopoint(this.GetBounds().Xmax, search.Ymin);

                for (int i = 0; i < Nodes.Count - 1; i++)
                {
                    if (CrossHelper.IsCrossLines(Nodes[i], Nodes[i + 1], startLinePoint, endLinePoint)) crossCount++;
                }

            }
            if (crossCount % 2 != 0) return this;
            return null;
        }

    }
}
