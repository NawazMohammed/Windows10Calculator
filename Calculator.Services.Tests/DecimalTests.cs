using System.Linq;

using Calculator.Models.Expressions;

using Calculator.Contracts;
using Calculator.Models;
using NUnit.Framework;

namespace Calculator.Services.Tests
{
    using Calculator.Models.Commands;
    using Calculator.Models.Operators;

    [TestFixture]
    public class GivenACalculatorServiceWithADecimalExpression
    {
        private ICalculatorService calculatorService;
        private IExpressionFactory expressionFactory;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            expressionFactory = new ExpressionFactory();
            calculatorService = new CalculatorService(expressionFactory);
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
            calculatorService.OnNumericCommand(new Two());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            //(see setup)

            //Act
            calculatorService.OnNumericCommand(new Two());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange 

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("0 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new Two());

            //Act
            calculatorService.OnNumericCommand(new Three());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("23"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnNumericCommand(new Three());
            calculatorService.OnNumericCommand(new Five());

            //Act
            calculatorService.OnControlCommand(ControlCommand.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("23"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            //Act
            calculatorService.OnOperatorCommand(new Minus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 + 3 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());


            //Act
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnOperatorCommand(new Minus());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("2 + 3 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());

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
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            calculatorService.OnNumericCommand(new Three());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("3"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnNumericCommand(new Point());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());
            calculatorService.OnOperatorCommand(new Minus());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Multiply());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Five());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Divide());
            calculatorService.OnNumericCommand(new Five());
            calculatorService.OnNumericCommand(new Zero());
            calculatorService.OnOperatorCommand(new Power());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());
            calculatorService.OnOperatorCommand(new Root());
            calculatorService.OnNumericCommand(new Two());
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
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnNumericCommand(new Point());
            calculatorService.OnNumericCommand(new Nine());


            //Act
            calculatorService.OnNumericCommand(new Point());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2.9"));
        }

        [Test]
        public void WhenTheLastCharacterOfANumberIsPointAndEqualsIsPressedThenPointShouldBeIgnored()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnNumericCommand(new Point());


            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsCreated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());

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
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());

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
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnNumericCommand(new Three());
            calculatorService.OnNumericCommand(new Point());
            calculatorService.OnNumericCommand(new Five());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Seven());
            calculatorService.OnNumericCommand(new Three());
            calculatorService.OnOperatorCommand(new Multiply());
            calculatorService.OnNumericCommand(new Eight());
            calculatorService.OnNumericCommand(new Six());
            calculatorService.OnNumericCommand(new Point());
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Divide());
            calculatorService.OnNumericCommand(new Eight());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("1039.7875"));

        }


        [Test]
        public void WhenAddOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Three());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("5"));
        }

        [Test]
        public void WhenSubstractOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Minus());
            calculatorService.OnNumericCommand(new Three());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("-1"));
        }

        [Test]
        public void WhenMultiplyOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Two());
            calculatorService.OnOperatorCommand(new Multiply());
            calculatorService.OnNumericCommand(new Three());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("6"));
        }

        [Test]
        public void WhenDivideOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Six());
            calculatorService.OnOperatorCommand(new Divide());
            calculatorService.OnNumericCommand(new Three());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenRootOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Eight());
            calculatorService.OnOperatorCommand(new Root());
            calculatorService.OnNumericCommand(new Three());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("2"));
        }

        [Test]
        public void WhenPowerOperationExistsAndEqualsIsPressedThenNewExpressionIsExecutedCorrectly()
        {
            //Arrange
            calculatorService.OnNumericCommand(new Eight());
            calculatorService.OnOperatorCommand(new Power());
            calculatorService.OnNumericCommand(new Two());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("64"));
        }
    }

}
