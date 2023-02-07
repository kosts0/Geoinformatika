using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geoinformatika
{
    /// <summary>
    /// Рамка примитива
    /// </summary>
    public class GeoRect
    {
        public double Xmin, Ymin, Xmax, Ymax;
        public GeoRect(double xMin,double yMin,double xMax,double yMax)
        {
            Xmin = xMin;
            Ymin = yMin;
            Xmax = xMax;
            Ymax = yMax;
        }
        /// <summary>
        /// GeoRect Существует
        /// </summary>
        public bool IsExist => !(Xmin == 0 && Ymin == 0 && Xmax == 0 && Ymax==0);
        /// <summary>
        /// Объединение двух GeoRect
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static GeoRect Union(GeoRect a, GeoRect b)
        {
            if (!a.IsExist) return b;
            if (!b.IsExist) return a;
            return new GeoRect(xMin: Math.Min(a.Xmin, b.Xmin),yMin: Math.Min(a.Ymin, b.Ymin),
                xMax: Math.Max(a.Xmax, b.Xmax), yMax: Math.Max(a.Ymax, b.Ymax));
        }
        public static bool IsIntersect(GeoRect A, GeoRect B)
        {
            Rectangle AR = new Rectangle((int)A.Xmin, (int)A.Ymin, (int)(A.Xmax - A.Xmin), (int)(A.Ymax - A.Ymin));
            Rectangle BR = new Rectangle((int)B.Xmin, (int)B.Ymin, (int)(B.Xmax - B.Xmin), (int)(B.Ymax - B.Ymin));
            return AR.IntersectsWith(BR);
        }
    }
}
