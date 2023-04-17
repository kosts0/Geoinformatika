using Geoinformatika;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LayerModuleTest
{
    [TestFixture]
    public class LayerTest
    {
        private const string TestDataSource = "TestDataSources";
        /// <summary>
        /// Проверка загрузки слоя из корректного mif файла
        /// </summary>
        [Test]
        public void LoadFromCorrectMifFileTest()
        {
            string layerMifFile = "mifLayer.mif";
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                TestDataSource, layerMifFile);
            VectorLayer layer = new VectorLayer();
            layer.LoadFromFile(filePath);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, layer.Objects.Where(o => o.GetType() == typeof(Point)).Count(),
                    $"Неверное количество объектов типа точка загружено из файла {layerMifFile}");
                Assert.AreEqual(1, layer.Objects.Where(o => o.GetType() == typeof(Polyline)).Count(),
                    $"Неверное количество объектов типа полилиния загружено из фала {layerMifFile}");
                Assert.AreEqual(1, layer.Objects.Where(o => o.GetType() == typeof(Polygon)).Count(),
                    $"Неверное колчество объектов типа полигон загружено из файла {layerMifFile}");
                Assert.AreEqual(1, layer.Objects.Where(o => o.GetType() == typeof(Line)).Count(),
                    $"Неверное количество объектов типа линия загружено из файла {layerMifFile}");
            });
        }
        /// <summary>
        /// Проверка загрузки слоя из корректного mif файла
        /// </summary>
        [Test]
        public void LoadFromUnexistMifFileTest()
        {
            string layerMifFile = "unexistMifLayer.mif";
            string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), TestDataSource, layerMifFile);
            VectorLayer layer = new VectorLayer();
            Assert.Throws(typeof(FileNotFoundException), () => layer.LoadFromFile(filePath), "Некорректная ошибка отсутвия файла");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Point)).Count(), $"Неверное количество объектов типа точка загружено из файла {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polyline)).Count(), $"Неверное количество объектов типа полилиния загружено из фала {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polygon)).Count(), $"Неверное колчество объектов типа полигон загружено из файла {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Line)).Count(), $"Неверное количество объектов типа линия загружено из файла {layerMifFile}");
            });
        }
        /// <summary>
        /// Проверка загрузки слоя из корректного mif файла
        /// </summary>
        [Test]
        public void LoadFromIncorrectMifFileTest()
        {
            string layerMifFile = "incorrectMifLayer.mif";
            string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), TestDataSource, layerMifFile);
            VectorLayer layer = new VectorLayer();
            Assert.Throws(typeof(NotSupportedException), () => layer.LoadFromFile(filePath), "Некорректная ошибка");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, layer.Objects.Where(o => o.GetType() == typeof(Point)).Count(), $"Неверное количество объектов типа точка загружено из файла {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polyline)).Count(), $"Неверное количество объектов типа полилиния загружено из фала {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polygon)).Count(), $"Неверное колчество объектов типа полигон загружено из файла {layerMifFile}");
                Assert.AreEqual(1, layer.Objects.Where(o => o.GetType() == typeof(Line)).Count(), $"Неверное количество объектов типа линия загружено из файла {layerMifFile}");
            });
        }
        [Test]
        public void LoadFromCorrectCsvFile()
        {
            string layerCsvFile = "points3d.csv";
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TestDataSource, layerCsvFile);
            VectorLayer layer = new VectorLayer();
            layer.LoadFromFile(filePath);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1953, layer.Objects.Where(o => o.GetType() == typeof(Point)).Count(),
    $"Неверное количество объектов типа точка загружено из файла {layerCsvFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polyline)).Count(),
                    $"Неверное количество объектов типа полилиния загружено из фала {layerCsvFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polygon)).Count(),
                    $"Неверное колчество объектов типа полигон загружено из файла {layerCsvFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Line)).Count(),
                    $"Неверное количество объектов типа линия загружено из файла {layerCsvFile}");

            });
        }
        /// <summary>
        /// Проверка загрузки слоя из корректного csv файла
        /// </summary>
        [Test]
        public void LoadFromUnexistCsvFileTest()
        {
            string layerMifFile = "unexistCsvLayer.csv";
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TestDataSource, layerMifFile);
            VectorLayer layer = new VectorLayer();
            Assert.Throws(typeof(FileNotFoundException), () => layer.LoadFromFile(filePath), "Некорректная ошибка отсутвия файла");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Point)).Count(), $"Неверное количество объектов типа точка загружено из файла {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polyline)).Count(), $"Неверное количество объектов типа полилиния загружено из фала {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polygon)).Count(), $"Неверное колчество объектов типа полигон загружено из файла {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Line)).Count(), $"Неверное количество объектов типа линия загружено из файла {layerMifFile}");
            });
        }
        /// <summary>
        /// Проверка загрузки слоя из корректного mif файла
        /// </summary>
        [Test]
        public void LoadFromIncorrectCsvFileTest()
        {
            string layerMifFile = "points3dIncorrect.csv";
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TestDataSource, layerMifFile);
            VectorLayer layer = new VectorLayer();
            Assert.Throws(typeof(NotSupportedException), () => layer.LoadFromFile(filePath), "Некорректная ошибка");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, layer.Objects.Where(o => o.GetType() == typeof(Point)).Count(), $"Неверное количество объектов типа точка загружено из файла {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polyline)).Count(), $"Неверное количество объектов типа полилиния загружено из фала {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Polygon)).Count(), $"Неверное колчество объектов типа полигон загружено из файла {layerMifFile}");
                Assert.AreEqual(0, layer.Objects.Where(o => o.GetType() == typeof(Line)).Count(), $"Неверное количество объектов типа линия загружено из файла {layerMifFile}");
            });
        }
        public static MapObject[] MapObjectsSource =
        {
            new Point(new Geopoint(1,1)),
            new Point(new Geopoint(1,1,1)),
            new Line(begin: new Geopoint(1,1),end: new Geopoint(2,2)),
            new Polyline(new List<Geopoint>(){ new Geopoint(1,1), new Geopoint(2,2), new Geopoint(3, 1) }),
            new Polygon(new List<Geopoint>(){ new Geopoint(1,1), new Geopoint(2,2), new Geopoint(3, 1) })
        };
        /// <summary>
        /// Проверка добавления объектов
        /// </summary>
        /// <param name="mapObject">Массив объектов</param>
        [Test, TestCaseSource(nameof(MapObjectsSource))]
        public void AddObjectToLayer(MapObject mapObject)
        {
            VectorLayer layer = new VectorLayer();
            layer.AddObject(mapObject);
            Assert.AreEqual(1, layer.Objects.Where(o => o.GetType() == mapObject.GetType()).Count(),
                $"Неверное количество объектов типа {mapObject.GetType()}");
            Assert.AreEqual(layer.GetBounds.Xmin, mapObject.Bounds.Xmin,
                $"Неверная рамка для слоя с объектом {mapObject.GetType()}, Xmin");
            Assert.AreEqual(layer.GetBounds.Xmax, mapObject.Bounds.Xmax,
                $"Неверная рамка для слоя с объектом {mapObject.GetType()}, Xmax");
            Assert.AreEqual(layer.GetBounds.Ymin, mapObject.Bounds.Ymin,
                $"Неверная рамка для слоя с объектом {mapObject.GetType()}, Ymin");
            Assert.AreEqual(layer.GetBounds.Ymax, mapObject.Bounds.Ymax,
                $"Неверная рамка для слоя с объектом {mapObject.GetType()}, Ymax");
        }
        /// <summary>
        /// Проверка удаления объектов из слоя
        /// </summary>
        [Test]
        public void RemoveObjectsFromLayer()
        {
            VectorLayer layer = new VectorLayer();
            GeoRect bounds = new GeoRect(1,1,1,1);
            foreach(var obj in MapObjectsSource)
            {
                layer.AddObject(obj);
                bounds = GeoRect.Union(bounds, obj.Bounds);
            }
            ValidateBouds(layer.GetBounds, bounds);
            
            for(int i=0; i < MapObjectsSource.Length; i++){
                bounds = new GeoRect(1, 1, 1, 1);
                for (int j = 0; j<MapObjectsSource.Length - i; j++)
                {
                    bounds = GeoRect.Union(bounds, MapObjectsSource[i].Bounds);
                }
                layer.DeleteObject(MapObjectsSource[MapObjectsSource.Length - 1 - i]);
            }
        }
        /// <summary>
        /// Вспомогательный метод проверки рамок геополя
        /// </summary>
        public static void ValidateBouds(GeoRect bounds1, GeoRect bounds2)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(bounds1.Xmin, bounds2.Xmin, "Неверная рамка слоя, xMin");
                Assert.AreEqual(bounds1.Xmax, bounds2.Xmax, "Неверная рамка слоя, xMax");
                Assert.AreEqual(bounds1.Ymin, bounds2.Ymin, "Неверная рамка слоя, Ymin");
                Assert.AreEqual(bounds1.Ymax, bounds2.Ymax, "Неверная рамка слоя, yMax");
            });
        }
        /// <summary>
        /// Проверка генерации геополя из векторного слоя (метод ближайших точек)
        /// </summary>
        [Test]
        public void GridLayerLoadNearestCount()
        {
            string points3dLayerFileName = "points3d.csv";
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TestDataSource, points3dLayerFileName);
            VectorLayer points3dLayer = new VectorLayer();
            points3dLayer.LoadFromFile(filePath);
            GridLayer gridLayer = new GridLayer(new GridGeometry(0, 0, 0, 0, 0));
            Assert.DoesNotThrow(() => gridLayer.InterpolateFromPoints(points3dLayer, new InterpalationParams()
            {
                SearchType = SearchType.NearestCount,
                NearestCount = points3dLayer.Objects.Count()/3,
            }), $"Не удалось построить геосеть по векторному слою из файла {points3dLayerFileName}");
            //Assert.IsTrue(gridLayer.GetBounds.IsExist, $"Некорректно заданы границы у слоя GridLayer после интерполяции из векторного слоя");
        }
        /// <summary>
        /// Проверка генерации геополя из векторного слоя (метод радиуса поиска)
        /// </summary>
        [Test]
        public void GridLayerLoadSearchRadius()
        {
            string points3dLayerFileName = "points3d.csv";
            string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), TestDataSource, points3dLayerFileName);
            VectorLayer points3dLayer = new VectorLayer();
            points3dLayer.LoadFromFile(filePath);
            GridLayer gridLayer = new GridLayer(new GridGeometry(0, 0, 0, 0, 0));
            Assert.DoesNotThrow(() => gridLayer.InterpolateFromPoints(points3dLayer, new InterpalationParams()
            {
                SearchType = SearchType.SearchRadius,
                SearchRadius =  Math.Abs((points3dLayer.GetBounds.Xmax - points3dLayer.GetBounds.Xmin))/10,
            }), $"Не удалось построить геосеть по векторному слою из файла {points3dLayerFileName}");
            //Assert.IsTrue(gridLayer.GetBounds.IsExist, $"Некорректно заданы границы у слоя GridLayer после интерполяции из векторного слоя");
        }
    }
}