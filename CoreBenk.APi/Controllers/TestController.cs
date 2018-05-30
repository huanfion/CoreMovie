using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBenk.APi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoreBenk.APi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private ApiDbContext _context;

        public TestController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}