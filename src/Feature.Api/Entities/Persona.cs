using Feature.Core.Entities;

namespace Feature.Api.Entities
{
    public class Persona : GrillaBase
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? DocumentoTipoId { get; set; }
        public string DocumentoDescripcion { get; set; }
        public string DocumentoNumero { get; set; }
        public int? ComedorId { get; set; }
        public string ComedorDescripcion { get; set; }
        public string ComedorTitulo { get; set; }
        public string ComedorDireccionCalle { get; set; }
        public int? ComedorDireccionNumero { get; set; }
        public string DireccionCalle { get; set; }
        public int? DireccionNumero { get; set; }
        public int? Edad { get; set; }
        public int? PersonaDetalleId { get; set; }
        public int? PersonaIdTutor { get; set; }
        public string PersonaTutorNombre { get; set; }
        public string PersonaTutorApellido { get; set; }
        public string TelefenoNumero { get; set; }
        public int? SexoTipoId { get; set; }
        public string SexoTipoDescripcion { get; set; }
        public string SexoTipoDescripcionCorta { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool EsInfante { get; set; }
    }
}
