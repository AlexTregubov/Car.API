using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Car.Api.Data.Entities;
using Car.Api.Infrastructure;
using Car.Api.Interfaces;
using Car.Api.Model;

namespace Car.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    [Route("api/cars")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [NoCache]
        [HttpGet]
        // GET api/cars
        public Task<IEnumerable<CarModel>> GetCars()
        {
            return _carService.GetAllCars();
        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public Task<CarModel> GetCar(string id)
        {
            return _carService.GetCar(id);
        }

        // POST api/cars
        [HttpPost]
        public Task<CarModel> AddCar([FromBody] CreateCarModel model)
        {
            return _carService.AddCar(model);
        }

        // PUT api/cars/1
        [HttpPut("{id}")]
        public Task<CarModel> UpdateCar(string id, [FromBody] UpdateCarModel model)
        {
            return _carService.UpdateCar(id, model);
        }

        // DELETE api/cars/1
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _carService.DeleteCar(id);
        }
    }
}
