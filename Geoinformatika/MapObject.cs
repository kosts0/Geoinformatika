// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System.Windows.Forms;

namespace Geoinformatika
{
    /// <summary>
    /// Базовый класс объекта
    /// </summary>
    public abstract class MapObject
    {
        /// <summary>
        /// Связка со слоем
        /// </summary>
        public VectorLayer Layer { get; set; }
        /// <summary>
        /// Абстрактный метод отрисовки
        /// </summary>
        /// <param name="e"></param>
        public abstract void Draw(PaintEventArgs e);
        public GeoRect Bounds => GetBounds();
        protected abstract GeoRect GetBounds();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public abstract MapObject IsCross(GeoRect search);
        public bool Selected { get; set; }
    }
}
