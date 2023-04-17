using NUnit.Extensions.Forms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class TestClass : NUnitFormTest
    {
        //public override Type FormType
        //{
        //    get { return typeof(Geoinformatika.MiniGISSavchenko); }
        //}
        [Test]
        public void Test1()
        {
            CurrentForm = new Geoinformatika.MiniGISSavchenko();
            CurrentForm.Show();
            base.init();
            ControlTester myControl = new ControlTester("map1", CurrentForm);
            using (MouseController mouse = myControl.MouseController())
            {
                mouse.Drag(10, 10, 20, 20);
            }
        }
    }
}
