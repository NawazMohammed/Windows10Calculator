using System;
using System.ComponentModel;
using Calculator.Models.Numbers;
using Calculator.Models.Operators;

namespace Calculator.Models.Expressions
{ 
    public class BinaryExpression : ExpressionBase
    {
        public BinaryExpression(int id, BinaryNumber defaultNumber, BinaryNumber defaultValue)
            : base(id, defaultNumber, defaultValue)
        { }

        protected override INumber GetNumber(string value)
        {
            return new BinaryNumber(value);
        }

        protected override INumber GetNumber(decimal value)
        {
            return new BinaryNumber(value);
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
