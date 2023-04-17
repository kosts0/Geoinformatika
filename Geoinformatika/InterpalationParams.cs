// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geoinformatika
{
    public class InterpalationParams
    {
        public int Power { get; set; } = 2;
        public SearchType SearchType { get; set; }
        /// <summary>
        /// Радиус поиска при поиске SearchRadius
        /// </summary>
        public double SearchRadius { get; set; }
        /// <summary>
        /// Колчкество точек при поиске NearestCount
        /// </summary>
        public int NearestCount { get; set; }
    }
    /// <summary>
    /// Тип поиска
    /// </summary>
    public enum SearchType
    {
        NearestCount,
        SearchRadius,
    }
}
