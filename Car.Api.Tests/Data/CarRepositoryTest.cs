using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Api.Data.Entities;
using Car.Api.Interfaces;

namespace Car.Api.Tests.Data
{
    public class CarRepositoryTest : ICarRepository
    {
        public readonly List<CarModel> InMemoryCollection = new List<CarModel>();

        public Task<IEnumerable<CarModel>> GetAllCars()
        {
            return Task.FromResult(InMemoryCollection.AsEnumerable());
        }

        public Task<CarModel> GetCar(string id)
        {
            return Task.FromResult(InMemoryCollection.FirstOrDefault(x => x.Id == id));
        }

        public Task AddCar(CarModel item)
        {
            InMemoryCollection.Add(item);

            return Task.CompletedTask;
        }

        public Task RemoveCar(string id)
        {
            var dbCar = InMemoryCollection.FirstOrDefault(x => x.Id == id);
            InMemoryCollection.Remove(dbCar);

            return Task.CompletedTask;
        }

        public Task UpdateCar(CarModel model)
        {
            var dbCar = InMemoryCollection.FirstOrDefault(x => x.Id == model.Id);
            InMemoryCollection.Remove(dbCar);

            InMemoryCollection.Add(model);

            return Task.CompletedTask;
        }

        public Task RemoveAllCars()
        {
            InMemoryCollection.Clear();
            return Task.CompletedTask;
        }
    }
}
