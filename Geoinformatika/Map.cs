using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geoinformatika
{
    public partial class Map : UserControl
    {
        /// <summary>
        /// Масштаб
        /// </summary>
        public double MapScale = 1;
        /// <summary>
        /// Центр карты
        /// </summary>
        public Geopoint MapCenter = new Geopoint(0,0);
        /// <summary>
        /// Слои карты
        /// </summary>
        public List<AbstractLayer> Layers = new List<AbstractLayer>();
        /// <summary>
        /// Тип действия
        /// </summary>
        public MapToolType ActiveTool = MapToolType.SelectObject;
        /// <summary>
        /// Константа манхэтеннского расстояния для начала рисования рамки
        /// </summary>
        const int Shake = 10;
        /// <summary>
        /// Координата позиции нажатия мыши на экран
        /// </summary>
        private System.Drawing.Point mouseDownPosition;
        /// <summary>
        /// Кнопка мыши нажада в данный момент
        /// </summary>
        bool isMouseDown = false;
        /// <summary>
        /// Слой для рисования рамки приближения
        /// </summary>
        public VectorLayer cosmeticLayer = new VectorLayer();
        /// <summary>
        /// Рамка для увеличения
        /// </summary>
        private Polyline drawedPolygon = new Polyline();
        /// <summary>
        /// Границы карты
        /// </summary>
        private double rectSize = 2.0;
        public Polygon RulerPolygon = new Polygon() { Selected = true};
        public LayerControl layerControl;
        public double? LastValue;
        public GeoRect Bounds {

            get
            {
                GeoRect bounds = new GeoRect(0, 0, 0, 0);
                foreach(var item in Layers)
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
            MapCenter = new Geopoint(0,0);
            cosmeticLayer.Name = "Noname";
            this.AddLayer(cosmeticLayer);
            VectorLayer layer = new VectorLayer();
            layer.Name = "Noname";
            layer.Map = this;
            layer.AddObject(new Line(new Geopoint(-200, 0), new Geopoint(200, 0)));
            layer.AddObject(new Line(new Geopoint(0,-200), new Geopoint(0,200)));
            layer.Visible = false;
            List<Geopoint> geopoints = new List<Geopoint>();
            geopoints.Add(new Geopoint(5, 5));
            geopoints.Add(new Geopoint(100,300));
            geopoints.Add(new Geopoint(50, 50));
            layer.AddObject(new Polyline(geopoints));
            geopoints.Clear();
            Polygon polygon = new Polygon();
            polygon.AddNode(new Geopoint(50, 50));
            polygon.AddNode(new Geopoint(50, -50));
            polygon.AddNode(new Geopoint(-50, -50));
            polygon.AddNode(new Geopoint(-50, 50));
            layer.AddObject(polygon);
            layer.AddObject(new Point(250, 30));
            layer.AddObject(new Line(new Geopoint(-150, 25), new Geopoint(-300, 250)));
            this.AddLayer(layer);
        }
        public void AddLayer(AbstractLayer layer)
        {
            layer.Map = this;
            Layers.Add(layer);
            if (layerControl != null)
            {
                layerControl.RefreshList();
            }
        }
        public void RemoveLayer(AbstractLayer layer)
        {
            Layers.Remove(layer);
            if (layerControl != null)
            {
                layerControl.RefreshList();
            }
        }
        public void InsertLayer(AbstractLayer layer, int index)
        {
            layer.Map = this;
            Layers.Insert(index, layer);
            if (layerControl != null)
            {
                layerControl.RefreshList();
            }
        }
        public void DeleteLayerByIndex(int index)
        {
            Layers.RemoveAt(index);
            if (layerControl != null)
            {
                layerControl.RefreshList();
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
            return new System.Drawing.Point(x,y);
        }
        public Geopoint ScreenToMap(System.Drawing.Point screenPoint)
        {
            double x = (screenPoint.X - Width / 2) / MapScale + MapCenter.X;
            double y = (Height / 2 - screenPoint.Y) / MapScale + MapCenter.Y;
            return new Geopoint(x,y);
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            foreach(var layer in Layers)
            {
                if(layer.Visible) layer.Draw(e);
            }
        }


        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPosition = e.Location;
                isMouseDown = true;
            }

        }
        private Polyline polyline = new Polyline();
        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left && isMouseDown)
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

                        }
                        else
                        {
                            cosmeticLayer.DeleteObject(polyline);
                            List<Geopoint> geopoints = new List<Geopoint>();
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X , ScreenToMap(mouseDownPosition).Y));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X + dx, ScreenToMap(mouseDownPosition).Y));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X + dx, ScreenToMap(mouseDownPosition).Y + dy));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X, ScreenToMap(mouseDownPosition).Y + dy));
                            geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X, ScreenToMap(mouseDownPosition).Y));
                            //polyline = new Polyline() {Style =  new LineStyle(wight: 2,color: Color.Green)};
                            polyline.Nodes = geopoints;
                            //cosmeticLayer.DeleteObject(drawedPolygon);
                            cosmeticLayer.AddObject(polyline);
                            //Layers.FirstOrDefault().DeleteObject(drawedPolygon);
                            //Layers.FirstOrDefault().AddObject(polyline);
                        }
                        break;

                    case MapToolType.ZoomOut:
                        break;
                    case MapToolType.SelectObject:
                        break;
                    default:
                        break;
                }
            }
            Refresh();
        }
        Polyline selectedRect = new Polyline();
        /// <summary>
        /// Действия при отпускании кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_MouseUp(object sender, MouseEventArgs e)
        {

                switch (ActiveTool)
                {
                    case MapToolType.Pen:
                        break;
                    case MapToolType.ZoomIn:
                    int dx = ((int)((int)(e.Location.X - mouseDownPosition.X)/MapScale));
                    int dy = (int)((e.Location.Y - mouseDownPosition.Y)/MapScale);
                    if (Math.Abs(dx) < Shake && Math.Abs(dy) < Shake)
                    {
                        MapCenter = ScreenToMap(e.Location);
                        MapScale = MapScale * 1.25;
                    }
                    else
                    {
                        List<Geopoint> geopoints = new List<Geopoint>();
                        //geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X, ScreenToMap(mouseDownPosition).Y));
                        //geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X + dx, ScreenToMap(mouseDownPosition).Y));
                        //geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X + dx, ScreenToMap(mouseDownPosition).Y - dy));
                        //geopoints.Add(new Geopoint(ScreenToMap(mouseDownPosition).X, ScreenToMap(mouseDownPosition).Y - dy));
                        //drawedPolygon = new Polyline();
                        //drawedPolygon.Nodes = geopoints;
                        //cosmeticLayer.DeleteObject(drawedPolygon);
                        cosmeticLayer.DeleteObject(polyline);
                        MapCenter.X = (ScreenToMap(mouseDownPosition).X) + dx / 2;
                        MapCenter.Y = (ScreenToMap(mouseDownPosition).Y) - dy / 2;
                        if (Math.Abs(dx) > Math.Abs(dy)) MapScale = (double)Height / (double)Math.Abs(dy);
                        else MapScale = (double)Width / (double)Math.Abs(dx);
                    }
                    
                        break;
                    case MapToolType.ZoomOut:
                        MapCenter = ScreenToMap(e.Location);
                        MapScale = MapScale / 1.25;
                        break;
                    case MapToolType.SelectObject:
                        if(Math.Abs(e.Location.X - mouseDownPosition.X) < Shake || 
                        Math.Abs(e.Location.Y - mouseDownPosition.Y) < Shake)
                    {
                        if (!cntrlPressed)
                        {
                            foreach(var layer in Layers.Select(_ => _).Where(l => l is VectorLayer).Reverse())
                            {
                                foreach(var obj in  (layer as VectorLayer).objects)
                                {
                                    obj.Selected = false;
                                }
                            }
                        }
                        Geopoint searchCenter = ScreenToMap(e.Location);
                        GeoRect search = new GeoRect(searchCenter.X - rectSize/ MapScale, searchCenter.Y - rectSize/ MapScale, searchCenter.X + rectSize/ MapScale, searchCenter.Y + rectSize/ MapScale);
                       // cosmeticLayer.DeleteObject(selectedRect);
                        //selectedRect = new Polyline();
                        //selectedRect.AddNode(new Geopoint(searchCenter.X - rectSize / MapScale, searchCenter.Y - rectSize / MapScale));
                        //selectedRect.AddNode(new Geopoint(searchCenter.X - rectSize / MapScale, searchCenter.Y + rectSize / MapScale));
                        //selectedRect.AddNode(new Geopoint(searchCenter.X + rectSize / MapScale, searchCenter.Y + rectSize / MapScale));
                        //selectedRect.AddNode(new Geopoint(searchCenter.X + rectSize / MapScale, searchCenter.Y - rectSize / MapScale));
                        //selectedRect.AddNode(new Geopoint(searchCenter.X - rectSize / MapScale, searchCenter.Y - rectSize / MapScale));
                        
                        MapObject searchObject = FindObject(search);
                        if (searchObject != null)
                        {
                            searchObject.Selected = true;
                        }
                        //cosmeticLayer.AddObject(selectedRect);
                        Refresh();
                    }
                        break;
                case MapToolType.Ruler:
                    cosmeticLayer.DeleteObject(RulerPolygon);
                    RulerPolygon.Selected = true;
                    RulerPolygon.AddNode(ScreenToMap(mouseDownPosition));
                    if (RulerPolygon.Nodes.Count > 1)
                    {
                        cosmeticLayer.AddObject(RulerPolygon);
                    }
                    Refresh();
                        break;
                case MapToolType.GetValue:
                    foreach (var layer in Layers.Where(l => l is GridLayer).Select(l => l as GridLayer).Reverse())
                    {
                        var geopoint = ScreenToMap(mouseDownPosition);
                        LastValue = layer.GetValue(geopoint.X, geopoint.Y);
                        break;
                    }
                    break;
                default:
                        break;
                }
                isMouseDown = false;

            Refresh();
        }
        private MapObject FindObject(GeoRect search)
        {
            MapObject result = null;
            for(int i = Layers.Count - 1; i >= 0; i--)
            {
                if (Layers[i] is GridLayer) continue;
                if(Layers[i].Visible) result = (Layers[i] as VectorLayer).FindObject(search);
                if (result != null) return result;
            }
            return result;
        }
        public void ZoomToAll()
        {
            GeoRect currentRect = this.Bounds;
            MapCenter.X = (currentRect.Xmax + currentRect.Xmin) / 2; 
            MapCenter.Y = (currentRect.Ymax + currentRect.Ymin) / 2;
            double dx = currentRect.Xmax - currentRect.Xmin;
            double dy = currentRect.Ymax - currentRect.Ymin;
            MapScale = Math.Min(Math.Min((double)Height / (double)Math.Abs(dy), (double)(Width / (double)Math.Abs(dx))),30);
            //if (Math.Abs(dx) < Math.Abs(dy)) MapScale = (double)Height / (double)Math.Abs(dy) * 0.8;
            //else MapScale = (double)(Width / (double)Math.Abs(dx)) * 0.8;
        }
        bool cntrlPressed = false;
        private void Map_KeyDown(object sender, KeyEventArgs e)
        {
            cntrlPressed = e.Control;
        }
        private void Map_KeyUp(object sender, KeyEventArgs e)
        {
            cntrlPressed = false;
        }
    }
}
