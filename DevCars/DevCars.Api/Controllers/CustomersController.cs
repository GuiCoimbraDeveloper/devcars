using DevCars.Api.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModels model)
        {
            return Ok();
        }

        // POST api/customers/2/orders
        [HttpPost("{id}")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModels model)
        {
            return Ok();
        }

        // GET api/customers/1/orders/3
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetById(int id, int orderid)
        {
            return Ok();
        }
    }
}
