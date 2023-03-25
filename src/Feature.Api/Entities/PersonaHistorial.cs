using Feature.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Api.Entities
{
    public class PersonaHistorial : GrillaBase
    {
        public int PersonaId { get; set; }
        public int PersonaHistorialId { get; set; }
        public double Altura { get; set; }
        public double? CinturaCircunferencia { get; set; }
        public string Diagnostico { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public double IMC { get; set; }
        public string IMCDescripcion { get; set; }
        
        public DateTime FechaDeCarga { get; set; }

    }
}
