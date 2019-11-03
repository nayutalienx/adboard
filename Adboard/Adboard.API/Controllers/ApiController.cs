using AdvertPlatform.WebApi.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adboard.API.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected ApiResult ApiResult(object @object) =>
            new ApiResult(@object);
    }
}
