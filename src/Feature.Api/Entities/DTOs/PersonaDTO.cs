using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Api.Entities.DTOs
{
    public class PersonaDTO
    {
        public int? PersonaId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? DocumentoTipoId { get; set; }
        public string? Documento { get; set; }
        public int? SexoTipoId { get; set; }
        public int? ComedorId { get; set; }
        public int? Edad { get; set; }
        public string DireccionCalle { get; set; }
        public int? DireccionNumero { get; set; }
        public int? PersonaTutorId { get; set; }
        public string TelefonoNumero { get; set; }
        public int? UsuarioId { get; set; }

    }
}
