using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : Controller
    {

        [HttpGet]
        public ActionResult<string> index()
        {
            return "_CoreService.controllers";
        }

    }
}