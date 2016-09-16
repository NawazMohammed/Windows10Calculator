using System;

namespace Calculator.Models.Expressions
{
    using System.ComponentModel;

    using Calculator.Models.Operators;
    public class OctalExpression : ExpressionBase
    {
        public OctalExpression(int id, Octal defaultNumber, Octal defaultValue)
            : base(id, defaultNumber, defaultValue)
        { }

        protected override INumber GetNumber(string value)
        {
            return new Octal(value);
        }

        protected override INumber GetNumber(decimal value)
        {
            return new Octal(value);
        }

        protected override IOperator GetOperator(Command command)
        {
            switch (command)
            {
                case Command.PLUS:
                    return new Add();
                case Command.MINUS:
                    return new Substract();
                case Command.MULTIPLY:
                    return new Multiply();
                case Command.DIVIDE:
                    return new Divide();
                case Command.EQUAL:
                    return new Equals();
                default:
                    throw new InvalidOperationException();
            }
        }
        protected override bool IsValidNumericCommand(Command command)
        {
            switch (command)
            {
                case Command.ZERO:
                case Command.ONE:
                case Command.TWO:
                case Command.THREE:
                case Command.FOUR:
                case Command.FIVE:
                case Command.SIX:
                case Command.SEVEN:
                    return true;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
        protected override char GetNumericCommandCharacter(Command command)
        {
            switch (command)
            {
                case Command.ZERO:
                    return '0';
                case Command.ONE:
                    return '1';
                case Command.TWO:
                    return '2';
                case Command.THREE:
                    return '3';
                case Command.FOUR:
                    return '4';
                case Command.FIVE:
                    return '5';
                case Command.SIX:
                    return '6';
                case Command.SEVEN:
                    return '7';
                case Command.EIGHT:
                    return '8';
                default:
                    throw new InvalidOperationException();
            }
        }
      
        protected override bool IsValidOperatorCommand(Command command)
        {
            switch (command)
            {
                case Command.PLUS:
                case Command.MINUS:
                case Command.MULTIPLY:
                case Command.DIVIDE:
                    return true;
                default:
                    return false;
            }
        }

    }
}
