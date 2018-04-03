using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore
{
    public class TestController : Controller
    {
        //public TestController(IFoo foo)
        //{

        //}

        public IActionResult Foo()
        {
            return Ok(new { message = "Foo was called" });
        }
    }
}
