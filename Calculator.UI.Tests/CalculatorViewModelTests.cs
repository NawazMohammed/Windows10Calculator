namespace Calculator.UI.Tests
{
    using Calculator.Contracts;
    using Calculator.Models;
    using Calculator.Models.Commands;
    using Calculator.Models.Operators;
    using Calculator.UI.ViewModels;

    using FakeItEasy;

    using NUnit.Framework;

    using A = FakeItEasy.A;

    [TestFixture]
    public class GivenACalculatorViewModelWithACalculatorService
    {

        private CalculatorViewModel calculatorViewModel;

        private ICalculatorService calculatorService;

        private ICommandFactory commandFactory;


        [SetUp]
        public void SetUp()
        {
            //Arrange
            calculatorService = A.Fake<ICalculatorService>();
            commandFactory = A.Fake<ICommandFactory>();
            calculatorViewModel = new CalculatorViewModel(calculatorService, commandFactory);
        }

        [Test]
        public void WhenANumericCommandIsClickedThenCalculatorServiceOnNumericCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.NumericCommandClick.Execute(NumericCommandType.ONE);

            //Assert
            A.CallTo(()=>calculatorService.OnNumericCommand(A<INumericCommand>._)).MustHaveHappened(Repeated.Exactly.Once);
        }


        [Test]
        public void WhenAnOperatorCommandIsClickThenCalculatorServiceOnOperatorCommandIsCalled()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorViewModel.OperatorCommandClick.Execute(OperatorCommandType.PLUS);

            //Assert
            A.CallTo(() => calculatorService.OnOperatorCommand(A<IOperator>._)).MustHaveHappened(Repeated.Exactly.Once);
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
