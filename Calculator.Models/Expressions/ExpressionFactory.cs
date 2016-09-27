using System;

namespace Calculator.Models.Expressions
{
    using Calculator.Models.Numbers;

    public interface IExpressionFactory
    {
        Expression<NumberBase> GetExpression(Mode mode, int id, string defaultNumber, string defaultValue);
    }

    public class ExpressionFactory : IExpressionFactory
    {
        public Expression<NumberBase> GetExpression(Mode mode, int id, string defaultNumber, string defaultValue)
        {
            switch (mode)
            {
                case Mode.DEC:
                    return new Expression<NumberBase>(id, new DecimalNumber(defaultNumber), new DecimalNumber(defaultValue));
                case Mode.HEX:
                    return new Expression<NumberBase>(id, new HexNumber(defaultNumber), new HexNumber(defaultValue));
                case Mode.OCT:
                    return new Expression<NumberBase>(id, new OctalNumber(defaultNumber), new OctalNumber(defaultValue));
                case Mode.BIN:
                    return new Expression<NumberBase>(id, new BinaryNumber(defaultNumber), new BinaryNumber(defaultValue));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
