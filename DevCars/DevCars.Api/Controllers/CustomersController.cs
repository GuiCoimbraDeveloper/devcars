using DevCars.Api.Entities;
using DevCars.Api.InputModels;
using DevCars.Api.Persistence;
using DevCars.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly DevCarsDbContext _dbContext;

        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModels model)
        {
            var customer = new Customer(model.FullName, model.Document, model.BirthDate);
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return Ok();
        }

        // POST api/customers/2/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModels model)
        {
            var extraItems = model.ExtraItems.Select(i => new ExtraOrderItem(i.Description, i.Price)).ToList();
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);

            var order = new Order(model.IdCar, model.IdCustomer, car.Price, extraItems);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.IdCustomer, orderid = order.Id }, model);
        }

        // GET api/customers/1/orders/3
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var order = _dbContext.Orders
                .Include(c => c.ExtraItems)
                .SingleOrDefault(o => o.Id == orderid);

            if (order == null)
                return NotFound();


            var extraItems = order.ExtraItems.Select(a => a.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}
