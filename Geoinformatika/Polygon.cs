// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Geoinformatika
{
    /// <summary>
    /// Замкнутый объект из линий
    /// </summary>
    public class Polygon : Polyline, IDisposable
    {
        public void Dispose()
        {
            SolidBrush.Dispose();
        }
        /// <summary>
        /// Заливка
        /// </summary>
        SolidBrush SolidBrush { get; set; }
        public Polygon()
        {
            SolidBrush = new SolidBrush(Color.Green);
        }
        public Polygon(string mifString)
        {
            List<string> splited = mifString.Split('\r').ToList();
            int n = 0;
            try
            {
                n = Convert.ToInt32(splited[1], CultureInfo.CurrentCulture);
                const string regex = @"(-?\d*) ?(-?\d*)";
                for (int i = 0; i < n; i++)
                {
                    int x1 = Convert.ToInt32(Regex.Match(splited[i + 2], regex).Groups[1].Value);
                    int y1 = Convert.ToInt32(Regex.Match(splited[i + 2], regex).Groups[2].Value);
                    AddNode(new Geopoint(x1, y1));
                }
            }
            catch
            {
                throw new NotSupportedException("Неверный формат");
            }
            for (int i = n+2; i < splited.Count; i++)
            {
                if (splited[i].Contains("PEN"))
                {
                    const string penRegex = @"PEN\D(-?\d*), (-?\d*), (-?\d*)";
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
                        throw new NotSupportedException("Неверная mif строка (Pen)");
                    }
                    Style = new LineStyle(width, type, rGBColor);
                }
                if (splited[i].Contains("Brush"))
                {
                    const string penRegex = @"Brush\D(-?\d*)";
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
                        throw new NotSupportedException("Неверная mif строка (Pen)");
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
            System.Drawing.Pen pen = new Pen(Selected ? Color.DarkRed : Style.Color, Style.Wight);
            List<System.Drawing.Point> mapPoints = new List<System.Drawing.Point>();
            foreach (var item in Nodes)
            {
                mapPoints.Add(Layer.Map.MapToScreen(item));
            }
            e.Graphics.DrawPolygon(pen, mapPoints.ToArray());
            e.Graphics.FillPolygon(SolidBrush, mapPoints.ToArray());
            if (Selected)
            {
                DrawAreaSize(e);
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
        protected void DrawAreaSize(PaintEventArgs e)
        {
            string s = S.ToString(CultureInfo.CurrentCulture);
            if (s.Replace('.', ',').Contains(',')) s = s.Substring(0, s.IndexOf(',') + 2);
            e.Graphics.DrawString(s,
                    new Font(FontFamily.GenericMonospace, emSize: 14),
                    new SolidBrush(Color.Blue),
                    Layer.Map.MapToScreen(new Geopoint((GetBounds().Xmax + GetBounds().Xmin) / 2, (GetBounds().Ymax + GetBounds().Ymin) / 2)));
        }


    }
}
