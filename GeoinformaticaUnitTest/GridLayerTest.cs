using Geoinformatika;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Extensions.Forms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace GeoinformaticaUnitTest
{
    [TestClass]
    public class GridLayerTest : NUnitFormTest
    {
        public override Type FormType
        {
            get { return typeof(MiniGISSavchenko); }
        }
        [TestMethod]
        public void TestMethod1()
        {
            ControlTester myControl = new ControlTester("map1", CurrentForm);
            using (MouseController mouse = myControl.MouseController())
            {
                mouse.Drag(10, 10, 20, 20);
            }
        }
    }
}
