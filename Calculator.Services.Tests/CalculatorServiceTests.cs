namespace Calculator.Services.Tests
{
    using System.Linq;

    using Contracts;
    using Models;
    using NUnit.Framework;

    [TestFixture]
    public class GivenACalculatorServiceWithADecimalExpression
    {
        private ICalculatorService calculatorService;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            calculatorService = new CalculatorService();
        }

        [Test]
        public void OnStartUpDisplayIsZeroAndExpressionStringIsEmptyAndModeIsDecimal()
        {
            //Arrange 
            //(see setup)

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.CurrentMode, Is.EqualTo(Mode.DEC));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorService.OnNumericCommand(Command.TWO);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorService.OnNumericCommand(Command.TWO);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange 

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("0 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.TWO);

            //Act
            calculatorService.OnNumericCommand(Command.THREE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("23"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnNumericCommand(Command.THREE);
            calculatorService.OnNumericCommand(Command.FIVE);

            //Act
            calculatorService.OnControlCommand(Command.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("23"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            //Act
            calculatorService.OnOperatorCommand(Command.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 + 3 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);


            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnOperatorCommand(Command.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 + 3 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);
            calculatorService.OnControlCommand(Command.EQUAL);

            //Act
            calculatorService.OnNumericCommand(Command.THREE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("3"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);
            calculatorService.OnControlCommand(Command.EQUAL);

            //Act
            var expre = calculatorService.Expressions.First();

            //Assert
            Assert.That(expre.Display, Is.EqualTo("5"));
            Assert.That(expre.ExpressionString, Is.EqualTo("2 + 3 ="));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsCreated()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService = new CalculatorService();
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }
    }
}
