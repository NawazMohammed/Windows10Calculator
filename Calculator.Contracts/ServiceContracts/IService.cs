using Calculator.Models;
using System.Collections.Generic;

namespace Calculator.Contracts.ServiceContracts
{
    using Calculator.Models.Expressions;

    public interface IService
    {
        List<ExpressionBase> Expressions { get; }

        ExpressionBase Expression { get; }

        void OnNumericCommand(NumericCommand command);

        void OnOperatorCommand(NumericCommand command);

        void OnControlCommand(NumericCommand command);
    }

    public interface IServiceFactory
    {
        IService GetService(Mode mode);
    }

    }
