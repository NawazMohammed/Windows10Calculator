using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.Commands
{
    using Calculator.Models.Operators;

    public interface ICommandFactory
    {
        IOperator GetArithmeticOperator(OperatorCommand command);
        char GetNumericCharacter(NumericCommand command);
        IControlOperator GetControlOperator(ControlCommand command);
    }
}
