using System;

namespace Calculator.Models.Expressions
{
    using System.ComponentModel;

    using Calculator.Models.Numbers;
    using Calculator.Models.Operators;

    public class HexExpression : ExpressionBase
    {
        public HexExpression(int id, HexNumber defaultNumber, HexNumber defaultValue)
            : base(id, defaultNumber, defaultValue)
        { }

        protected override INumber GetNumber(string value)
        {
            return new HexNumber(value);
        }

        protected override INumber GetNumber(decimal value)
        {
            return new HexNumber(value);
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
                    throw new InvalidEnumArgumentException();
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
                case Command.EIGHT:
                case Command.NINE:
                case Command.POINT:
                case Command.A:
                case Command.B:
                case Command.C:
                case Command.D:
                case Command.E:
                case Command.F:
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
                case Command.NINE:
                    return '9';
                case Command.A:
                    return 'A';
                case Command.B:
                    return 'B';
                case Command.C:
                    return 'C';
                case Command.D:
                    return 'D';
                case Command.E:
                    return 'E';
                case Command.F:
                    return 'F';

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
