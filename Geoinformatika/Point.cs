using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Geoinformatika
{
    /// <summary>
    /// Геоданные точки
    /// </summary>
    public class Geopoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
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
        public Geopoint Location { get; set; }
        /// <summary>
        /// Стиль тички (символ)
        /// </summary>
        public SymbolStyle Style { get; set; }
        public Point(Geopoint point)
        {
            Location = point;
            Style = new SymbolStyle();
        }
        public Point(double x, double y)
        {
            Location = new Geopoint(x, y);
            Style = new SymbolStyle();
        }
        public Point(Geopoint point, SymbolStyle symbolStyle)
        {
            Location = point;
            Style = symbolStyle;
        }
        public Point(string mifString)
        {
            const string regex = @"POINT (-?\d*) (-?\d*)";
            var match = Regex.Match(mifString, regex);
            int x;
            int y;
            try
            {
                x = Convert.ToInt32(match.Groups[1].Value, CultureInfo.CurrentCulture);
                y = Convert.ToInt32(match.Groups[2].Value, CultureInfo.CurrentCulture);
            }
            catch
            {
                throw new NotSupportedException("Неверная mif строка");
            }
            const string stymbolReges = @"Symbol\D(\d*), (\d*), (\d*)";
            match = Regex.Match(mifString, stymbolReges);
            byte type;
            int color;
            Color rGBColor;
            int size;
            try
            {
                type = Convert.ToByte(match.Groups[1].Value, CultureInfo.CurrentCulture);
                color = Convert.ToInt32(match.Groups[2].Value, CultureInfo.CurrentCulture);
                rGBColor = Color.FromArgb((color & 0xFF0000) / 65534, (color & 0xFF00) / 256, color & 0xFF);
                size = Convert.ToInt32(match.Groups[3].Value, CultureInfo.CurrentCulture);
            }
            catch
            {
                throw new NotSupportedException("Неверная mif строка (Style)");
            }
            SymbolStyle style = new SymbolStyle(type, rGBColor, size);
            Location = new Geopoint(x, y);
            Style = style;
        }
        public override void Draw(PaintEventArgs e)
        {
            System.Drawing.Point mapToScreenPoint = Layer.Map.MapToScreen(Location);
            string symbol = Convert.ToChar(Style.Type, CultureInfo.CurrentCulture).ToString();
            var font = new System.Drawing.Font(Style.Font, Style.Size);
            var measureString = e.Graphics.MeasureString(symbol, font);
            mapToScreenPoint.X = (int)(mapToScreenPoint.X - measureString.Width / 2);
            mapToScreenPoint.Y = (int)(mapToScreenPoint.Y - measureString.Height / 2);
            var brush = new System.Drawing.SolidBrush(Selected ? Color.DarkRed : Style.Color);
            e.Graphics.DrawString(symbol, font, brush, mapToScreenPoint);
        }
        /// <summary>
        /// Получить рамку для точки
        /// </summary>
        /// <returns></returns>
        protected override GeoRect GetBounds()
        {
            return new GeoRect(Location.X, Location.Y, Location.X, Location.Y);
        }
        public override MapObject IsCross(GeoRect search)
        {
            System.Drawing.Graphics graphics = Layer.Map.CreateGraphics();
            string symbol = Convert.ToChar(Style.Type, CultureInfo.CurrentCulture).ToString();
            System.Drawing.Font font = new System.Drawing.Font(Style.Font, Style.Size);
            var size = graphics.MeasureString(symbol, font);
            GeoRect rect = new GeoRect(Location.X - (size.Width / 2) / Layer.Map.MapScale, Location.Y - (size.Height / 2) / Layer.Map.MapScale,
                Location.X + (size.Width / 2) / Layer.Map.MapScale, Location.Y + (size.Height / 2) / Layer.Map.MapScale);
            if (GeoRect.IsIntersect(search, rect))
            {
                return this;
            }
            return null;
        }
    }
}
