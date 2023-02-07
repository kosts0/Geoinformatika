using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geoinformatika
{
    public class Line : MapObject
    {
        public Geopoint Beginpoint;
        public Geopoint Endpoint;
        public LineStyle Style;
        public Line(Geopoint begin, Geopoint end)
        {
            Beginpoint = begin;
            Endpoint = end;
            Style = new LineStyle();
        }
        public Line(string mifString)
        {
            string regex = @"LINE (-?\d*) (-?\d*) (-?\d*) (-?\d*)";
            var match = Regex.Match(mifString, regex);
            int x1, y1,x2,y2;
            try
            {
                x1 = Convert.ToInt32(match.Groups[1].Value);
                y1 = Convert.ToInt32(match.Groups[2].Value);
                x2 = Convert.ToInt32(match.Groups[3].Value);
                y2 = Convert.ToInt32(match.Groups[4].Value);

            }
            catch
            {
                throw new Exception("Неверная mif строка");
            }
            string penRegex = @"PEN\D(-?\d*), (-?\d*), (-?\d*)";
            match = Regex.Match(mifString, penRegex);
            int width;
            int type;
            int color;
            Color rGBColor;
            try
            {
                width = Convert.ToByte(match.Groups[1].Value);
                type = Convert.ToInt32(match.Groups[2].Value);
                color = Convert.ToInt32(match.Groups[3].Value);
                rGBColor = Color.FromArgb((color & 0xFF0000) / 65534, (color & 0xFF00) / 256, color & 0xFF);
            }
            catch
            {
                throw new Exception("Неверная mif строка (Pen)");
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
            System.Drawing.Point mapToScreenBeginPoint = layer.Map.MapToScreen(Beginpoint);
            System.Drawing.Point mapToScreenEndPoint = layer.Map.MapToScreen(Endpoint);
            System.Drawing.Pen pen = new System.Drawing.Pen(Selected ? Color.DarkRed : Style.Color, Style.Wight);
            pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)Style.Type;
            e.Graphics.DrawLine(pen, mapToScreenBeginPoint, mapToScreenEndPoint);
        }

        public override MapObject IsCross(GeoRect search)
        {
            double dx = search.Xmax - search.Xmin;
            double dy = search.Ymax - search.Ymin;
            if (CrossHelper.IsCrossLines(new Geopoint(search.Xmin, search.Ymin), new Geopoint(search.Xmax, search.Ymin), Beginpoint, Endpoint)) return this;
            if (CrossHelper.IsCrossLines(new Geopoint(search.Xmin, search.Ymin), new Geopoint(search.Xmin, search.Ymax), Beginpoint, Endpoint)) return this;
            if (CrossHelper.IsCrossLines(new Geopoint(search.Xmin, search.Ymax), new Geopoint(search.Xmax, search.Ymax), Beginpoint, Endpoint)) return this;
            if (CrossHelper.IsCrossLines(new Geopoint(search.Xmax, search.Ymin), new Geopoint(search.Xmax, search.Ymax), Beginpoint, Endpoint)) return this;
            return null;
        }

        protected override GeoRect GetBounds()
        {
            return new GeoRect(Math.Min(Beginpoint.X, Endpoint.X), Math.Min(Beginpoint.Y, Endpoint.Y),
                Math.Max(Beginpoint.X, Endpoint.X), Math.Max(Beginpoint.Y, Endpoint.Y));
        }
    }
}
