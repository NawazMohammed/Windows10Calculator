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
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(DecimalExpression)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
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
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnNumericCommand(Command.POINT);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);
            calculatorService.OnOperatorCommand(Command.MINUS);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.MULTIPLY);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.FIVE);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnOperatorCommand(Command.DIVIDE);
            calculatorService.OnNumericCommand(Command.FIVE);
            calculatorService.OnNumericCommand(Command.ZERO);
            calculatorService.OnOperatorCommand(Command.POWER);
            calculatorService.OnNumericCommand(Command.ONE);
            calculatorService.OnNumericCommand(Command.ZERO);
            calculatorService.OnOperatorCommand(Command.ROOT);
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnControlCommand(Command.EQUAL);

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
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnNumericCommand(Command.POINT);
            calculatorService.OnNumericCommand(Command.NINE);


            //Act
            calculatorService.OnNumericCommand(Command.POINT);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2.9"));
        }

        [Test]
        public void WhenTheLastCharacterOfANumberIsPointAndEqualsIsPressedThenPointShouldBeIgnored()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnNumericCommand(Command.POINT);


            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsCreated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(DecimalExpression)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(DecimalExpression)));
        }

        [Test]
        public void WhenAMultipleOperationsExistAndEqualsIsPressedThenTheExpressionIsCalculatedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnNumericCommand(Command.THREE);
            calculatorService.OnNumericCommand(Command.POINT);
            calculatorService.OnNumericCommand(Command.FIVE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.SEVEN);
            calculatorService.OnNumericCommand(Command.THREE);
            calculatorService.OnOperatorCommand(Command.MULTIPLY);
            calculatorService.OnNumericCommand(Command.EIGHT);
            calculatorService.OnNumericCommand(Command.SIX);
            calculatorService.OnNumericCommand(Command.POINT);
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.DIVIDE);
            calculatorService.OnNumericCommand(Command.EIGHT);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("1039.7875"));

        }


        [Test]
        public void WhenAddOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
        }

        [Test]
        public void WhenSubstractOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.MINUS);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("-1"));
        }

        [Test]
        public void WhenMultiplyOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.TWO);
            calculatorService.OnOperatorCommand(Command.MULTIPLY);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("6"));
        }

        [Test]
        public void WhenDivideOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.SIX);
            calculatorService.OnOperatorCommand(Command.DIVIDE);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenRootOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.EIGHT);
            calculatorService.OnOperatorCommand(Command.ROOT);
            calculatorService.OnNumericCommand(Command.THREE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenPowerOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.EIGHT);
            calculatorService.OnOperatorCommand(Command.POWER);
            calculatorService.OnNumericCommand(Command.TWO);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("64"));
        }
    }

}
