﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Core.Controllers
{
    [Authorize]
    public class CoreAuthControllerBase : CoreControllerBase
    {

    }
}
