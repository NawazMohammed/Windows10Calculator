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
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("0 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnNumericCommand(Command.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("101"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnControlCommand(Command.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("100 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);
            calculatorService.OnNumericCommand(Command.ZERO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            //Act
            calculatorService.OnOperatorCommand(Command.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("100 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ONE);

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("10 + 1 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ONE);


            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnOperatorCommand(Command.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("100"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("11 + 1 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("1001"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryExpression)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnControlCommand(Command.EQUAL);

            //Act
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ONE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnControlCommand(Command.EQUAL);

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
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ONE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryExpression)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnControlCommand(Command.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryExpression)));
        }
    }
}
