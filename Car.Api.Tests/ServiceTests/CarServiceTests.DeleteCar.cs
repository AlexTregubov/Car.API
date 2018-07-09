using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Api.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Car.Api.Tests.ServiceTests
{
    public partial class CarServiceTests
    {
        [Test]
        public async Task DeleteCar_Success()
        {
            var input = _fixture.Create<CreateCarModel>();

            var result = await _carService.AddCar(input);

            result.RegistrationNumber.Should().Be(input.RegistrationNumber);
            result.Name.Should().Be(input.Name);
            result.Description.Should().Be(input.Description);

            _carRepositoryTest.InMemoryCollection.Should().HaveCount(1);

            _carRepositoryTest.InMemoryCollection.First().Name.Should().Be(result.Name);
            _carRepositoryTest.InMemoryCollection.First().Description.Should().Be(result.Description);
            _carRepositoryTest.InMemoryCollection.First().RegistrationNumber.Should().Be(result.RegistrationNumber);
            _carRepositoryTest.InMemoryCollection.First().Id.Should().Be(result.Id);

            await _carService.DeleteCar(result.Id);

            _carRepositoryTest.InMemoryCollection.Should().HaveCount(0);
        }
    }
}
