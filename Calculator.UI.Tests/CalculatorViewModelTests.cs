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
            calculatorViewModel.NumericCommandClick.Execute(Command.ONE);

            //Assert
            A.CallTo(()=>calculatorService.OnNumericCommand(A<Command>._)).MustHaveHappened(Repeated.Exactly.Once);
        }


        [Test]
        public void WhenAnOperatorCommandIsClickThenCalculatorServiceOnOperatorCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.OperatorCommandClick.Execute(Command.PLUS);

            //Assert
            A.CallTo(() => calculatorService.OnOperatorCommand(A<Command>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void WhenAControlCommandIsClickThenCalculatorServiceOnControlCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.ControlCommandClick.Execute(Command.EQUAL);

            //Assert
            A.CallTo(() => calculatorService.OnControlCommand(A<Command>._)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
