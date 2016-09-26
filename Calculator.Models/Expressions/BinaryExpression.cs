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
                default:
                    throw new InvalidEnumArgumentException();
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
