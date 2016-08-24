
namespace Ada.Framework.Util.FileMonitor
{
    /// <summary>
    /// Factoría del utilitario de archivos.
    /// </summary>
    /// <remarks>
    ///     Registro de versiones:
    ///     
    ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
    /// </remarks>
    public static class MonitorArchivoFactory
    {
        /// <summary>
        /// Obtiene la implementación para el utilitario actual.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <returns>Una implementación de <see>Ada.HardyFramework.Estructura.FileMonitor.IArchivo</see>.</returns>
        public static IMonitorArchivo ObtenerArchivo()
        {
            return new MonitorArchivo();
        }
    }
}
