using System.Linq;
using System.Threading.Tasks;
using Car.Api.Model;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using FluentValidation;

namespace Car.Api.Tests.ServiceTests
{
    public partial class CarServiceTests
    {
        [Test]
        public void AddCar_NameEmpty()
        {
            var input = _fixture.Create<CreateCarModel>();
            input.Name = string.Empty;

            _carService
                .Awaiting(s => s.AddCar(input))
                .Should()
                .Throw<ValidationException>()
                .Where(e => e.Message.Contains(@"'Name' should not be empty."));
        }

        [Test]
        public void AddCar_NameNull()
        {
            var input = _fixture.Create<CreateCarModel>();
            input.Name = null;

            _carService
                .Awaiting(s => s.AddCar(input))
                .Should()
                .Throw<ValidationException>()
                .Where(e => e.Message.Contains(@"'Name' must not be empty."));
        }

        [Test]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        [TestCase("testString")]
        public void AddCar_RegistrationNumberWrong(string regNumber)
        {
            var input = _fixture.Create<CreateCarModel>();
            input.RegistrationNumber = regNumber;

            _carService
                .Awaiting(s => s.AddCar(input))
                .Should()
                .Throw<ValidationException>();
        }

        [Test]
        public async Task AddCar_Success()
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
        }
    }
}
