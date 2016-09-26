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
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("A1"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.B);
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
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("A01"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnNumericCommand(NumericCommand.ZERO);

            //Act
            calculatorService.OnControlCommand(ControlCommand.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("AF"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnNumericCommand(NumericCommand.NINE);
            calculatorService.OnNumericCommand(NumericCommand.B);

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("F9B +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.C);
            calculatorService.OnNumericCommand(NumericCommand.THREE);
            calculatorService.OnNumericCommand(NumericCommand.NINE);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("C39 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);

            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("AF + 9 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);


            //Act
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnOperatorCommand(OperatorCommand.MINUS);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("AF + 9 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);


            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexExpression)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            calculatorService.OnNumericCommand(NumericCommand.C);
            calculatorService.OnNumericCommand(NumericCommand.ONE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("C1"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));

        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

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
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexExpression)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(NumericCommand.A);
            calculatorService.OnNumericCommand(NumericCommand.F);
            calculatorService.OnOperatorCommand(OperatorCommand.PLUS);
            calculatorService.OnNumericCommand(NumericCommand.NINE);

            //Act
            calculatorService.OnControlCommand(ControlCommand.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexExpression)));
        }
    }
}
