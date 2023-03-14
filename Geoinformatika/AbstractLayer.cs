// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.Windows.Forms;

namespace Geoinformatika
{
    public abstract class AbstractLayer
    {
        /// <summary>
        /// Название слоя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Связь с картой
        /// </summary>
        public Map Map { get; set; }
        private bool visible = true;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                Map.Refresh();
            }

        }
        public abstract void LoadFromFile(string path);
        public abstract void Draw(PaintEventArgs e);
        /// <summary>
        /// Граница слоя
        /// </summary>
        protected GeoRect Bounds = new GeoRect(0, 0, 0, 0);
        public abstract GeoRect GetBounds { get; }
        public override string ToString() => Name ?? "Без имени";
    }
}
