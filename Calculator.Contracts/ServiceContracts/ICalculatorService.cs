using Calculator.Models;
using System.Collections.Generic;

namespace Calculator.Contracts
{
    using Calculator.Models.Expressions;

    public interface ICalculatorService
    {
        Mode CurrentMode { get; set; }

        List<ExpressionBase> Expressions { get; }

        ExpressionBase Expression { get; }

        void OnNumericCommand(NumericCommand command);

        void OnOperatorCommand(OperatorCommand command);

        void OnControlCommand(ControlCommand command);

        string GetResultInFormat(Mode mode);
    }
}
