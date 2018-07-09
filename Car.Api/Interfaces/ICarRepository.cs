using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Api.Data.Entities;

namespace Car.Api.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarModel>> GetAllCars();

        Task<CarModel> GetCar(string id);

        Task AddCar(CarModel item);

        Task RemoveCar(string id);

        Task UpdateCar(CarModel model);

        Task RemoveAllCars();
    }
}
