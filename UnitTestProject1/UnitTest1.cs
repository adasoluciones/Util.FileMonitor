using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ada.Framework.Util.FileMonitor;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();

        [TestMethod]
        public void Caso1A_1B()
        {
            Assert.AreEqual("C:\\a\\b\\archivo.txt", monitor.ObtenerRutaAbsoluta("C:\\a\\b\\c", "..\\archivo.txt"));
        }

        [TestMethod]
        public void Caso1A_2B()
        {
            Assert.AreEqual("C:\\a\\b\\c\\A", monitor.ObtenerRutaAbsoluta("C:\\a\\b\\c", "[RutaActual][DS]A"));
        }

        [TestMethod]
        public void Caso1A_3B()
        {
            Assert.AreEqual("C:\\a\\b\\c", monitor.ObtenerRutaAbsoluta("C:\\a\\b\\c", null));
        }
        





        [TestMethod]
        public void Caso2A_1B()
        {
            Assert.AreEqual(@"D:\TyP\AdaFrameworkDotNet\Util\FileMonitor\UnitTestProject1\bin\Debug\archivo.txt", monitor.ObtenerRutaAbsoluta("[RutaActual]\\A", "..\\archivo.txt"));
        }

        [TestMethod]
        public void Caso2A_2B()
        {
            Assert.AreEqual(@"D:\TyP\AdaFrameworkDotNet\Util\FileMonitor\UnitTestProject1\bin\Debug\A\A", monitor.ObtenerRutaAbsoluta("[RutaActual]\\A", "[RutaActual][DS]A"));
        }

        [TestMethod]
        public void Caso2A_3B()
        {
            Assert.AreEqual(@"D:\TyP\AdaFrameworkDotNet\Util\FileMonitor\UnitTestProject1\bin\Debug\A", monitor.ObtenerRutaAbsoluta("[RutaActual]\\A", null));
        }






        [TestMethod]
        public void Caso3A_1B()
        {
            Assert.AreEqual(@"D:\TyP\AdaFrameworkDotNet\Util\FileMonitor\UnitTestProject1\bin\archivo.txt", monitor.ObtenerRutaAbsoluta(null, "..\\archivo.txt"));
        }

        [TestMethod]
        public void Caso3A_2B()
        {
            Assert.AreEqual(@"D:\TyP\AdaFrameworkDotNet\Util\FileMonitor\UnitTestProject1\bin\Debug\A", monitor.ObtenerRutaAbsoluta(null, "[RutaActual][DS]A"));
        }

        [TestMethod]
        public void Caso3A_3B()
        {
            Assert.IsNull(monitor.ObtenerRutaAbsoluta(null, null));
        }
    }
}
