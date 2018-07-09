using System.Linq;
using System.Threading.Tasks;
using Car.Api.Model;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using AutoFixture;

namespace Car.Api.Tests.ServiceTests
{
    public partial class CarServiceTests
    {
        [Test]
        [TestCase("  ")]
        [TestCase("testString")]
        public void UpdateCar_RegistrationNumberWrong(string regNumber)
        {
            var input = _fixture.Create<UpdateCarModel>();
            input.RegistrationNumber = regNumber;

            _carService
                .Awaiting(s => s.UpdateCar(_fixture.Create<string>(), input))
                .Should()
                .Throw<ValidationException>();
        }

        [Test]
        public async Task UpdateCar_Success_Name()
        {
            var input = _fixture.Create<CreateCarModel>();

            var result = await _carService.AddCar(input);

            var newName = _fixture.Create<string>();

            result.Name = newName;

            var updateResult = await _carService.UpdateCar(result.Id, new UpdateCarModel
            {
                Name = result.Name
            });

            updateResult.RegistrationNumber.Should().Be(input.RegistrationNumber);
            updateResult.Name.Should().Be(newName);
            updateResult.Description.Should().Be(input.Description);

            _carRepositoryTest.InMemoryCollection.Should().HaveCount(1);

            _carRepositoryTest.InMemoryCollection.First().Name.Should().Be(newName);
            _carRepositoryTest.InMemoryCollection.First().Description.Should().Be(result.Description);
            _carRepositoryTest.InMemoryCollection.First().RegistrationNumber.Should().Be(result.RegistrationNumber);
            _carRepositoryTest.InMemoryCollection.First().Id.Should().Be(result.Id);
        }

        [Test]
        public async Task UpdateCar_Success_Description()
        {
            var input = _fixture.Create<CreateCarModel>();

            var result = await _carService.AddCar(input);

            var newDescription = _fixture.Create<string>();

            result.Description = newDescription;

            var updateResult = await _carService.UpdateCar(result.Id, new UpdateCarModel
            {
                Description = result.Description
            });

            updateResult.RegistrationNumber.Should().Be(input.RegistrationNumber);
            updateResult.Name.Should().Be(input.Name);
            updateResult.Description.Should().Be(newDescription);

            _carRepositoryTest.InMemoryCollection.Should().HaveCount(1);

            _carRepositoryTest.InMemoryCollection.First().Name.Should().Be(result.Name);
            _carRepositoryTest.InMemoryCollection.First().Description.Should().Be(newDescription);
            _carRepositoryTest.InMemoryCollection.First().RegistrationNumber.Should().Be(result.RegistrationNumber);
            _carRepositoryTest.InMemoryCollection.First().Id.Should().Be(result.Id);
        }

        [Test]
        public async Task UpdateCar_Success_RegNumber()
        {
            var input = _fixture.Create<CreateCarModel>();

            var result = await _carService.AddCar(input);

            var newRegNumber = "J678TR34";

            result.RegistrationNumber = newRegNumber;

            var updateResult = await _carService.UpdateCar(result.Id, new UpdateCarModel
            {
                RegistrationNumber = result.RegistrationNumber
            });

            updateResult.RegistrationNumber.Should().Be(newRegNumber);
            updateResult.Name.Should().Be(input.Name);
            updateResult.Description.Should().Be(input.Description);

            _carRepositoryTest.InMemoryCollection.Should().HaveCount(1);

            _carRepositoryTest.InMemoryCollection.First().Name.Should().Be(result.Name);
            _carRepositoryTest.InMemoryCollection.First().Description.Should().Be(result.Description);
            _carRepositoryTest.InMemoryCollection.First().RegistrationNumber.Should().Be(newRegNumber);
            _carRepositoryTest.InMemoryCollection.First().Id.Should().Be(result.Id);
        }
    }
}
