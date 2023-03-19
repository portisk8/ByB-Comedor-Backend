using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Core.Enums
{
    public enum UserPropertyEnum : int
    {
        [Description("Ultima entidad/organismo donde se logeo el usuario o cambio la misma desde la UI")]
        EntidadActualId = 1

    }
}
