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

        void OnNumericCommand(Command command);

        void OnOperatorCommand(Command command);

        void OnControlCommand(Command command);

        string GetResultInFormat(Mode mode);
    }
}
