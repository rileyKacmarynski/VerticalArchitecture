using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok("here are all the products.");
        }
    }
}
