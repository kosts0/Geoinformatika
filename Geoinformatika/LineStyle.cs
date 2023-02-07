using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geoinformatika
{
    /// <summary>
    /// Класс линии
    /// </summary>
    public class LineStyle
    {
        /// <summary>
        /// Ширина
        /// </summary>
        public int Wight;
        /// <summary>
        /// Тип (пунктир, сплошная)
        /// </summary>
        public int Type;
        /// <summary>
        /// Цвет
        /// </summary>
        public Color Color;
        public LineStyle()
        {
            Wight = 5;
            Type = 0;
            Color = Color.Red;
        }
        public LineStyle(int wight,int type,Color color)
        {
            Wight = wight;
            Type = type;
            Color = color;
        }
    }
}
