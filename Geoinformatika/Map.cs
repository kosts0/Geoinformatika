using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Geoinformatika
{
    public partial class Map : UserControl
    {
        /// <summary>
        /// Масштаб
        /// </summary>
        public double MapScale { get; set; } = 1;
        /// <summary>
        /// Центр карты
        /// </summary>
        public Geopoint MapCenter { get; set; }
        /// <summary>
        /// Слои карты
        /// </summary>
        public List<AbstractLayer> Layers { get; set; } = new List<AbstractLayer>();
        /// <summary>
        /// Тип действия
        /// </summary>
        public MapToolType ActiveTool { get; set; } = MapToolType.SelectObject;
        /// <summary>
        /// Константа манхэтеннского расстояния для начала рисования рамки
        /// </summary>
        const int Shake = 10;
        /// <summary>
        /// Координата позиции нажатия мыши на экран
        /// </summary>
        private System.Drawing.Point mouseDownPosition { get; set; }
        /// <summary>
        /// Кнопка мыши нажада в данный момент
        /// </summary>
        bool IsMouseDown { get; set; }
        /// <summary>
        /// Слой для рисования рамки приближения
        /// </summary>
        public VectorLayer CosmeticLayer { get; set; } = new VectorLayer();
        /// <summary>
        /// Границы карты
        /// </summary>
        private double rectSize = 2.0;
        public Polygon RulerPolygon { get; set; } = new Polygon { Selected = true };
        public LayerControl LayerControl { get; set; }
        public double? LastValue { get; set; }
        public GeoRect LayersBounds
        {

            get
            {
                GeoRect bounds = new GeoRect(0, 0, 0, 0);
                foreach (var item in Layers)
                {
                    if (item.Visible)
                    {
                        bounds = GeoRect.Union(bounds, item.GetBounds);
                    }
                }
                return bounds;
            }
        }
        public Map()
        {
            InitializeComponent();
            MapCenter = new Geopoint(0, 0);
            CosmeticLayer.Name = "Noname";
            this.AddLayer(CosmeticLayer);
            VectorLayer layer = new VectorLayer();
            layer.Name = "Noname";
            this.AddLayer(layer);
            layer.Visible = false;
            //GridLayer gridLayer = new GridLayer(new GridGeometry(0,0,5,10,10));
            //gridLayer.Map = this;
            //gridLayer.Name = "Debug layer";
            //Layers.Add(gridLayer);
            //gridLayer.InterpolateFromPoints(new List<Geopoint>() { new Geopoint(1, 1, 10), new Geopoint(2, 2, -3), new Geopoint(4, 5, 15) }, new InterpalationParams() { SearchType = SearchType.SearchRadius, SearchRadius = 10 });
        }
        public void AddLayer(AbstractLayer layer)
        {
            layer.Map = this;
            Layers.Add(layer);
            if (LayerControl != null)
            {
                LayerControl.RefreshList();
            }
        }
        public void RemoveLayer(AbstractLayer layer)
        {
            Layers.Remove(layer);
            if (LayerControl != null)
            {
                LayerControl.RefreshList();
            }
        }
        public void InsertLayer(AbstractLayer layer, int index)
        {
            layer.Map = this;
            Layers.Insert(index, layer);
            if (LayerControl != null)
            {
                LayerControl.RefreshList();
            }
        }
        public void DeleteLayerByIndex(int index)
        {
            Layers.RemoveAt(index);
            if (LayerControl != null)
            {
                LayerControl.RefreshList();
            }
        }
        /// <summary>
        /// Преобразования в экранную систему координат
        /// </summary>
        /// <param name="geopoint"></param>
        /// <returns></returns>
        public System.Drawing.Point MapToScreen(Geopoint geopoint)
        {
            int x = (int)((geopoint.X - MapCenter.X) * MapScale + Width / 2 + 0.5);
            int y = (int)((MapCenter.Y - geopoint.Y) * MapScale + Height / 2 + 0.5);
            return new System.Drawing.Point(x, y);
        }
        public Geopoint ScreenToMap(System.Drawing.Point screenPoint)
        {
            double x = (screenPoint.X - Width / 2) / MapScale + MapCenter.X;
            double y = (Height / 2 - screenPoint.Y) / MapScale + MapCenter.Y;
            return new Geopoint(x, y);
        }

        public void Map_Paint(object sender, PaintEventArgs e)
        {
            foreach (var layer in Layers)
            {
                if (layer.Visible)
                {
                    layer.Draw(e);
                }
            }
        }


        public void Map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPosition = e.Location;
                IsMouseDown = true;
            }

        }
        private Polyline polyline = new Polyline();
        public void Map_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && IsMouseDown)
            {
                switch (ActiveTool)
                {
                    case MapToolType.Pen:
                        MapCenter.X = MapCenter.X - (e.Location.X - mouseDownPosition.X) / MapScale;
                        MapCenter.Y = MapCenter.Y + (e.Location.Y - mouseDownPosition.Y) / MapScale;
                        mouseDownPosition = e.Location;
                        break;
                    case MapToolType.ZoomIn:
                        double dx = (e.Location.X - mouseDownPosition.X)/MapScale;
                        double dy = (-e.Location.Y + mouseDownPosition.Y)/MapScale;
                        if (Math.Abs(dx) < Shake || Math.Abs(dy) < Shake)
                        {
                            break;
                        }
                        else
                        {
                            CosmeticLayer.DeleteObject(polyline);
                            List<Geopoint> geopoints = new List<Geopoint>();
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X, ScreenToMap(mouseDownPosition).Y));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X + dx, ScreenToMap(mouseDownPosition).Y));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X + dx, ScreenToMap(mouseDownPosition).Y + dy));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X, ScreenToMap(mouseDownPosition).Y + dy));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X, ScreenToMap(mouseDownPosition).Y));
                            polyline.Nodes = geopoints;
                            CosmeticLayer.AddObject(polyline);
                        }
                        break;

                    case MapToolType.ZoomOut:
                        break;
                    case MapToolType.SelectObject:
                        break;
                    case MapToolType.Ruler:
                        break;
                    case MapToolType.GetValue:
                        break;
                    default:
                        throw new NotSupportedException($"Нет реализации для {ActiveTool}");
                }
            }
            Refresh();
        }
        private const double MapScaleCoeficient = 1.25;
        /// <summary>
        /// Действия при отпускании кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Map_MouseUp(object sender, MouseEventArgs e)
        {

            switch (ActiveTool)
            {
                case MapToolType.Pen:
                    break;
                case MapToolType.ZoomIn:
                    MouseUpZoomInAction(e);
                    break;
                case MapToolType.ZoomOut:
                    MapCenter = ScreenToMap(e.Location);
                    MapScale = MapScale / MapScaleCoeficient;
                    break;
                case MapToolType.SelectObject:
                    MouseUpSelectOnjectVaAction(e);
                    break;
                case MapToolType.Ruler:
                    CosmeticLayer.DeleteObject(RulerPolygon);
                    RulerPolygon.Selected = true;
                    RulerPolygon.AddNode(ScreenToMap(mouseDownPosition));
                    if (RulerPolygon.Nodes.Count > 1)
                    {
                        CosmeticLayer.AddObject(RulerPolygon);
                    }
                    Refresh();
                    break;
                case MapToolType.GetValue:
                    var layer = Layers.OfType<GridLayer>().Select(l => l).Reverse().FirstOrDefault();
                    var geopoint = ScreenToMap(mouseDownPosition);
                    LastValue = layer?.GetValue(geopoint.X, geopoint.Y);
                    break;
                default:
                    throw new NotSupportedException($"Нет реализации для {ActiveTool}");
            }
            IsMouseDown = false;

            Refresh();
        }
        public void MouseUpZoomInAction(MouseEventArgs e)
        {
            int dx = ((int)((e.Location.X - mouseDownPosition.X)/MapScale));
            int dy = (int)((e.Location.Y - mouseDownPosition.Y)/MapScale);
            if (Math.Abs(dx) < Shake && Math.Abs(dy) < Shake)
            {
                MapCenter = ScreenToMap(e.Location);
                MapScale = MapScale * MapScaleCoeficient;
            }
            else
            {
                CosmeticLayer.DeleteObject(polyline);
                MapCenter.X = (ScreenToMap(mouseDownPosition).X) + dx / 2;
                MapCenter.Y = (ScreenToMap(mouseDownPosition).Y) - dy / 2;
                MapScale = Math.Abs(dx) > Math.Abs(dy) ? (double)Height / (double)Math.Abs(dy) : (double)Width / (double)Math.Abs(dx);
            }
        }
        public void MouseUpSelectOnjectVaAction(MouseEventArgs e)
        {
            if (Math.Abs(e.Location.X - mouseDownPosition.X) < Shake ||
                    Math.Abs(e.Location.Y - mouseDownPosition.Y) < Shake)
            {
                if (!CntrlPressed)
                {
                    foreach (var layer in Layers.Select(_ => _).Where(l => l is VectorLayer).Reverse())
                    {
                        foreach (var obj in (layer as VectorLayer).Objects)
                        {
                            obj.Selected = false;
                        }
                    }
                }
                Geopoint searchCenter = ScreenToMap(e.Location);
                GeoRect search = new GeoRect(searchCenter.X - rectSize/ MapScale, searchCenter.Y - rectSize/ MapScale, searchCenter.X + rectSize/ MapScale, searchCenter.Y + rectSize/ MapScale);
                MapObject searchObject = FindObject(search);
                if (searchObject != null)
                {
                    searchObject.Selected = true;
                }
                Refresh();
            }
        }
        public MapObject FindObject(GeoRect search)
        {
            MapObject result = null;
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                if (Layers[i] is GridLayer)
                {
                    continue;
                }
                if (Layers[i].Visible)
                {
                    result = (Layers[i] as VectorLayer).FindObject(search);
                }
                if (result != null)
                {
                    return result;
                }
            }
            return result;
        }
        public void ZoomToAll()
        {
            GeoRect currentRect = this.LayersBounds;
            MapCenter.X = (currentRect.Xmax + currentRect.Xmin) / 2;
            MapCenter.Y = (currentRect.Ymax + currentRect.Ymin) / 2;
            double dx = currentRect.Xmax - currentRect.Xmin;
            double dy = currentRect.Ymax - currentRect.Ymin;
            MapScale = Math.Min(Math.Min((double)Height / Math.Abs(dy), (Width / Math.Abs(dx))), 30);
        }
        bool CntrlPressed { get; set; }
        public void Map_KeyDown(object sender, KeyEventArgs e)
        {
            CntrlPressed = e.Control;
        }
        public void Map_KeyUp(object sender, KeyEventArgs e)
        {
            CntrlPressed = false;
        }
    }
}
