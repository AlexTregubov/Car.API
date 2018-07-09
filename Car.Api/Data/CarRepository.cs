using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Api.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using MongoDB.Bson;
using Car.Api.Interfaces;
using Car.Api.Model;

namespace Car.Api.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly CarContext _context;

        public CarRepository(IOptions<Settings> settings)
        {
            _context = new CarContext(settings);
        }

        public async Task<IEnumerable<CarModel>> GetAllCars()
        {
            return await _context.Cars.Find(x => true).ToListAsync();
        }

        public async Task<CarModel> GetCar(string id)
        {
            var filter = Builders<CarModel>.Filter.Eq("Id", id);

            return await _context.Cars
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task AddCar(CarModel item)
        {
            await _context.Cars.InsertOneAsync(item);
        }

        public Task RemoveCar(string id)
        {
            return _context.Cars
                .DeleteOneAsync(Builders<CarModel>.Filter.Eq("Id", id));
        }

        public Task UpdateCar(CarModel model)
        {
            var filter = Builders<CarModel>.Filter.Eq(s => s.Id, model.Id);
            var update = Builders<CarModel>
                .Update
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.RegistrationNumber, model.RegistrationNumber);

            return _context.Cars.UpdateOneAsync(filter, update);
        }

        public Task RemoveAllCars()
        {
            return _context.Cars.DeleteManyAsync(new BsonDocument());
        }
    }
}
