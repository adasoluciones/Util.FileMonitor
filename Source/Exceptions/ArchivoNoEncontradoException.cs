using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Ada.Framework.Util.FileMonitor.Exceptions
{
    /// <summary>
    /// Clase que representa una excepción al acceder a un archivo que no existe.  Esta clase no puede heredarse.
    /// </summary>
    /// <remarks>
    ///     Registro de versiones:
    ///     
    ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
    /// </remarks>
    [Serializable]
    public sealed class ArchivoNoEncontradoException:Exception
    {
        /// <summary>
        /// Contiene la ruta del archivo no encontrado.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        private string rutaArchivo;

        /// <summary>
        /// Obtiene la ruta del archivo que no ha sido encontrado.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        public string Ruta { get { return rutaArchivo; } }

        /// <summary>
        /// Constructor de la clase que proporciona un mensaje de descripción.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="mensaje">Mensaje de descripción.</param>
        /// <param name="ruta">Ruta del archivo no encontrado.</param>
        public ArchivoNoEncontradoException(string mensaje, string ruta)
            : base(mensaje)
        {
            rutaArchivo = ruta;
        }

        /// <summary>
        /// Constructor de la clase que proporciona un mensaje de descripción, la ruta del archivo y la excepcion específica que la ocacionó.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="mensaje">Mensaje de descripción.</param>
        /// <param name="ruta">Ruta del archivo no encontrado.</param>
        /// <param name="innerException">Excepción que la lazó.</param>
        public ArchivoNoEncontradoException(string mensaje, string ruta, Exception innerException)
            : base(mensaje, innerException) 
        {
            rutaArchivo = ruta;
        }

        /// <summary>
        /// Constructor que recive un serializador y un contexto.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="info">Almacena todos los datos necesarios para serializar o deserializar un objeto.</param>
        /// <param name="context">Describe el origen y destino de una secuencia de serialización determinada y proporciona
        /// un contexto definido por el llamador adicional.</param>
        private ArchivoNoEncontradoException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Cuando se reemplaza en una clase derivada, establece System.Runtime.Serialization.SerializationInfo
        /// con información sobre la excepción.
        /// </summary>
        /// <remarks>
        ///     Registro de versiones:
        ///     
        ///         1.0 02/03/2015 Marcos Abraham Hernández Bravo (Ada Ltda.): versión inicial.
        /// </remarks>
        /// <param name="info">Constructor <see cref="System.Runtime.Serialization.SerializationInfo"/> que contiene los datos serializados del objeto 
        /// que hacen referencia a la excepción que se va a producir.</param>
        /// <param name="context"> Enumeración <see cref="System.Runtime.Serialization.StreamingContext"/> que contiene información
        /// contextual sobre el origen o el destino.</param>
        /// <exception cref="System.ArgumentNullException">Lanzada si info es nulo.</exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info == null) throw new ArgumentNullException("info");
            info.AddValue("Ruta", Ruta);
        }
    }
}
