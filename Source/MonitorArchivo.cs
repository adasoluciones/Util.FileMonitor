using Ada.Framework.Util.FileMonitor.Exceptions;
using System;
using System.IO;

namespace Ada.Framework.Util.FileMonitor
{
    /// <summary>
    /// Representa un archivo en el sistema.
    /// </summary>
    /// <remarks>
    ///     Registro de versiones:
    ///     
    ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
    /// </remarks>
    internal class MonitorArchivo : IMonitorArchivo
    {
        /// <summary>
        /// Obtiene el directorio de la aplicación actual (la que lo invoca).
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/01/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        public string RutaActual { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        /// <summary>
        /// Obtiene el valor que indica se debe reemplazar por la ruta actual.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        public string RUTA_AUTOMATICA { get { return "[Auto]"; } }

        /// <summary>
        /// Obtiene el valor que indica se debe reemplazar por el separador de carpetas del sistema operativo actual.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 12/01/2016 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        public string SEPARADOR_CARPETAS { get { return "[DS]"; } }

        /// <summary>
        /// Obtiene el valor que indica se debe reemplazar por la ruta actual.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        public string RUTA_ACTUAL { get { return "[RutaActual]"; } }

        /// <summary>
        /// Obtiene el valor que indica se debe reemplazar por en nombre de archivo indicado. 
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 26/11/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        public string NOMBRE_ARCHIVO { get { return "[FileName]"; } }

        /// <summary>
        /// Comprueba si un archivo existe en el sistema de archivos del sistema Windows.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta completa del archivo en el sistema.</param>
        /// <returns><value>true</value> de encontrarlo, o <value>false</value> de lo contrario.</returns>
        public bool Existe(string ruta)
        {
            ruta = ObtenerRutaAbsoluta(ruta);
            return File.Exists(ruta);
        }

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
        public bool FueModificado(DateTime fechaUltimaModificacion, string ruta)
        {
            ruta = ObtenerRutaAbsoluta(ruta);
            if (!Existe(ruta)) throw new ArchivoNoEncontradoException("¡No se encuentra archivo en el sistema!", ruta);
            return fechaUltimaModificacion < ObtenerFechaUltimaModificacion(ruta);
        }

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
        public DateTime ObtenerFechaUltimaModificacion(string ruta)
        {
            ruta = ObtenerRutaAbsoluta(ruta);
            if (!Existe(ruta)) throw new ArchivoNoEncontradoException("¡No se encuentra archivo en el sistema!", ruta);
            return File.GetLastWriteTime(ruta);
        }

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
        public string ObtenerRutaArchivo(string ruta, string nombreArchivo)
        {
            string retorno = ObtenerRutaAbsoluta(ruta);

            if (retorno != null)
            {
                bool agregarNombreArchivo = false;
                if (retorno.Trim().Equals(RUTA_AUTOMATICA, StringComparison.InvariantCultureIgnoreCase))
                {
                    agregarNombreArchivo = true;
                }

                retorno = retorno.Replace(RUTA_AUTOMATICA, AppDomain.CurrentDomain.BaseDirectory);
                retorno = retorno.Replace(NOMBRE_ARCHIVO, nombreArchivo);

                if (agregarNombreArchivo)
                {
                    if (retorno[retorno.Length - 1] != Path.DirectorySeparatorChar)
                    {
                        retorno += Path.DirectorySeparatorChar;
                    }
                    retorno += nombreArchivo;
                }

                while (retorno[retorno.Length - 1] == Path.DirectorySeparatorChar) retorno = retorno.Substring(0, retorno.Length - 1);
            }

            return retorno;
        }

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
        public string ObtenerRutaArchivoExistente(string ruta, string nombreArchivo)
        {
            string retorno = ObtenerRutaArchivo(ruta, nombreArchivo);

            if (!Existe(retorno))
            {
                throw new ArchivoNoEncontradoException(string.Format("¡No se ha encontrado el archivo solicitado en {0}!", retorno), retorno);
            }

            return retorno;
        }

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
        public void ComprobarExistenciaArchivo(string ruta)
        {
            ruta = ObtenerRutaAbsoluta(ruta);
            if (!Existe(ruta))
            {
                throw new ArchivoNoEncontradoException(string.Format("¡El archivo {} no existe!", ruta), ruta);
            }
        }

        /// <summary>
        /// Comprueba la existencia de cada subdirectorio de la ruta. De no existir una carpeta, la crea.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 06/10/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="ruta">Ruta a preparar.</param>
        public void PrepararDirectorio(string ruta)
        {
            ruta = ObtenerRutaAbsoluta(ruta);
            string[] rutaSplit = ruta.Split(Path.DirectorySeparatorChar);
            string rutaAux = rutaSplit[0];
			
            for (int i = 1; i < rutaSplit.Length; i++)
            {
                rutaAux += Path.DirectorySeparatorChar + rutaSplit[i];
                if (rutaSplit[i].Contains(".")) break;
                if (i != 0)
                {
                    DirectoryInfo info = new DirectoryInfo(rutaAux);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                }
            }
        }

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
        public string ObtenerRutaAbsoluta(string rutaReferencial, string ruta)
        {
            string retorno = null;

            if (rutaReferencial != null && ruta != null)
            {
                if (!string.IsNullOrEmpty(Path.GetExtension(rutaReferencial)))
                {
                    rutaReferencial = Path.GetDirectoryName(rutaReferencial);
                }
            }

            if (rutaReferencial != null && ruta != null && ruta.Contains(RUTA_ACTUAL))
            {
                ruta = ruta.Replace(RUTA_ACTUAL, ".");
            }

            if (rutaReferencial == null && ruta != null && !ruta.Contains(RUTA_ACTUAL))
            {
                rutaReferencial = RutaActual;
            }

            if (ruta == null)
            {
                retorno = rutaReferencial;
            }

            if (rutaReferencial == null)
            {
                retorno = ruta;
            }

            if (rutaReferencial != null && ruta != null)
            {
                retorno = Path.Combine(rutaReferencial, ruta);

                retorno = retorno.Replace(RUTA_ACTUAL, RutaActual)
                    .Replace(SEPARADOR_CARPETAS, Path.DirectorySeparatorChar.ToString());

                retorno = Path.GetFullPath(retorno);
                return retorno;
            }

            if (retorno != null)
            {
                retorno = retorno.Replace(RUTA_ACTUAL, RutaActual)
                    .Replace(SEPARADOR_CARPETAS, Path.DirectorySeparatorChar.ToString());
            }
            
            return retorno;
        }

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
        public string ObtenerRutaAbsoluta(string ruta)
        {
            return ObtenerRutaAbsoluta(RutaActual, ruta);
        }

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
        public bool EsDirectorio(string ruta)
        {
            ruta = ObtenerRutaAbsoluta(ruta);
            return File.GetAttributes(ruta) == FileAttributes.Directory;
        }

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
        public bool EsArchivo(string ruta)
        {
            ruta = ObtenerRutaAbsoluta(ruta);
            return File.GetAttributes(ruta) == FileAttributes.Archive;
        }
    }
}
