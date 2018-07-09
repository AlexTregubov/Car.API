using MongoDB.Bson.Serialization.Attributes;

namespace Car.Api.Data.Entities
{
    public class CarModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string RegistrationNumber { get; set; } = string.Empty;
    }
}
