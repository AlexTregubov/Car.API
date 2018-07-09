using System.Collections.Generic;
using AutoFixture;
using Car.Api.Model;
using Car.Api.Services;
using Car.Api.Tests.Data;
using Car.Api.Validation;
using FluentValidation;
using NUnit.Framework;

namespace Car.Api.Tests.ServiceTests
{
    [TestFixture]
    public partial class CarServiceTests
    {
        private const string ValidTestNumber = "A123BC123";

        private CarService _carService;

        private CarRepositoryTest _carRepositoryTest;

        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            var validators = new List<IValidator>
            {
                new CreateCarModelValidator(),
                new UpdateCarModelValidator(),
                new RegistrationNumberValidator()
            };

            _carRepositoryTest = new CarRepositoryTest();

            _carService = new CarService(
                _carRepositoryTest,
                new BusinessRulesValidator(validators));

            _fixture.Customize<CreateCarModel>(c => c
                .With(x => x.RegistrationNumber, ValidTestNumber));
        }
    }
}
