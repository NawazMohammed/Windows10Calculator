using System;

namespace Calculator.Models.Commands
{
    using Calculator.Models.Operators;

    public class DecimalCommandFactory :ICommandFactory
    {
      
        public INumericCommand GetNumericCommand(NumericCommandType command)
        {
            switch (command)
            {
                case NumericCommandType.ZERO:
                    return new Zero();
                case NumericCommandType.ONE:
                    return new One();
                case NumericCommandType.TWO:
                    return new Two();
                case NumericCommandType.THREE:
                    return new Three();
                case NumericCommandType.FOUR:
                    return new Four();
                case NumericCommandType.FIVE:
                    return new Five();
                case NumericCommandType.SIX:
                    return new Six();
                case NumericCommandType.SEVEN:
                    return new Seven();
                case NumericCommandType.EIGHT:
                    return new Eight();
                case NumericCommandType.NINE:
                    return new Nine();
                case NumericCommandType.POINT:
                    return new Point();
                default:
                    throw new InvalidOperationException();
            }
        }

        public IOperator GetOperator(OperatorCommandType command)
        {
            switch (command)
            {
                case OperatorCommandType.PLUS:
                    return new Plus();
                case OperatorCommandType.MINUS:
                    return new Minus();
                case OperatorCommandType.MULTIPLY:
                    return new Multiply();
                case OperatorCommandType.DIVIDE:
                    return new Divide();
                case OperatorCommandType.ROOT:
                    return new Root();
                case OperatorCommandType.POWER:
                    return new Power();
                case OperatorCommandType.EQUALS:
                    return new Equals();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
