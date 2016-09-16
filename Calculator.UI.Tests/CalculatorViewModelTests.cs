namespace Calculator.UI.Tests
{
    using Calculator.Contracts;
    using Calculator.Models;
    using Calculator.UI.ViewModels;

    using FakeItEasy;

    using NUnit.Framework;

    [TestFixture]
    public class GivenACalculatorViewModelWithACalculatorService
    {

        private CalculatorViewModel calculatorViewModel;

        private ICalculatorService calculatorService;


        [SetUp]
        public void SetUp()
        {
            //Arrange
            calculatorService = A.Fake<ICalculatorService>();
            calculatorViewModel = new CalculatorViewModel(calculatorService);
        }

        [Test]
        public void WhenANumericCommandIsClickedThenCalculatorServiceOnNumericCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.OnNumericCommandClick(Command.ONE);

            //Assert
            A.CallTo(()=>calculatorService.OnNumericCommand(A<Command>._)).MustHaveHappened(Repeated.Exactly.Once);
        }


        [Test]
        public void WhenAnOperatorCommandIsClickThenCalculatorServiceOnOperatorCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.OnOperatorCommandClick(Command.ONE);

            //Assert
            A.CallTo(() => calculatorService.OnOperatorCommand(A<Command>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void WhenAControlCommandIsClickThenCalculatorServiceOnControlCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.OnControlCommandClick(Command.PLUS);

            //Assert
            A.CallTo(() => calculatorService.OnControlCommand(A<Command>._)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
