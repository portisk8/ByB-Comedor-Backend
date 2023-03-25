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
        public bool? EsInfante { get; set; }
        public int? ComedorId { get; set; }
    }
}
