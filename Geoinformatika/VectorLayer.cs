﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public List<MapObject> Objects { get; set; } = new List<MapObject>();

        public void AddObject(MapObject obj)
        {
            obj.Layer = this;
            Bounds = GeoRect.Union(this.Bounds, obj.Bounds);
            Objects.Add(obj);
        }
        public void DeleteObject(MapObject obj)
        {
            Objects.Remove(obj);
            //Удаление Bounds
        }
        public void InsertObject(MapObject obj, int pos)
        {
            obj.Layer = this;
            Bounds = GeoRect.Union(this.Bounds, obj.Bounds);
            Objects.Insert(pos, obj);
        }
        public void DeleteObjectByIndex(int pos)
        {
            Objects.RemoveAt(pos);
        }

        public override void Draw(PaintEventArgs e)
        {
            foreach (var obj in Objects)
            {
                obj.Draw(e);
            }
        }
        public MapObject FindObject(GeoRect search)
        {
            MapObject result = null;
            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                result = Objects[i].IsCross(search);
                if (result != null)
                {
                    return result;
                }
            }
            return result;
        }
        public override void LoadFromFile(string fileName)
        {
            string fileExt = Path.GetExtension(fileName);
            switch (fileExt)
            {
                case ".csv":
                    UploadCsvFile(fileName);
                    break;
                case ".mif":
                    UploadMifFile(fileName);
                    break;
                default:
                    throw new NotSupportedException($"Нет реализации для {fileName}");
            }
        }
        private void UploadCsvFile(string fileName)
        {
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
                    double x, y, z;
                    try
                    {
                        x = Convert.ToDouble(line.Split(';')[0]);
                        y = Convert.ToDouble(line.Split(';')[1]);
                        z = Convert.ToDouble(line.Split(';')[2]);
                    }
                    catch (System.FormatException)
                    {
                        throw new NotSupportedException("Некорректная csv строка");
                    }
                    points.Add(new Geopoint(x, y, z));
                    AddObject(new Point(new Geopoint(x, y, z)));
                }
            }
        }
        private void UploadMifFile(string fileName)
        {
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
                        if (textFromFile.Split('\n').Length > i + 1
                            && textFromFile.Split('\n')[i + 1].Contains("Symbol"))
                        {
                            currentLine += textFromFile.Split('\n')[i + 1];
                        }
                        AddObject(new Point(currentLine));
                        continue;
                    }
                    if (currentLine.Contains("LINE") && !currentLine.Contains("PLINE"))
                    {
                        if (textFromFile.Split('\n').Length > i + 1 
                            && textFromFile.Split('\n')[i + 1].Contains("PEN")) currentLine += textFromFile.Split('\n')[i + 1];
                        AddObject(new Line(currentLine));
                        continue;
                    }
                    if (currentLine.Contains("PLINE"))
                    {
                        StringBuilder res = new StringBuilder("");
                        res.Append(splited[i])
                            .Append(splited[i+1]);
                        int n = Convert.ToInt32(splited[i + 1]);
                        for (int j = 0; j < n; j++)
                        {
                            res.Append(splited[2 + i + j]);
                        }
                        if (i + n + 2 < splited.Count && splited[i + n + 2].Contains("PEN"))
                        {
                            res.Append(splited[i + n + 2]);
                        }
                        AddObject(new Polyline(res.ToString()));
                    }
                    if (currentLine.Contains("REGION"))
                    {
                        StringBuilder res = new StringBuilder("");
                        res.Append(splited[i]);
                        res.Append(splited[i + 1]);
                        int n = Convert.ToInt32(splited[i + 1]);
                        for (int j = 0; j < n; j++)
                        {
                            res.Append(splited[2 + i + j]);
                        }
                        if (i + n + 2 < splited.Count && (splited[i + n + 2].Contains("PEN") || splited[i + n + 2].Contains("Brush")))
                        {
                            res.Append(splited[i + n + 2]);
                        }
                        if (i + n + 3 < splited.Count && (splited[i + n + 3].Contains("PEN") || splited[i + n + 3].Contains("Brush")))
                        {
                            res.Append(splited[i + n + 3]);
                        }
                        AddObject(new Polygon(res.ToString()));
                    }
                }
            }
        }
    }
}