namespace Calculator.Services.Tests
{
    using Calculator.Contracts.ServiceContracts;
    using Calculator.Models;

    using NUnit.Framework;

    [TestFixture]
    public  class DecimalServiceTests
    {

        private IService _decimalService;

        [SetUp]
        public void SetUp()
        {
            
        }




        [Test]
        public void OnStartUpNewExpressionIsInitialisedWithDisplayAsZeroAndExpressionStringAsEmpty()
        {

            //Arrange
            _decimalService = new DecimalService();

            //Act


            //Assert

            Assert.That(_decimalService.Expression, Is.Not.Null);

            Assert.That(_decimalService.Expression.ValueString, Is.EqualTo("0"));

            Assert.That(_decimalService.Expression.ExpressionString, Is.EqualTo(""));

        }


        [Test]
        public void GivenDisplayIsAtZeroAndWhenANumberIsPressedThenDisplayShouldBeUpdated()
        {

            //Arrange
            _decimalService = new DecimalService();

            //Act
            _decimalService.OnNumericCommand(Command.TWO);

            //Assert

            Assert.That(_decimalService.Expression.ValueString, Is.EqualTo("2"));

        }

        [Test]
        public void GivenDisplayIsAtZeroAndWhenANumberIsPressedThenExpressionShouldNotChange()
        {

            //Arrange
            _decimalService = new DecimalService();

            //Act
            _decimalService.OnNumericCommand(Command.TWO);

            //Assert

            Assert.That(_decimalService.Expression.ExpressionString, Is.EqualTo(""));

        }
    }
}
