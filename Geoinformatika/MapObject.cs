using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public VectorLayer layer;
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
        public bool Selected;
    }
}
