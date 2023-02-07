using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geoinformatika
{
    public partial class MiniGISSavchenko : Form
    {
        public MiniGISSavchenko()
        {
            InitializeComponent();
            layerControl1.map = map1;
            map1.layerControl = layerControl1;
            layerControl1.RefreshList();
        }
        private void Pan_Click(object sender, EventArgs e)
        {

                map1.cosmeticLayer.DeleteObject(map1.RulerPolygon);
                RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
            map1.ActiveTool = MapToolType.Pen;
            Pan.BackColor = Color.Red;
            ZoomIn.BackColor = Color.White;
            ZoomOut.BackColor = Color.White;
            Center.BackColor = Color.White;
            SelectObject.BackColor = Color.White;
            map1.Cursor = Cursors.Hand;
            RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {

                map1.cosmeticLayer.DeleteObject(map1.RulerPolygon);
                RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
            map1.ActiveTool = MapToolType.ZoomIn;
            Pan.BackColor = Color.White;
            ZoomIn.BackColor = Color.Red;
            ZoomOut.BackColor = Color.White;
            Center.BackColor = Color.White;
            SelectObject.BackColor = Color.White;
            map1.Cursor = Cursors.Default;
            RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {

                map1.cosmeticLayer.DeleteObject(map1.RulerPolygon);
                RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
            map1.ActiveTool = MapToolType.ZoomOut;
            Pan.BackColor = Color.White;
            ZoomIn.BackColor = Color.White;
            ZoomOut.BackColor = Color.Red;
            Center.BackColor = Color.White;
            SelectObject.BackColor = Color.White;
            map1.Cursor = Cursors.Default;
            RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
        }

        private void Center_Click(object sender, EventArgs e)
        {

                map1.cosmeticLayer.DeleteObject(map1.RulerPolygon);
                RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
            Pan.BackColor = Color.White;
            ZoomIn.BackColor = Color.White;
            ZoomOut.BackColor = Color.White;
            Center.BackColor = Color.Red;
            SelectObject.BackColor = Color.White;
            map1.Cursor = Cursors.Default;
            RulerButton.BackColor = Color.White;
            map1.ZoomToAll();
            map1.RulerPolygon = new Polygon();
        }

        private void SelectObject_Click(object sender, EventArgs e)
        {

                map1.cosmeticLayer.DeleteObject(map1.RulerPolygon);
                RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
            map1.ActiveTool = MapToolType.SelectObject;
            Pan.BackColor = Color.White;
            ZoomIn.BackColor = Color.White;
            ZoomOut.BackColor = Color.White;
            Center.BackColor = Color.White;
            SelectObject.BackColor = Color.Red;
            map1.Cursor = Cursors.Default;
            RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
        }

        private void map1_MouseMove(object sender, MouseEventArgs e)
        {
            
            Geopoint geopoint = map1.ScreenToMap(e.Location);
            toolStripStatusLabel2.Text = $"Координаты: {geopoint.X} {geopoint.Y}";
            toolStripStatusLabel3.Text = $"Текущий масштаб: {map1.MapScale}";
        }

        private void RulerButton_Click(object sender, EventArgs e)
        {
            map1.ActiveTool = MapToolType.Ruler;
            Pan.BackColor = Color.White;
            ZoomIn.BackColor = Color.White;
            ZoomOut.BackColor = Color.White;
            SelectObject.BackColor = Color.White;
            if(RulerButton.BackColor == Color.Red)
            {
                map1.cosmeticLayer.DeleteObject(map1.RulerPolygon);
                RulerButton.BackColor = Color.White;
                map1.RulerPolygon = new Polygon();
            }
            else
            {
                RulerButton.BackColor = Color.Red;
            }
            
            map1.Cursor = Cursors.Default;
            map1.RulerPolygon = new Polygon();

        }

        private void LoadLayer_Click(object sender, EventArgs e)
        {

                map1.cosmeticLayer.DeleteObject(map1.RulerPolygon);
                RulerButton.BackColor = Color.White;
            map1.RulerPolygon = new Polygon();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName != null)
                {
                    try
                    {
                        VectorLayer layer = new VectorLayer();
                        layer.Name = Regex.Match(openFileDialog1.FileName, @"\\(\w*).mif").Groups[1].Value;
                        if(string.IsNullOrEmpty(layer.Name)) layer.Name = Regex.Match(openFileDialog1.FileName, @"\\(\w*).csv").Groups[1].Value;
                        layer.LoadFromFile(openFileDialog1.FileName);
                        
                        map1.AddLayer(layer);
                        map1.RemoveLayer(map1.cosmeticLayer);
                        map1.AddLayer(map1.cosmeticLayer);
                        map1.ZoomToAll();
                        map1.Refresh();
                        layerControl1.RefreshList();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Ошибка открытия файла ", exception.Message);
                    }
                }
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        /// <summary>
        /// Кнопка открытия диалога выбора минималнього цвета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Color color;
            if (minColorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            color = minColorDialog.Color;
            foreach (var layer in map1.Layers.Where(l => l is GridLayer).Select(l => l as GridLayer))
            {
                layer.colorMin = color;
            }
            Invalidate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            map1.ActiveTool = MapToolType.GetValue;
        }

        private void map1_MouseUp(object sender, MouseEventArgs e)
        {
            if(map1.ActiveTool == MapToolType.GetValue)
            {
                toolStripStatusLabel5.Text = $"Значение геополя: {map1.LastValue}";
            }
        }

        private void CalcGrid_Click(object sender, EventArgs e)
        {
            InterpolatioGrid();

        }
        private void InterpolatioGrid()
        {
            string filePath = @"D:\TPU\Geoinformatika\Geoinformatika (28.02.2022)\Geoinformatika (1)\Geoinformatika\Geoinformatika\interpolationTimes.txt";
            GridCreationForm gridCreationForm = new GridCreationForm();
            gridCreationForm.Map = map1;
            //if (gridCreationForm.ShowDialog() != DialogResult.OK || DialogResult != DialogResult.Yes) return;
            switch (gridCreationForm.ShowDialog())
            {
                case DialogResult.OK:
                    GridLayer gridLayer = new GridLayer(gridCreationForm.GridGeometry);
                    gridLayer.InterpolateFromPoints(gridCreationForm.SelectedLayer, gridCreationForm.InterpalationParams);
                    gridLayer.Name = $"Grid from {gridCreationForm.SelectedLayer.Name}";
                    map1.AddLayer(gridLayer as AbstractLayer);
                break;
                case DialogResult.Yes:
                    switch (gridCreationForm.InterpalationParams.SearchType)
                    {
                        
                        case SearchType.SearchRadius:
                            filePath = @"D:\TPU\Geoinformatika\Geoinformatika (28.02.2022)\Geoinformatika (1)\Geoinformatika\Geoinformatika\interpolationTimes.txt";
                            double maxR = Math.Min(gridCreationForm.GridGeometry.Xmax - gridCreationForm.GridGeometry.Xmin,
                            gridCreationForm.GridGeometry.Ymax - gridCreationForm.GridGeometry.Ymin) / 2;
                            double minR = maxR / 20;
                            double dt = (maxR - minR) / 30;
                            for (double radius = minR; radius <= maxR; radius += dt)
                            {
                                
                                GridLayer gridLayerWithRadius = new GridLayer(gridCreationForm.GridGeometry);
                                gridCreationForm.InterpalationParams.SearchRadius = radius;
                                TimeSpan startTime = DateTime.Now.TimeOfDay;
                                gridLayerWithRadius.InterpolateFromPoints(gridCreationForm.SelectedLayer, gridCreationForm.InterpalationParams);
                                using (StreamWriter writer = new StreamWriter(filePath, true))
                                {
                                    writer.WriteLine($"{radius}; {(DateTime.Now.TimeOfDay - startTime).TotalMilliseconds}");
                                }
                                gridLayerWithRadius.Name = $"r {radius} Grid from {gridCreationForm.SelectedLayer.Name}";
                                gridLayerWithRadius.Map = map1;
                                gridLayerWithRadius.Visible = false;
                                map1.AddLayer(gridLayerWithRadius as AbstractLayer);
                            }
                            break;
                        case SearchType.NearestCount:
                            filePath = @"D:\TPU\Geoinformatika\Geoinformatika (28.02.2022)\Geoinformatika (1)\Geoinformatika(29.03.2022)\Geoinformatika\interpolationPointsTimes.txt";
                            
                            List<Geoinformatika.Geopoint> points = gridCreationForm.SelectedLayer.objects.Where(o => o is Point).Select(p => (p as Point).location).ToList();
                            int maxNearestCount = points.Count;
                            int minNearestCount = maxNearestCount / 50;
                            int step = (maxNearestCount - minNearestCount) / 20;
                            for(int pointsCount = minNearestCount; pointsCount <= maxNearestCount; pointsCount+= step)
                            {
                                
                                GridLayer gridLayerWithPoints = new GridLayer(gridCreationForm.GridGeometry);
                                gridCreationForm.InterpalationParams.NearestCount = pointsCount;
                                TimeSpan startTime = DateTime.Now.TimeOfDay;
                                gridLayerWithPoints.InterpolateFromPoints(points, gridCreationForm.InterpalationParams);
                                using (StreamWriter writer = new StreamWriter(filePath, true))
                                {
                                    writer.WriteLine($"{pointsCount}; {(DateTime.Now.TimeOfDay - startTime).TotalMilliseconds}");
                                }
                                gridLayerWithPoints.Name = $"p {pointsCount} Grid from {gridCreationForm.SelectedLayer.Name}";
                                gridLayerWithPoints.Map = map1;
                                gridLayerWithPoints.Visible = false;
                                map1.AddLayer(gridLayerWithPoints as AbstractLayer);
                                
                            }
                            break;
                    }
                    break;
                default:
                    return;
            }
            
        }
    }
}
