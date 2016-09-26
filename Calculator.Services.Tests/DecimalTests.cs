using System.Linq;

using Calculator.Models.Expressions;

using Calculator.Contracts;
using Calculator.Models;
using NUnit.Framework;

namespace Calculator.Services.Tests
{
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
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(DecimalExpression)));
        }
 
        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorService.OnNumericCommand(NumericCommand.TWO);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorService.OnNumericCommand(NumericCommand.TWO);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange 

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("0 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.TWO);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("23"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnNumericCommand(NumericCommand.THREE);
            calculatorService.OnNumericCommand(NumericCommand.FIVE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("23"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 + 3 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);


            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 + 3 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(DecimalExpression)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("3"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnNumericCommand(NumericCommand.POINT);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.MULTIPLY);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.FIVE);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnOperatorCommand(OperatorCommand.DIVIDE);
            calculatorService.OnNumericCommand(NumericCommand.FIVE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);
            calculatorService.OnOperatorCommand(OperatorCommand.POWER);
            calculatorService.OnNumericCommand(NumericCommand.ONE);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);
            calculatorService.OnOperatorCommand(OperatorCommand.ROOT);
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            var expre = calculatorService.Expressions.First();

            //Assert
            Assert.That(expre.Display, Is.EqualTo("257237.731740877"));
            Assert.That(expre.ExpressionString, Is.EqualTo("2 + 3 - 1 * 151 ÷ 50 ^ 10 yroot 2 ="));
        }

        [Test]
        public void WhenANumberWithPointExitsAndAnotherPointIsPressedThenNewPointShouldBeIgnored()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnNumericCommand(NumericCommand.POINT);
            calculatorService.OnNumericCommand(NumericCommand.NINE);


            //Act
            calculatorService.OnNumericCommand(NumericCommand.POINT);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2.9"));
        }

        [Test]
        public void WhenTheLastCharacterOfANumberIsPointAndEqualsIsPressedThenPointShouldBeIgnored()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnNumericCommand(NumericCommand.POINT);


            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsCreated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(DecimalExpression)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(DecimalExpression)));
        }

        [Test]
        public void WhenAMultipleOperationsExistAndEqualsIsPressedThenTheExpressionIsCalculatedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnNumericCommand(NumericCommand.THREE);
            calculatorService.OnNumericCommand(NumericCommand.POINT);
            calculatorService.OnNumericCommand(NumericCommand.FIVE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.SEVEN);
            calculatorService.OnNumericCommand(NumericCommand.THREE);
            calculatorService.OnOperatorCommand(OperatorCommand.MULTIPLY);
            calculatorService.OnNumericCommand(NumericCommand.EIGHT);
            calculatorService.OnNumericCommand(NumericCommand.SIX);
            calculatorService.OnNumericCommand(NumericCommand.POINT);
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.DIVIDE);
            calculatorService.OnNumericCommand(NumericCommand.EIGHT);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("1039.7875"));

        }


        [Test]
        public void WhenAddOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
        }

        [Test]
        public void WhenSubstractOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("-1"));
        }

        [Test]
        public void WhenMultiplyOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.TWO);
            calculatorService.OnOperatorCommand(OperatorCommand.MULTIPLY);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("6"));
        }

        [Test]
        public void WhenDivideOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.SIX);
            calculatorService.OnOperatorCommand(OperatorCommand.DIVIDE);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenRootOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.EIGHT);
            calculatorService.OnOperatorCommand(OperatorCommand.ROOT);
            calculatorService.OnNumericCommand(NumericCommand.THREE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenPowerOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.EIGHT);
            calculatorService.OnOperatorCommand(OperatorCommand.POWER);
            calculatorService.OnNumericCommand(NumericCommand.TWO);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("64"));
        }
    }

}
