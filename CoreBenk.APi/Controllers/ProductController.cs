using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBenk.APi.Dtos;
using CoreBenk.APi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreBenk.APi.Controllers
{
    public class ProductController : Controller
    {
        [Route("api/[controller]")]
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(ProductService.Current.Products);
        }

        [Route("api/[controller]/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product=ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}