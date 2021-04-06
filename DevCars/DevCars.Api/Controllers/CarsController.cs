using Dapper;
using DevCars.Api.Entities;
using DevCars.Api.InputModels;
using DevCars.Api.Persistence;
using DevCars.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly string _connectionString;
        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            //SE EU UTILIZAR O DB CONTEXT, E UTILIZAR O inmemory vai dar erro
            //_connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            _connectionString = configuration.GetConnectionString("DevCarsCs");
        }

        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            //List<Car> cars = _dbContext.Cars.ToList();
            //List<CarItemViewModel> carsViewModel = cars
            //    .Where(c => c.Status == CarStatusEnum.Available)
            //    .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
            //    .ToList();

            using var sqlConnection = new SqlConnection(_connectionString);
            var query = "SELECT Id, Brand, Model, Price FROM Cars WHERE Status = 0";
            var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);
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
        /// <summary>
        /// Cadastrar um carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///  "brand":"honda",
        ///  "model":"Civic",
        ///  "vincode":"abc123",
        ///  "year":2021
        ///  "color":"branco",
        ///  "productionDate":"2021-04-05
        ///  }
        ///  </remarks>
        ///  <param name="model">Dados de um novo Carro</param>
        ///  <returns>Objeto recem-criado</returns>
        ///  <response code="201">Objeto criado com sucesso.</response>
        ///  <response code="400">Dados invalidos </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Atualizar dados de  um carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///  "price":1000
        ///  "color":"branco",
        ///  }
        ///  </remarks>
        ///  ///  <param name="id">Identificado do carro/param>
        ///  <param name="model">Dados de alteração</param>
        ///  <returns>Não tem retorno</returns>
        ///  <response code="204">Objeto criado com sucesso.</response>
        ///  <response code="404">Carro não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModels model)
        {
            Car car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            car.Update(model.Color, model.Price);
            //await _dbContext.SaveChangesAsync();

            using var sqlConnection = new SqlConnection(_connectionString);
            var query = "UPDATE CArs set Color=@color,Price=@price WHERE Id=id";
            sqlConnection.Execute(query, new { color = model.Color, price = model.Price, id = id });
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
