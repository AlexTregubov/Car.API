using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Api.Data.Entities;
using Car.Api.Model;

namespace Car.Api.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarModel>> GetAllCars();

        Task<CarModel> GetCar(string id);

        Task<CarModel> AddCar(CreateCarModel model);

        Task<CarModel> UpdateCar(string id, UpdateCarModel model);

        Task DeleteCar(string id);

        Task DeleteAllCars();
    }
}
