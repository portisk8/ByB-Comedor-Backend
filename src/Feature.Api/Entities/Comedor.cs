using Feature.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Api.Entities
{
    public class Comedor : GrillaBase
    {
        public int ComedorId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string DireccionCalle { get; set; }
        public int? DireccionNumero { get; set; }
        public int? ComedorDetalleId { get; set; }
        public int? CantidadPersonas { get; set; }
        public DateTime? FechaDeCarga { get; set; }
        public int? UsuarioIdDeCarga { get; set; }
    }
}
