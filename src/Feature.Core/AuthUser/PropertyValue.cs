using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Core.AuthUser
{
    public class PropertyValue
    {
        public int PropiedadValorId { get; set; }
        public string Nombre { get; set; }
        public int PropiedadId { get; set; }
        public int UsuarioId { get; set; }
        public string Valor { get; set; }
    }
}
