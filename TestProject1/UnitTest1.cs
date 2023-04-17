using NUnit.Extensions.Forms;
using NUnit.Framework;
using System;

namespace TestProject1
{
    public class Tests : NUnitFormTest
    {
        public override Type FormType
        {
            get { return typeof(Geoinformatika.MiniGISSavchenko); }
        }
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void Test1()
        {
            ControlTester myControl = new ControlTester("map1", CurrentForm);
            using (MouseController mouse = myControl.MouseController())
            {
                mouse.Drag(10, 10, 20, 20);
            }
        }
    }
}