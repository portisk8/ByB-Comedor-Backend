using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Core.AuthUser
{
    public class Membresia
    {
        public int UsuarioId { get; set; }

        public byte[] Salt { get; set; }
        public byte[] Password { get; set; }


        public bool Verificado { get; set; }
        public Guid? Verificador { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimoLogin { get; set; }

        public bool Habilitado { get; set; }
        public bool Bloqueado { get; set; }
        public bool SolicitarCambioPassword { get; set; }

        public string PreguntaSecreta { get; set; }
        public string PreguntaSecretaRespuesta { get; set; }

        public int IntentoFallidoLoginCantidad { get; set; }
        public DateTime IntentoFallidoLoginVentana { get; set; }
        public int IntentoFallidoPreguntaCantidad { get; set; }
        public DateTime IntentoFallidoPreguntaVentana { get; set; }

        public string RecuperarToken { get; set; }
        public DateTime? RecuperarFecha { get; set; }

        public DateTime? FechaUltimaModificacion { get; set; }
        public int? UsuarioIdUltimaModificacion { get; set; }
    }
}
