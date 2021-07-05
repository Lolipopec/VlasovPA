using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Critical;
using Microsoft.Win32;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [STAThread]
        static string Dialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Ввод.csv";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV documents (.csv)|*.csv";
            dlg.ShowDialog();
            return dlg.FileName;
        }
        Critic Cr = new Critic();
        [TestMethod]
        public void TestMethod1()
        {
            var Test = Cr.Input(Dialog());
            Assert.AreEqual(Cr.MaxElem(Test), 8);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var Test = Cr.Input(Dialog());
            Assert.AreEqual(Cr.MinElem(Test), 2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var Test = Cr.Input(Dialog());
            Assert.AreEqual(Cr.LenFunc(Test), 43);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(Cr.s, "");
        }
        [TestMethod]
        public void TestMethod5()
        {
            Assert.IsInstanceOfType(Cr.s, typeof(string));
        }
    }
}
