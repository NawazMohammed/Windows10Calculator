using System.Linq;

using Calculator.Contracts;
using Calculator.Models;
using Calculator.Models.Expressions;

using NUnit.Framework;

namespace Calculator.Services.Tests
{
    [TestFixture]
    public class GivenACalculatorServiceWithAHexExpression
    {
        private ICalculatorService calculatorService;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            calculatorService = new CalculatorService { CurrentMode = Mode.HEX };
        }

        [Test]
        public void OnStartUpDisplayIsZeroAndExpressionStringIsEmptyAndModeIsDecimal()
        {
            //Arrange 
            //(see setup)

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.CurrentMode, Is.EqualTo(Mode.HEX));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexExpression)));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("A1"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnNumericCommand(Command.B);
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
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnNumericCommand(Command.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("A01"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnNumericCommand(Command.ZERO);

            //Act
            calculatorService.OnControlCommand(Command.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("AF"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnNumericCommand(Command.NINE);
            calculatorService.OnNumericCommand(Command.B);

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("F9B +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.C);
            calculatorService.OnNumericCommand(Command.THREE);
            calculatorService.OnNumericCommand(Command.NINE);
            calculatorService.OnOperatorCommand(Command.PLUS);
            //Act
            calculatorService.OnOperatorCommand(Command.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("C39 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);

            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("AF + 9 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);


            //Act
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnOperatorCommand(Command.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("AF + 9 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);


            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexExpression)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);
            calculatorService.OnControlCommand(Command.EQUAL);

            //Act
            calculatorService.OnNumericCommand(Command.C);
            calculatorService.OnNumericCommand(Command.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("C1"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));

        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);
            calculatorService.OnControlCommand(Command.EQUAL);

            //Act
            var expre = calculatorService.Expressions.First();

            //Assert
            Assert.That(expre.Display, Is.EqualTo("B8"));
            Assert.That(expre.ExpressionString, Is.EqualTo("AF + 9 ="));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsCreated()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);

            //Act
            calculatorService.OnControlCommand(Command.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexExpression)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(Command.A);
            calculatorService.OnNumericCommand(Command.F);
            calculatorService.OnOperatorCommand(Command.PLUS);
            calculatorService.OnNumericCommand(Command.NINE);

            //Act
            calculatorService.OnControlCommand(Command.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexExpression)));
        }
    }
}
