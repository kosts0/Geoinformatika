using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Geoinformatika
{
    public class Line : MapObject
    {
        public Geopoint Beginpoint { get; set; }
        public Geopoint Endpoint { get; set; }
        public LineStyle Style { get; set; }
        public Line(Geopoint begin, Geopoint end)
        {
            Beginpoint = begin;
            Endpoint = end;
            Style = new LineStyle();
        }
        public Line(string mifString)
        {
            const string regex = @"LINE (-?\d*) (-?\d*) (-?\d*) (-?\d*)";
            var match = Regex.Match(mifString, regex);
            int x1;
            int y1;
            int x2;
            int y2;
            try
            {
                x1 = Convert.ToInt32(match.Groups[1].Value, CultureInfo.CurrentCulture);
                y1 = Convert.ToInt32(match.Groups[2].Value, CultureInfo.CurrentCulture);
                x2 = Convert.ToInt32(match.Groups[3].Value, CultureInfo.CurrentCulture);
                y2 = Convert.ToInt32(match.Groups[4].Value, CultureInfo.CurrentCulture);

            }
            catch
            {
                throw new NotSupportedException("Неверная mif строка");
            }
            const string penRegex = @"PEN\D(-?\d*), (-?\d*), (-?\d*)";
            match = Regex.Match(mifString, penRegex);
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
            LineStyle style = new LineStyle(width, type, rGBColor);
            Beginpoint = new Geopoint(x1, y1);
            Endpoint = new Geopoint(x2, y2);
            Style = style;
        }
        /// <summary>
        /// Линия
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(PaintEventArgs e)
        {
            System.Drawing.Point mapToScreenBeginPoint = Layer.Map.MapToScreen(Beginpoint);
            System.Drawing.Point mapToScreenEndPoint = Layer.Map.MapToScreen(Endpoint);
            System.Drawing.Pen pen = new System.Drawing.Pen(Selected ? Color.DarkRed : Style.Color, Style.Wight);
            pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)Style.Type;
            e.Graphics.DrawLine(pen, mapToScreenBeginPoint, mapToScreenEndPoint);
        }

        public override MapObject IsCross(GeoRect search)
        {
            if (CrossHelper.IsCrossLines(new Geopoint(search.Xmin, search.Ymin), new Geopoint(search.Xmax, search.Ymin), Beginpoint, Endpoint)
                || CrossHelper.IsCrossLines(new Geopoint(search.Xmin, search.Ymin), new Geopoint(search.Xmin, search.Ymax), Beginpoint, Endpoint)
                || CrossHelper.IsCrossLines(new Geopoint(search.Xmin, search.Ymax), new Geopoint(search.Xmax, search.Ymax), Beginpoint, Endpoint)
                || CrossHelper.IsCrossLines(new Geopoint(search.Xmax, search.Ymin), new Geopoint(search.Xmax, search.Ymax), Beginpoint, Endpoint))
            {
                return this;
            }
            return null;
        }

        protected override GeoRect GetBounds()
        {
            return new GeoRect(Math.Min(Beginpoint.X, Endpoint.X), Math.Min(Beginpoint.Y, Endpoint.Y),
                Math.Max(Beginpoint.X, Endpoint.X), Math.Max(Beginpoint.Y, Endpoint.Y));
        }
    }
}
