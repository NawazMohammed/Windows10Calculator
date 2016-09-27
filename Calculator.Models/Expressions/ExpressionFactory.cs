using System;

namespace Calculator.Models.Expressions
{
    using Calculator.Models.Numbers;

    public interface IExpressionFactory
    {
        Expression GetExpression(Mode mode, int id, string defaultNumber, string defaultValue);
    }

    public class ExpressionFactory : IExpressionFactory
    {
        public Expression GetExpression(Mode mode, int id, string defaultNumber, string defaultValue)
        {
            switch (mode)
            {
                case Mode.DEC:
                    return new DecimalExpression(id, new DecimalNumber(defaultNumber), new DecimalNumber(defaultValue));
                case Mode.HEX:
                    return new HexExpression(id, new HexNumber(defaultNumber), new HexNumber(defaultValue));
                case Mode.OCT:
                    return new OctalExpression(id, new OctalNumber(defaultNumber), new OctalNumber(defaultValue));
                case Mode.BIN:
                    return new BinaryExpression(id, new BinaryNumber(defaultNumber), new BinaryNumber(defaultValue));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
