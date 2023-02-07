﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geoinformatika
{
    public abstract class AbstractLayer
    {
        /// <summary>
        /// Название слоя
        /// </summary>
        public string Name;
        /// <summary>
        /// Связь с картой
        /// </summary>
        public Map Map;
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
        public abstract void LoadFromFile(string fileName);
        public abstract void Draw(PaintEventArgs e);
        /// <summary>
        /// Граница слоя
        /// </summary>
        protected GeoRect Bounds = new GeoRect(0, 0, 0, 0);
        public abstract GeoRect GetBounds { get; }
        public override string ToString() => Name ?? "Без имени";
    }
}