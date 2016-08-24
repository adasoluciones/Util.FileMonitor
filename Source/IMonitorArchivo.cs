using System;

namespace Ada.Framework.Util.FileMonitor
{
    /// <summary>
    /// Contrato del utilitario de archivos.
    /// </summary>
    /// <remarks>
    ///     Registro de versiones:
    ///     
    ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
    /// </remarks>
    public interface IMonitorArchivo
    {
        /// <summary>
        /// Obtiene el valor que indica que la ruta que debe calcular automáticamente según la ruta actual de la aplicación.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        string RUTA_AUTOMATICA { get; }

        /// <summary>
        /// Obtiene el directorio de la aplicación actual (la que lo invoca).
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/01/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        string RutaActual { get; }

        /// <summary>
        /// Obtiene el valor que indica se debe reemplazar por el separador de carpetas del sistema operativo actual.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 12/01/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        string SEPARADOR_CARPETAS { get; }

        /// <summary>
        /// Obtiene el valor que indica se debe reemplazar por la ruta actual.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        string RUTA_ACTUAL { get; }

        /// <summary>
        /// Obtiene el valor que indica se debe reemplazar por en nombre de archivo indicado. 
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 26/11/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        string NOMBRE_ARCHIVO { get; }
        
        /// <summary>
        /// Comprueba si un archivo existe en el sistema de archivos físico.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta completa del archivo en el sistema.</param>
        /// <returns><value>true</value> de encontrarlo, o <value>false</value> de lo contrario.</returns>
        bool Existe(string ruta);

        /// <summary>
        /// Obtiene la fecha de última modificación del archivo al que representa la instancia actual.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta completa del archivo en el sistema.</param>
        /// <returns>Fecha y hora de última modificación.</returns>
        /// <exception cref="Ada.Framework.Util.FileMonitor.Exceptions.ArchivoNoEncontradoException">Lanzada al no encontrar el archivo en el sistema.</exception>
        DateTime ObtenerFechaUltimaModificacion(string ruta);

        /// <summary>
        /// Comprueba si un archivo a sido modificado, según la fecha de última modificación especificada.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="fechaUltimaModificacion">Fecha de última modificación.</param>
        /// <param name="ruta">Ruta completa del archivo en el sistema.</param>
        /// <returns><value>true</value> de haber sido modificado, o <value>false</value> de lo contrario.</returns>
        /// <exception cref="Ada.Framework.Util.FileMonitor.Exceptions.ArchivoNoEncontradoException">Lanzada al no encontrar el archivo en el sistema.</exception>
        bool FueModificado(DateTime fechaUltimaModificacion, string ruta);

        /// <summary>
        /// Obtiene la ruta de una archivo de manera automática o manual, según el valor de la ruta.
        /// De ser el valor de ruta igual al valor de RUTA_AUTOMATICA, se busca la ruta actual y se añade el nombre del archivo.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/10/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta del archivo.</param>
        /// <param name="nombreArchivo">Nombre del archivo.</param>
        /// <returns>Ruta final del archivo solicitado.</returns>
        string ObtenerRutaArchivo(string ruta, string nombreArchivo);

        /// <summary>
        /// Obtiene la ruta absoluta a partir de una ruta relativa, paramétrica o una combinación de ambas.
        /// </summary>
        /// <example>
        ///     RutaActual = "C:\Ruta\de\ejemplo".
        ///     
        ///     Ruta relativa: 
        ///         ObtenerRutaAbsoluta(RutaActual,"../a.txt");
        ///     Retorno:
        ///         "C:\Ruta\de\a.txt"
        ///     
        ///     Ruta Paramétrica:
        ///         ObtenerRutaAbsoluta(RutaActual,"[RutaActual][DS]a.txt");
        ///     Retorno:
        ///         "C:\Ruta\de\ejemplo\a.txt"
        ///     
        ///     Ruta combinada: 
        ///         ObtenerRutaAbsoluta(RutaActual,"[RutaActual]/../a.txt");
        ///     Retorno:
        ///         "C:\Ruta\de\a.txt"
        ///     
        /// </example>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/10/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="rutaReferencial">Ruta de referencia para realizar la evaluación. Generalmente la ruta actual.</param>
        /// <param name="ruta">Ruta ralativa, paramétrica o una combinación.</param>
        /// <returns>Ruta absoluta solicitada.</returns>
        string ObtenerRutaAbsoluta(string rutaReferencial, string ruta);

        /// <summary>
        /// Obtiene la ruta absoluta a partir de una ruta relativa, paramétrica o una combinación de ambas.
        /// </summary>
        /// <example>
        ///     RutaActual = "C:\Ruta\de\ejemplo".
        ///     
        ///     Ruta relativa: 
        ///         ObtenerRutaAbsoluta("../a.txt");
        ///     Retorno:
        ///         "C:\Ruta\de\a.txt"
        ///     
        ///     Ruta Paramétrica:
        ///         ObtenerRutaAbsoluta("[RutaActual][DS]a.txt");
        ///     Retorno:
        ///         "C:\Ruta\de\ejemplo\a.txt"
        ///     
        ///     Ruta combinada: 
        ///         ObtenerRutaAbsoluta("[RutaActual]/../a.txt");
        ///     Retorno:
        ///         "C:\Ruta\de\a.txt"
        ///     
        /// </example>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/10/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta ralativa, paramétrica o una combinación.</param>
        /// <returns>Ruta absoluta solicitada.</returns>
        string ObtenerRutaAbsoluta(string ruta);

        /// <summary>
        /// Obtiene la ruta de una archivo de manera automática o manual, según el valor de la ruta.
        /// De ser el valor de ruta igual al valor de RUTA_AUTOMATICA, se busca la ruta actual y se añade el nombre del archivo.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/10/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <exception cref="Ada.Framework.Util.FileMonitor.Exceptions.ArchivoNoEncontradoException">Lanzada en caso que no exista el archivo en la ruta final.</exception>
        /// <param name="ruta">Ruta del archivo.</param>
        /// <param name="nombreArchivo">Nombre del archivo.</param>
        /// <returns>Ruta final del archivo solicitado.</returns>
        string ObtenerRutaArchivoExistente(string ruta, string nombreArchivo);

        /// <summary>
        /// Comprueba si un archivo existe. Lanza una excepción si no existe el archivo en la ruta especificada.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/10/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <exception cref="Ada.Framework.Util.FileMonitor.Exceptions.ArchivoNoEncontradoException">Lanzada en caso que no exista el archivo en la ruta especificada.</exception>
        /// <param name="ruta">Ruta del archivo a comprobar.</param>
        void ComprobarExistenciaArchivo(string ruta);
        
        /// <summary>
        /// Comprueba la existencia de cada subdirectorio de la ruta. De no existir una carpeta, la crea.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/10/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta a preparar.</param>
        void PrepararDirectorio(string ruta);

        /// <summary>
        /// Determina si una ruta corresponde a un directorio.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 15/08/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta a verificar.</param>
        /// <returns></returns>
        bool EsDirectorio(string ruta);

        /// <summary>
        /// Determina si una ruta corresponde a un archivo.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 15/08/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta a verificar.</param>
        /// <returns></returns>
        bool EsArchivo(string ruta);
    }
}
