using System.Linq;

using Calculator.Contracts;
using Calculator.Models;
using Calculator.Models.Expressions;
using NUnit.Framework;

namespace Calculator.Services.Tests
{
    using Calculator.Models.Commands;
    using Calculator.Models.Numbers;
    using Calculator.Models.Operators;

    [TestFixture]
    public class GivenACalculatorServiceWithABinaryExpression
    {
        private ICalculatorService calculatorService;

        private INumberFactory numberFactory;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            numberFactory = new NumberFactory();
            calculatorService = new CalculatorService(numberFactory) { CurrentMode = Mode.BIN };
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
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryNumber)));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("0 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnNumericCommand(new One());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("101"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnControlCommand(ControlCommand.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("100 +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());
            calculatorService.OnNumericCommand(new Zero());
            calculatorService.OnOperatorCommand(new Plus());
            //Act
            calculatorService.OnOperatorCommand(new Minus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("100 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new One());

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("10 + 1 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new One());


            //Act
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnOperatorCommand(new Minus());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("100"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("11 + 1 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("1001"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.Type, Is.EqualTo(typeof(BinaryNumber)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("11"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new Zero());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new One());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new One());
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
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new One());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryNumber)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnNumericCommand(new One());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnControlCommand(ControlCommand.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(BinaryNumber)));
        }
    }
}
