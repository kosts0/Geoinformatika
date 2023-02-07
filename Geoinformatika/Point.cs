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
    /// <summary>
    /// Геоданные точки
    /// </summary>
    public class Geopoint
    {
        public double X;
        public double Y;
        public double Z;
        public Geopoint(double x1, double y1)
        {
            X = x1;
            Y = y1;
        }
        public Geopoint(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
    /// <summary>
    /// Точка
    /// </summary>
    public class Point : MapObject
    {
        /// <summary>
        /// Геопозиция
        /// </summary>
        public Geopoint location;
        /// <summary>
        /// Стиль тички (символ)
        /// </summary>
        public SymbolStyle Style;
        public Point(Geopoint point)
        {
            location = point;
            Style = new SymbolStyle();
        }
        public Point(double x, double y)
        {
            location = new Geopoint(x, y);
            Style = new SymbolStyle();
        }
        public Point(Geopoint point, SymbolStyle symbolStyle)
        {
            location = point;
            Style = symbolStyle;
        }
        public Point(string mifString)
        {
            string regex = @"POINT (-?\d*) (-?\d*)";
            var match = Regex.Match(mifString, regex);
            int x, y;
            try
            {
                x = Convert.ToInt32(match.Groups[1].Value);
                y = Convert.ToInt32(match.Groups[2].Value);
            }
            catch
            {
                throw new Exception("Неверная mif строка");
            }
            string stymbolReges = @"Symbol\D(\d*), (\d*), (\d*)";
            match = Regex.Match(mifString, stymbolReges);
            byte type;
            int color;
            Color rGBColor;
            int size;
            try
            {
                type = Convert.ToByte(match.Groups[1].Value);
                color = Convert.ToInt32(match.Groups[2].Value);
                rGBColor = Color.FromArgb((color & 0xFF0000) / 65534, (color & 0xFF00) / 256, color & 0xFF);
                size = Convert.ToInt32(match.Groups[3].Value);
            }
            catch
            {
                throw new Exception("Неверная mif строка (Style)");
            }
            SymbolStyle style = new SymbolStyle(type, rGBColor, size);
            location = new Geopoint(x, y);
            Style = style;
        }
        public override void Draw(PaintEventArgs e)
        {
            System.Drawing.Point mapToScreenPoint = layer.Map.MapToScreen(location);
            string symbol = Convert.ToChar(Style.Type).ToString();
            var font = new System.Drawing.Font(Style.Font, Style.Size);
            var measureString = e.Graphics.MeasureString(symbol, font);
            mapToScreenPoint.X = (int) (mapToScreenPoint.X - measureString.Width / 2);
            mapToScreenPoint.Y = (int) (mapToScreenPoint.Y - measureString.Height / 2);
            var brush = new System.Drawing.SolidBrush(Selected ? Color.DarkRed : Style.Color);
            e.Graphics.DrawString(symbol, font, brush, mapToScreenPoint);
            return;
        }
        /// <summary>
        /// Получить рамку для точки
        /// </summary>
        /// <returns></returns>
        protected override GeoRect GetBounds()
        {
            return new GeoRect(location.X, location.Y, location.X, location.Y);
        }
        public override MapObject IsCross(GeoRect search)
        {
            System.Drawing.Graphics graphics = layer.Map.CreateGraphics();
            string symbol = Convert.ToChar(Style.Type).ToString();
            System.Drawing.Font font = new System.Drawing.Font(Style.Font, Style.Size);
            var size = graphics.MeasureString(symbol, font);
            GeoRect rect = new GeoRect(location.X - (size.Width / 2) / layer.Map.MapScale, location.Y - (size.Height / 2) / layer.Map.MapScale,
                location.X + (size.Width / 2) / layer.Map.MapScale, location.Y + (size.Height / 2) / layer.Map.MapScale);
            if (GeoRect.IsIntersect(search, rect) == true)
            {
                return this;
            }
            return null;
        }
    }
}
