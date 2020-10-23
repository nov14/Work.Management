using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Helpers;

namespace Work.Api.Controllers
{
    [ApiController]
    [Route("api/text")]
    [Authorize]
    public class TextController: ControllerBase
    {
        [HttpGet]
        //[Authorize(Permissions.Name)]
        public string Get(string number)
        {
            return "Hello World!"+number;
        }
    }
}
