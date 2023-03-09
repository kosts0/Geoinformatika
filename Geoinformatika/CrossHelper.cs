// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System;

namespace Geoinformatika
{
    public class CrossHelper
    {
        /// <summary>
        /// Епсилон
        /// </summary>
        const double EPS = 1E-9;
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
            double zn = det(A1, B1, A2, B2);
            if (zn != 0)
            {
                double x = -det(C1, B1, C2, B2) * 1.0 / zn;
                double y = -det(A1, C1, A2, C2) * 1.0 / zn;
                return between(a.X, b.X, x) && between(a.Y, b.Y, y)
                    && between(c.X, d.X, x) && between(c.Y, d.Y, y);
            }
            else
                return det(A1, C1, A2, C2) == 0 && det(B1, C1, B2, C2) == 0
                    && intersect_1(a.X, b.X, c.X, d.X)
                    && intersect_1(a.Y, b.Y, c.Y, d.Y);
        }
        /// <summary>
        /// Определитель матрицы коэфициентов формулы Крамера
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double det(double a, double b, double c, double d)
        {
            return a * d - b * c;
        }
        static bool between(double a, double b, double c)
        {
            return Math.Min(a, b) <= c + EPS && c <= Math.Max(a, b) + EPS;
        }
        static bool intersect_1(double a, double b, double c, double d)
        {
            if (a > b)
            {//swap(a, b);
                double cur = a;
                a = b;
                b = cur;
            }
            if (c > d)
            { //swap(c, d);
                double cur = c;
                c = d;
                d = cur;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        }
    }
}
