using System;

namespace Calculator.Models.Expressions
{
    using System.ComponentModel;

    using Operators;

    public class BinaryExpression : ExpressionBase
    {
        public BinaryExpression(int id, Binary defaultNumber, Binary defaultValue)
            : base(id, defaultNumber, defaultValue)
        { }

        protected override INumber GetNumber(string value)
        {
            return new Binary(value);
        }

        protected override INumber GetNumber(decimal value)
        {
            return new Binary(value);
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
                default:
                    throw new InvalidEnumArgumentException();
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
