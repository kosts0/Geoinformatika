using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geoinformatika
{
    /// <summary>
    /// Слой
    /// </summary>
    public class VectorLayer : AbstractLayer
    {
        public override GeoRect GetBounds => Bounds;
        /// <summary>
        /// Объекты слоя
        /// </summary>
        public List<MapObject> objects = new List<MapObject>();

        public void AddObject(MapObject obj)
        {
            obj.layer = this;
            Bounds = GeoRect.Union(this.Bounds, obj.Bounds);
            objects.Add(obj);
        }
        public void DeleteObject(MapObject obj)
        {
            objects.Remove(obj);
            //Удаление Bounds
        }
        public void InsertObject(MapObject obj, int pos)
        {
            obj.layer = this;
            Bounds = GeoRect.Union(this.Bounds, obj.Bounds);
            objects.Insert(pos, obj);
        }
        public void DeleteObjectByIndex(int pos)
        {
            objects.RemoveAt(pos);
        }

        public override void Draw(PaintEventArgs e)
        {
            foreach (var obj in objects)
            {
                obj.Draw(e);
            }
        }
        public MapObject FindObject(GeoRect search)
        {
            MapObject result = null;
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                result = objects[i].IsCross(search);
                if (result != null) return result;
            }
            return result;
        }
        public override void LoadFromFile(string fileName)
        {
            string fileExt = Path.GetExtension(fileName);
            switch (fileExt)
            {
                case ".csv":
                    using (FileStream fstream = File.OpenRead(fileName))
                    {
                        byte[] array = new byte[fstream.Length];

                        fstream.Read(array, 0, array.Length);

                        string textFromFile = System.Text.Encoding.Default.GetString(array);
                        List<string> splited = textFromFile.Split('\r').ToList();
                        List<Geopoint> points = new List<Geopoint>();
                        splited.RemoveAt(splited.Count - 1);
                        foreach (string line in splited)
                        {
                            double x = Convert.ToDouble(line.Split(';')[0]);
                            double y = Convert.ToDouble(line.Split(';')[1]);
                            double z = Convert.ToDouble(line.Split(';')[2]);
                            points.Add(new Geopoint(x, y, z));
                            AddObject(new Point(new Geopoint(x, y, z)));
                        }
                    }
                    break;
                case ".mif":
                    using (FileStream fstream = File.OpenRead(fileName))
                    {
                        byte[] array = new byte[fstream.Length];

                        fstream.Read(array, 0, array.Length);

                        string textFromFile = System.Text.Encoding.Default.GetString(array);
                        List<string> splited = textFromFile.Split('\n').ToList();
                        for (int i = 0; i < textFromFile.Split('\n').Length; i++)
                        {
                            string currentLine = textFromFile.Split('\n')[i];
                            if (currentLine.Contains("POINT"))
                            {
                                if (textFromFile.Split('\n').Length > i + 1 && textFromFile.Split('\n')[i + 1].Contains("Symbol")) currentLine += textFromFile.Split('\n')[i + 1];
                                AddObject(new Point(currentLine));
                                continue;
                            }
                            if (currentLine.Contains("LINE") && !currentLine.Contains("PLINE"))
                            {
                                if (textFromFile.Split('\n').Length > i + 1 && textFromFile.Split('\n')[i + 1].Contains("PEN")) currentLine += textFromFile.Split('\n')[i + 1];
                                AddObject(new Line(currentLine));
                                continue;
                            }
                            if (currentLine.Contains("PLINE"))
                            {
                                string res = splited[i];
                                res += splited[i + 1];
                                int n = Convert.ToInt32(splited[i + 1]);
                                for (int j = 0; j < n; j++)
                                {
                                    res += splited[2 + i + j];
                                }
                                if (i + n + 2 < splited.Count && splited[i + n + 2].Contains("PEN"))
                                {
                                    res += splited[i + n + 2];
                                }
                                AddObject(new Polyline(res));
                            }
                            if (currentLine.Contains("REGION"))
                            {
                                string res = splited[i];
                                res += splited[i + 1];
                                int n = Convert.ToInt32(splited[i + 1]);
                                for (int j = 0; j < n; j++)
                                {
                                    res += splited[2 + i + j];
                                }
                                if (i + n + 2 < splited.Count && (splited[i + n + 2].Contains("PEN") || splited[i + n + 2].Contains("Brush")))
                                {
                                    res += splited[i + n + 2];
                                }
                                if (i + n + 3 < splited.Count && (splited[i + n + 3].Contains("PEN") || splited[i + n + 3].Contains("Brush")))
                                {
                                    res += splited[i + n + 3];
                                }
                                AddObject(new Polygon(res));
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        // 2 файла, 1 координатная система
        // 2 с объектами разных типов
        //отображаем только один,
        //можно делать вручную, а можно программно делать запись
        //сплитим по строкам, смотрим на ключевое слово первое и рисуем потом

    }

}