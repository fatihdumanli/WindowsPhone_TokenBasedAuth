using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTokenBasedAuth.Controllers
{
    public class HelloController : ApiController
    {

        [Authorize]
        public string Get()
        {
            return "Hello from protected resource.";
        }
    }
}
