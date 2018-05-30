using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBenk.APi.Dtos;
using CoreBenk.APi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreBenk.APi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private ILogger<ProductController> _logger;
        private readonly IMailService _localMailService;
        public ProductController(ILogger<ProductController> logger, IMailService localMailService)
        {
            _logger = logger;
            _localMailService=localMailService;
    }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(ProductService.Current.Products);
        }

        [Route("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product=ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                _logger.LogInformation($"Id为{id}的产品没有被找到..");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreation product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxId = ProductService.Current.Products.Max(x => x.Id);
            var newProduct = new ProductDto
            {
                Id = ++maxId,
                Name = product.Name,
                Price = product.Price
            };
            ProductService.Current.Products.Add(newProduct);

            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        [HttpPut]
        public IActionResult Put(int id,[FromBody] ProductModification product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            model.Name = product.Name;
            model.Price = product.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model= ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            ProductService.Current.Products.Remove(model);
            _localMailService.Send("Product Deleted", $"Id为{id}的产品被删除了");
            return NoContent();
        }
    }
}