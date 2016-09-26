using System.Linq;

using Calculator.Contracts;
using Calculator.Models;
using Calculator.Models.Expressions;
using NUnit.Framework;

namespace Calculator.Services.Tests
{  
    [TestFixture]
    public class GivenACalculatorServiceWithABinaryExpression
    {
        private ICalculatorService calculatorService;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            calculatorService = new CalculatorService { CurrentMode = Mode.BIN };
        }

        [Test]
        public void OnStartUpDisplayIsZeroAndExpressionStringIsEmptyAndModeIsDecimal()
        {
            //Arrange 
            //(see setup)

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.CurrentMode, Is.EqualTo(Mode.BIN));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryExpression)));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("0 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("101"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnControlCommand(ControlCommand.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("100 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("100 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("10 + 1 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);


            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("100"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("11 + 1 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("1001"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryExpression)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            var expre = calculatorService.Expressions.First();

            //Assert
            Assert.That(expre.Display, Is.EqualTo("10"));
            Assert.That(expre.ExpressionString, Is.EqualTo("1 + 1 ="));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsCreated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryExpression)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnControlCommand(ControlCommand.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryExpression)));
        }
    }
}
