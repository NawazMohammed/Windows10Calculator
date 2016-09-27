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
    public class GivenACalculatorServiceWithAHexExpression
    {
        private ICalculatorService calculatorService;
        private INumberFactory numberFactory;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            numberFactory = new NumberFactory();
            calculatorService = new CalculatorService(numberFactory) { CurrentMode = Mode.HEX };
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
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexNumber)));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new One());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("A1"));
        }

        [Test]
        public void WhenDisplayIsAtZeroAndANumberIsPressedThenExpressionStringIsEmpty()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnNumericCommand(new B());
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
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnNumericCommand(new One());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("A01"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndADeleteIsPressedThenDisplayIsUpdated()
        {
            //Arrange 
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnNumericCommand(new Zero());

            //Act
            calculatorService.OnControlCommand(ControlCommand.DELETE);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("AF"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnNumericCommand(new Nine());
            calculatorService.OnNumericCommand(new B());

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("F9B +"));
        }

        [Test]
        public void WhenDisplayIsAtSomeNumberAndAOperatorIsPressedAndADifferentOperatorIsPressedThenExpressionStringIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new C());
            calculatorService.OnNumericCommand(new Three());
            calculatorService.OnNumericCommand(new Nine());
            calculatorService.OnOperatorCommand(new Plus());
            //Act
            calculatorService.OnOperatorCommand(new Minus());

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("C39 -"));
        }

        [Test]
        public void WhenAOperationExistsAndAOperatorIsPressedThenExpressionIsExecuted()
        {
            //Arrange
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());

            //Act
            calculatorService.OnOperatorCommand(new Plus());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("AF + 9 +"));

        }

        [Test]
        public void WhenAOperationExistsAndTwoDifferentOperatorArePressedThenLastOperatorIsConsidered()
        {
            //Arrange
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());


            //Act
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnOperatorCommand(new Minus());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo("AF + 9 -"));

        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenNewExpressionIsStartedWithTotal()
        {
            //Arrange
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());


            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("B8"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexNumber)));
        }

        [Test]
        public void WhenEqualsIsExecutedAndANewNumberIsPressedThenDisplayShouldBeNewNumber()
        {
            //Arrange
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Act
            calculatorService.OnNumericCommand(new C());
            calculatorService.OnNumericCommand(new One());

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("C1"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
        }

        [Test]
        public void WhenAOperationExistsAndEqualsIsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expressions.Count, Is.EqualTo(1));

        }

        [Test]
        public void WhenAnExpressionIsSelectedItsPressedThenExpressionsListIsUpdated()
        {
            //Arrange
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());
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
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());

            //Act
            calculatorService.OnControlCommand(ControlCommand.EQUAL);

            //Assert
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexNumber)));
        }

        [Test]
        public void WhenAOperationExistsAndClearIsPressedThenTheExpressionIsCleared()
        {
            //Arrange
            calculatorService.OnNumericCommand(new A());
            calculatorService.OnNumericCommand(new F());
            calculatorService.OnOperatorCommand(new Plus());
            calculatorService.OnNumericCommand(new Nine());

            //Act
            calculatorService.OnControlCommand(ControlCommand.CLEAR);

            //Assert
            Assert.That(calculatorService.Expression.Display, Is.EqualTo("0"));
            Assert.That(calculatorService.Expression.ExpressionString, Is.EqualTo(""));
            Assert.That(calculatorService.Expression.GetType(), Is.EqualTo(typeof(HexNumber)));
        }
    }
}
