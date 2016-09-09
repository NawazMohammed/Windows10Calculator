using System;
using System.Collections.Generic;
using Calculator.Models.Operators;
using Calculator.Models;

namespace Calculator.Services
{
    using Calculator.Models.Expressions;

    public class BinaryService : ServiceBase
    {
        public BinaryService()
        {
            Expressions = new List<ExpressionBase>();
            Expression = new BinaryExpression(ExpressionId);
        }
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
                    return false;
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
                    throw new InvalidOperationException();
            }
        }
        protected override void UpdateDisplay(char character)
        {
            if (Expression.Display.Length > 64)
                return;

            if(Expression.Display == "0" && character =='0')
                return;

            if (Expression.Display == "0" && character != '0')
                Expression.Display = "";

            Expression.Display = Expression.Display + character;

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
