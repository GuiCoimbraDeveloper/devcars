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
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        public CarsController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            List<Car> cars = _dbContext.Cars.ToList();
            List<CarItemViewModel> carsViewModel = cars
                .Where(c => c.Status == CarStatusEnum.Available)
                .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
                .ToList();

            return Ok(carsViewModel);
        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Car car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            CarDetailsViewModel carDetailsViewModel = new(
                car.Id,
                car.Brand,
                car.Model,
                car.VinCode,
                car.Year,
                car.Price,
                car.Color,
                car.ProductionDate
              );
            return Ok(carDetailsViewModel);
        }

        // POST api/cars
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddCarInputModels model)
        {
            if (model.Model.Length > 50)
                return BadRequest("Modelo não pode ter mais de 50 caracteres.");

            Car car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, model);
        }

        // PUT api/cars
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateCarInputModels model)
        {
            Car car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            car.Update(model.Color, model.Price);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/cars/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Car car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            car.SetAsSuspended();
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
