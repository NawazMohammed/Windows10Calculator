using Calculator.Models;
using System.Collections.Generic;

namespace Calculator.Contracts
{
    using Calculator.Models.Commands;
    using Calculator.Models.Expressions;
    using Calculator.Models.Operators;

    public interface ICalculatorService
    {
        Mode CurrentMode { get; set; }

        List<Expression> Expressions { get; }

        Expression Expression { get; }

        void OnNumericCommand(INumericCommand command);

        void OnOperatorCommand(IOperator opr);

        void OnControlCommand(ControlCommand command);

        string GetResultInFormat(Mode mode);
    }
}
