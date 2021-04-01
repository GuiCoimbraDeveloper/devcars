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
    public class CarsController : ControllerBase
    {
        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        // POST api/cars
        [HttpPost]
        public IActionResult Post([FromBody] AddCarInputModels model)
        {
            return Ok();
        }

        // PUT api/cars
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] UpdateCarInputModels model)
        {
            return Ok();
        }

        // DELETE api/cars/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
