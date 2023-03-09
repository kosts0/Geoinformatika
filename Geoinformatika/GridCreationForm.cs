// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Geoinformatika
{
    public partial class GridCreationForm : Form
    {
        public GridCreationForm()
        {
            InitializeComponent();
        }
        public Map Map;
        public InterpalationParams InterpalationParams = new InterpalationParams();
        public GridGeometry GridGeometry = new GridGeometry(0, 0, 0, 0, 0);
        public VectorLayer SelectedLayer => (VectorLayer)comboBoxLayer.SelectedItem;

        private void GridCreationForm_Load(object sender, EventArgs e)
        {
            List<VectorLayer> layers = Map.Layers
                .Where(l => l is VectorLayer)
                .Select(l => l as VectorLayer).ToList();
            comboBoxLayer.Items.Clear();
            comboBoxLayer.Items.AddRange(layers.ToArray());
            comboBoxLayer.SelectedIndex = 1;
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLayer.SelectedItem == null) return;
            VectorLayer layer = (VectorLayer)comboBoxLayer.SelectedItem;
            var bounds = layer.GetBounds;
            XMinTextBox.Text = bounds.Xmin.ToString();
            XMaxTextBox.Text = bounds.Xmax.ToString();
            YMinTextBox.Text = bounds.Ymin.ToString();
            YMaxTextBox.Text = bounds.Ymax.ToString();
            int countX, countY;
            double cell;
            if ((bounds.Xmax - bounds.Xmin) > (bounds.Ymax - bounds.Ymin))
            {
                countX = 100;
                cell = (double)((bounds.Xmax - bounds.Xmin) / (countX - 1));
                countY = (int)((bounds.Ymax - bounds.Ymin) / cell + 1);
            }
            else
            {
                countY = 100;
                cell = (double)((bounds.Ymax - bounds.Ymin) / (countY - 1));
                countX = (int)((bounds.Xmax - bounds.Xmin) / cell + 1);
            }

            CellTextBox.Text = cell.ToString();
            CountX.Text = countX.ToString();
            CountY.Text = countY.ToString();

            int nearestCount = layer.objects.Count;
            if (nearestCount > 20) nearestCount /= 3;

            NearestCountTextBox.Text = nearestCount.ToString();
            double searchRadius = Math.Sqrt(Math.Pow(bounds.Xmax - bounds.Xmin, 2) + Math.Pow(bounds.Ymax - bounds.Ymin, 2)) / 3;
            RadiusSearchTextBox.Text = searchRadius.ToString();

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            InterpalationParams.NearestCount = NearestCountTextBox.Text.ToInt();
            InterpalationParams.SearchRadius = RadiusSearchTextBox.Text.ToDouble();
            InterpalationParams.Power = PowerComboBox.SelectedIndex + 1;

            GridGeometry.Xmin = XMinTextBox.Text.ToDouble();
            GridGeometry.Ymin = YMinTextBox.Text.ToDouble();
            GridGeometry.Cell = CellTextBox.Text.ToDouble();
            GridGeometry.CountX = CountX.Text.ToInt();
            GridGeometry.CountY = CountY.Text.ToInt();
            if (NearestPointsRadioButton.Checked) InterpalationParams.SearchType = SearchType.NearestCount;
            if (SearchRadiusRadiuButton.Checked) InterpalationParams.SearchType = SearchType.SearchRadius;
        }

        private void CellTextBox_TextChanged(object sender, EventArgs e)
        {
            VectorLayer layer = (VectorLayer)comboBoxLayer.SelectedItem;
            if (string.IsNullOrEmpty(CellTextBox.Text)) return;
            double cell = CellTextBox.Text.ToDouble();
            //if (string.IsNullOrEmpty(XMaxTextBox.Text) || string.IsNullOrEmpty(YMaxTextBox.Text)) return;
            int countX = (int)((int)(XMaxTextBox.Text.ToDouble() - XMinTextBox.Text.ToDouble()) / cell);
            int countY = (int)((YMaxTextBox.Text.ToDouble() - XMinTextBox.Text.ToDouble()) / cell);
            CountX.Text = countX.ToString();
            CountY.Text = countY.ToString();
        }

        private void CalculateTimeRadius_Click(object sender, EventArgs e)
        {
            SaveButton_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveButton_Click(sender, e);
            var res = GridLayer.CalculateAccuracy(SelectedLayer.objects.Where(o => o is Point).Select(p => (p as Point).location).ToList());
            string text = "";
            foreach (var result in res)
            {
                text += result.Item1.ToString() + " ->" + result.Item2.ToString() + "\n";
            }
            MessageBox.Show(text);
        }
    }
    public static class Ex
    {
        public static int ToInt(this string str) => Convert.ToInt32(str);
        public static double ToDouble(this string str) => Convert.ToDouble(str);
    }
}

