using System;

namespace Geoinformatika
{
    public static class CrossHelper
    {
        /// <summary>
        /// Епсилон
        /// </summary>
        const double EPS = double.Epsilon;
        /// <summary>
        /// Проверка пересечения прямых 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool IsCrossLines(Geopoint a, Geopoint b, Geopoint c, Geopoint d)
        {
            double A1 = (a.Y - b.Y);
            double B1 = (b.X - a.X);
            double C1 = (-A1 * a.X - B1 * a.Y);
            double A2 = (c.Y - d.Y);
            double B2 = (d.X - c.X);
            double C2 = (-A2 * c.X - B2 * c.Y);
            double zn = Det(A1, B1, A2, B2);
            if (Math.Abs(zn) > double.Epsilon)
            {
                double x = -Det(C1, B1, C2, B2) * 1.0 / zn;
                double y = -Det(A1, C1, A2, C2) * 1.0 / zn;
                return Between(a.X, b.X, x) && Between(a.Y, b.Y, y)
                    && Between(c.X, d.X, x) && Between(c.Y, d.Y, y);
            }
            else
            {
                return Math.Abs(Det(A1, C1, A2, C2)) <= double.Epsilon && Math.Abs(Det(B1, C1, B2, C2)) <= double.Epsilon
                    && intersect_1(a.X, b.X, c.X, d.X)
                    && intersect_1(a.Y, b.Y, c.Y, d.Y);
            }
        }
        /// <summary>
        /// Определитель матрицы коэфициентов формулы Крамера
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Det(double a, double b, double c, double d)
        {
            return a * d - b * c;
        }
        static bool Between(double a, double b, double c)
        {
            return Math.Min(a, b) <= c + EPS && c <= Math.Max(a, b) + EPS;
        }
        static bool intersect_1(double a, double b, double c, double d)
        {
            if (a > b)
            {
                double cur = a;
                a = b;
                b = cur;
            }
            if (c > d)
            {
                double cur = c;
                c = d;
                d = cur;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        }
    }
}
