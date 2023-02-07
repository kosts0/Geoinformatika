using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geoinformatika
{
    /// <summary>
    /// Сеть
    /// </summary>
    public class GridGeometry
    {
        public GridGeometry(double xmin, double ymin, double cell, int countx,int county)
        {
            xMin = xmin;
            yMin = ymin;
            this.cell = cell;
            countX = countx;
            countY = county;
        }
        /// <summary>
        /// Левый край X
        /// </summary>
        private double xMin;
        /// <summary>
        /// Нижний край Y
        /// </summary>
        private double yMin;
        /// <summary>
        /// Кол-во квадратов по X
        /// </summary>
        private int countX;
        /// <summary>
        /// Кол-во квадратов по Y
        /// </summary>
        private int countY;
        /// <summary>
        /// Шаг сетки
        /// </summary>
        private double cell;
        public double Cell
        {
            set
            {
                cell = value;
            }
            get => cell;
        }
        public double Xmin
        {
            set
            {
                xMin = value;
            }
            get => xMin;
        }
        public double Ymin
        {
            set
            {
                yMin = value;
            }
            get => yMin;
        }
        public int CountX
        {
            set
            {
                countX = value;
            }
            get => countX;
        }
        public int CountY
        {
            set
            {
                countY = value;
            }
            get => countY;
        }
        public double Xmax => xMin + cell * countX;
        public double Ymax => yMin + cell * countY;
    }
}
