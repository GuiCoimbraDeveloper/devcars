using DevCars.Api.Entities;
using DevCars.Api.InputModels;
using DevCars.Api.Persistence;
using DevCars.Api.ViewModels;
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
        private readonly DevCarsDbContext _dbContext;

        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModels model)
        {
            var customer = new Customer(4, model.FullName, model.Document, model.BirthDate);
            _dbContext.Customers.Add(customer);
            return Ok();
        }

        // POST api/customers/2/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModels model)
        {
            var extraItems = model.ExtraItems.Select(i => new ExtraOrderItem(i.Description, i.Price)).ToList();
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);

            var order = new Order(1, model.IdCar, model.IdCustomer, car.Price, extraItems);

            var customer = _dbContext.Customers.SingleOrDefault(a => a.Id == model.IdCustomer);
            customer.Purchase(order);

            return CreatedAtAction(nameof(GetOrder), new { id = customer.Id, orderid = order.Id }, model);
        }

        // GET api/customers/1/orders/3
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var customer = _dbContext.Customers.SingleOrDefault(a => a.Id == id);

            if (customer == null)
                return NotFound();

            var order = customer.Orders.SingleOrDefault(o => o.Id == orderid);

            var extraItems = order.ExtraItems.Select(a => a.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}
