using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geoinformatika
{
    /// <summary>
    /// Класс символа
    /// </summary>
    public class SymbolStyle
    {
        /// <summary>
        /// Тип символа (код)
        /// </summary>
        public byte Type;
        /// <summary>
        /// Размер
        /// </summary>
        public int Size;
        /// <summary>
        /// Цвет
        /// </summary>
        public Color Color;
        /// <summary>
        /// Шрифт
        /// </summary>
        public string Font = "Wingdings";
        public SymbolStyle()
        {
            Type = 0x26;
            Color = Color.Blue;
            Size = 14;
            Font = "Wingdings";
        }
        public SymbolStyle(byte type,int size,Color color,string font)
        {
            Type = type;
            Size = size;
            Color = color;
            Font = font;
        }
        public SymbolStyle(byte type,  Color color, int size)
        {
            Type = type;
            Size = size;
            Color = color;
        }
    }
}
