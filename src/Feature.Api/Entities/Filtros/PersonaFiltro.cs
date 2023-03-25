using Feature.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Api.Entities.Filtros
{
    public class PersonaFiltro : FiltroBase
    {
        public int? PersonaId { get; set; }
        public bool? EsInfante { get; set; }
        public int? ComedorId { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
