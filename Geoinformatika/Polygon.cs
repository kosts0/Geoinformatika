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
    /// Замкнутый объект из линий
    /// </summary>
    public class Polygon : Polyline
    {
        /// <summary>
        /// Заливка
        /// </summary>
        SolidBrush SolidBrush;
        public Polygon() : base()
        {
            SolidBrush = new SolidBrush(Color.Green);
        }
        public Polygon(string mifString)
        {
            List<string> splited = mifString.Split('\r').ToList();
            int n = 0;
            try
            {
                n = Convert.ToInt32(splited[1]);
                for (int i = 0; i < n; i++)
                {
                    int x1 = Convert.ToInt32(Regex.Match(splited[i + 2], @"(-?\d*) ?(-?\d*)").Groups[1].Value);
                    int y1 = Convert.ToInt32(Regex.Match(splited[i + 2], @"(-?\d*) ?(-?\d*)").Groups[2].Value);  
                    AddNode(new Geopoint(x1, y1));
                }
            }
            catch
            {
                throw new Exception("Неверный формат");
            }
            for (int i = n+2; i < splited.Count; i++)
            {
                if (splited[i].Contains("PEN"))
                {
                    string penRegex = @"PEN\D(-?\d*), (-?\d*), (-?\d*)";
                    var match = Regex.Match(mifString, penRegex);
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
                    Style = new LineStyle(width, type, rGBColor);
                }
                if (splited[i].Contains("Brush"))
                {
                    string penRegex = @"Brush\D(-?\d*)";
                    var match = Regex.Match(mifString, penRegex);
                    int color;
                    Color rGBColor;
                    try
                    {
                        color = Convert.ToInt32(match.Groups[1].Value);
                        rGBColor = Color.FromArgb((color & 0xFF0000) / 65534, (color & 0xFF00) / 256, color & 0xFF);
                    }
                    catch
                    {
                        throw new Exception("Неверная mif строка (Pen)");
                    }
                    SolidBrush = new SolidBrush(rGBColor);
                }
            }
        }
    
        /// <summary>
        /// Отрисовка полигона
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(PaintEventArgs e)
        {
            System.Drawing.Pen pen = new Pen(Selected ? Color.DarkRed : Style.Color,Style.Wight);
            List<System.Drawing.Point> mapPoints = new List<System.Drawing.Point>();
            foreach (var item in Nodes)
            {
                mapPoints.Add(layer.Map.MapToScreen(item));
            }
            e.Graphics.DrawPolygon(pen, mapPoints.ToArray());
            e.Graphics.FillPolygon(SolidBrush, mapPoints.ToArray());
            if (Selected)
            {
                drawAreaSize(e);
            }
        }
        /// <summary>
        /// Площадь (по формуле шунтирования)
        /// </summary>
        public double S
        {
            get
            {
                double s = 0;
                for (int i = 1; i < Nodes.Count; i++)
                {
                    s -= Nodes[i - 1].X * Nodes[i].Y;
                }
                s -= Nodes[Nodes.Count - 1].X * Nodes[0].Y;
                for (int i = 1; i < Nodes.Count; i++)
                {
                    s += Nodes[i].X * Nodes[i - 1].Y;
                }
                s += Nodes[0].X * Nodes[Nodes.Count - 1].Y;
                return Math.Abs(s) * 0.5;
            }
        }
        protected void drawAreaSize(PaintEventArgs e)
        {
            string s = S.ToString();
            if (S.ToString().Contains(',')) s = this.S.ToString().Substring(0, S.ToString().IndexOf(',') + 2);
            e.Graphics.DrawString(s,
                    new Font(FontFamily.GenericMonospace, emSize: 14),
                    new SolidBrush(Color.Blue),
                    layer.Map.MapToScreen(new Geopoint((GetBounds().Xmax + GetBounds().Xmin) / 2, (GetBounds().Ymax + GetBounds().Ymin) / 2)));
        }
    }
}
