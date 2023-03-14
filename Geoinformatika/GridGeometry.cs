// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace Geoinformatika
{
    /// <summary>
    /// Сеть
    /// </summary>
    public class GridGeometry
    {
        public GridGeometry(double xmin, double ymin, double cell, int countx, int county)
        {
            Xmin = xmin;
            Ymin = ymin;
            Cell = cell;
            CountX = countx;
            CountY = county;
        }

        public double Cell { get; set; }
        public double Xmin { get; set; }
        public double Ymin { get; set; }
        public int CountX { get; set; }
        public int CountY { get; set; }
        public double Xmax => Xmin + Cell * CountX;
        public double Ymax => Ymin + Cell * CountY;
    }
}
