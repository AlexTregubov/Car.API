using Car.Api.Data.Entities;
using Car.Api.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Car.Api.Data
{
    public class CarContext
    {
        private readonly IMongoDatabase _database;

        public CarContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<CarModel> Cars => _database.GetCollection<CarModel>("Car");
    }
}
