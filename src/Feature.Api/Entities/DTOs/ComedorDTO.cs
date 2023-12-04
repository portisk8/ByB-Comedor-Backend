using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Api.Entities.DTOs
{
   public class ComedorDTO
    {
        public int? ComedorId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string DireccionCalle { get; set; }
        public int? DireccionNumero { get; set; }
  
    }
}
