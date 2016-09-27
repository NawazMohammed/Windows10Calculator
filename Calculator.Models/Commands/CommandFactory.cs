using System;

namespace Calculator.Models.Commands
{
    using Calculator.Models.Operators;

    public interface ICommandFactory
    {
        IOperator GetOperator(OperatorCommandType command);

        INumericCommand GetNumericCommand(NumericCommandType command);
    }

    public class CommandFactory : ICommandFactory
    {
        public IOperator GetOperator(OperatorCommandType command)
        {
            throw new NotImplementedException();
        }

        public INumericCommand GetNumericCommand(NumericCommandType command)
        {
            throw new NotImplementedException();
        }
    }
}
