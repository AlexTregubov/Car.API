using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Api.Data.Entities;
using Car.Api.Interfaces;
using Car.Api.Model;

namespace Car.Api.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        private readonly IValidator _validator;

        public CarService(
            ICarRepository carRepository,
            IValidator validator)
        {
            _carRepository = carRepository;
            _validator = validator;
        }

        public Task<IEnumerable<CarModel>> GetAllCars()
        {
            return _carRepository.GetAllCars();
        }

        public Task<CarModel> GetCar(string id)
        {
            return _carRepository.GetCar(id);
        }

        public async Task<CarModel> AddCar(CreateCarModel model)
        {
            _validator.Validate(model);

            var dbCarModel = new CarModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                RegistrationNumber = model.RegistrationNumber
            };

            await _carRepository.AddCar(dbCarModel);

            return dbCarModel;
        }

        public async Task<CarModel> UpdateCar(string id, UpdateCarModel model)
        {
            _validator.Validate(model);

            var dbCarModel = await GetCar(id);

            if (model.Name != null)
            {
                dbCarModel.Name = model.Name;
            }

            if (model.Description != null)
            {
                dbCarModel.Description = model.Description;
            }

            if (model.RegistrationNumber != null)
            {
                dbCarModel.RegistrationNumber = model.RegistrationNumber;
            }

            await _carRepository.UpdateCar(dbCarModel);

            return dbCarModel;
        }

        public Task DeleteCar(string id)
        {
            return _carRepository.RemoveCar(id);
        }

        public Task DeleteAllCars()
        {
            return _carRepository.RemoveAllCars();
        }
    }
}
