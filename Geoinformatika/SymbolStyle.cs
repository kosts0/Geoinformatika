// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.Drawing;

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
            Type = 0xA7;
            Color = Color.Blue;
            Size = 14;
            Font = "Wingdings";
        }
        public SymbolStyle(byte type, int size, Color color, string font)
        {
            Type = type;
            Size = size;
            Color = color;
            Font = font;
        }
        public SymbolStyle(byte type, Color color, int size)
        {
            Type = type;
            Size = size;
            Color = color;
        }
    }
}
