using System;

namespace Calculator.Models.Expressions
{
    using System.ComponentModel;

    using Calculator.Models.Numbers;
    using Calculator.Models.Operators;
    public class OctalExpression : ExpressionBase
    {
        public OctalExpression(int id, OctalNumber defaultNumber, OctalNumber defaultValue)
            : base(id, defaultNumber, defaultValue)
        { }

        protected override INumber GetNumber(string value)
        {
            return new OctalNumber(value);
        }

        protected override INumber GetNumber(decimal value)
        {
            return new OctalNumber(value);
        }

        protected override IOperator GetOperator(OperatorCommand command)
        {
            switch (command)
            {
                case OperatorCommand.PLUS:
                    return new Add();
                case OperatorCommand.MINUS:
                    return new Substract();
                case OperatorCommand.MULTIPLY:
                    return new Multiply();
                case OperatorCommand.DIVIDE:
                    return new Divide();
                case OperatorCommand.EQUALS:
                    return new Equals();
                default:
                    throw new InvalidOperationException();
            }
        }
        protected override bool IsValidNumericCommand(NumericCommand command)
        {
            switch (command)
            {
                case NumericCommand.ZERO:
                case NumericCommand.ONE:
                case NumericCommand.TWO:
                case NumericCommand.THREE:
                case NumericCommand.FOUR:
                case NumericCommand.FIVE:
                case NumericCommand.SIX:
                case NumericCommand.SEVEN:
                    return true;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
        protected override char GetNumericCommandCharacter(NumericCommand command)
        {
            switch (command)
            {
                case NumericCommand.ZERO:
                    return '0';
                case NumericCommand.ONE:
                    return '1';
                case NumericCommand.TWO:
                    return '2';
                case NumericCommand.THREE:
                    return '3';
                case NumericCommand.FOUR:
                    return '4';
                case NumericCommand.FIVE:
                    return '5';
                case NumericCommand.SIX:
                    return '6';
                case NumericCommand.SEVEN:
                    return '7';
                case NumericCommand.EIGHT:
                    return '8';
                default:
                    throw new InvalidOperationException();
            }
        }
      
        protected override bool IsValidOperatorCommand(OperatorCommand command)
        {
            switch (command)
            {
                case OperatorCommand.PLUS:
                case OperatorCommand.MINUS:
                case OperatorCommand.MULTIPLY:
                case OperatorCommand.DIVIDE:
                    return true;
                default:
                    return false;
            }
        }

    }
}
