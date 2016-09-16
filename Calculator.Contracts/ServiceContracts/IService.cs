using Calculator.Models;
using System.Collections.Generic;

namespace Calculator.Contracts.ServiceContracts
{
    using Calculator.Models.Expressions;

    public interface IService
    {
        List<ExpressionBase> Expressions { get; }

        ExpressionBase Expression { get; }

        void OnNumericCommand(Command command);

        void OnOperatorCommand(Command command);

        void OnControlCommand(Command command);
    }

    public interface IServiceFactory
    {
        IService GetService(Mode mode);
    }

    }
