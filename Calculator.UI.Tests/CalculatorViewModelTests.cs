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
            calculatorViewModel.NumericCommandClick.Execute(NumericCommand.ONE);

            //Assert
            A.CallTo(()=>calculatorService.OnNumericCommand(A<NumericCommand>._)).MustHaveHappened(Repeated.Exactly.Once);
        }


        [Test]
        public void WhenAnOperatorCommandIsClickThenCalculatorServiceOnOperatorCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.OperatorCommandClick.Execute(OperatorCommand.PLUS);

            //Assert
            A.CallTo(() => calculatorService.OnOperatorCommand(A<OperatorCommand>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void WhenAControlCommandIsClickThenCalculatorServiceOnControlCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.ControlCommandClick.Execute(ControlCommand.EQUAL);

            //Assert
            A.CallTo(() => calculatorService.OnControlCommand(A<ControlCommand>._)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
