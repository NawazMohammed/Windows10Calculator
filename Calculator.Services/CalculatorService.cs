using Calculator.Contracts;
using System.Collections.Generic;
using Calculator.Models;
using Calculator.Models.Expressions;

namespace Calculator.Services
{
    using System;

    using Calculator.Models.Commands;
    using Calculator.Models.Operators;

    public class CalculatorService : ICalculatorService
    {
        private readonly IExpressionFactory expressionFactory;

        private readonly ICommandFactory commandFactory;

        private Mode currrentMode;
        private int expressionId;
        public CalculatorService(IExpressionFactory expressionFactory)
        {
            this.expressionFactory = expressionFactory;
            currrentMode = Mode.DEC;
            Expression = expressionFactory.GetExpression(Mode.DEC,ExpressionId, "0", "0");
            Expressions = new List<Expression>();
        }

        public int ExpressionId
        {
            get
            {
                expressionId = expressionId + 1;
                return expressionId;
            }
        }
        public Mode CurrentMode
        {
            get
            {
                return currrentMode;
            }
            set
            {
                currrentMode = value;
                ResetExpression("0", Expression.Display);
            }
        }

        private void ResetExpression(string defaultNumber, string defaultValue)
        {
            Expression = expressionFactory.GetExpression(
                currrentMode,
                ExpressionId,
                defaultNumber,
                defaultValue);
        }

        public List<Expression> Expressions { get; }

        public Expression Expression { get; private set; }

        public string GetResultInFormat(Mode mode)
        {
            switch (mode)
            {
                //case Mode.DEC:
                //    return DisplayResult;
                //case Mode.HEX:
                //    return decimal.Parse(DisplayResult).ToHex(); 
                //case Mode.OCT:
                //    return decimal.Parse(DisplayResult).ToOct();
                //case Mode.BIN:
                //    return decimal.Parse(DisplayResult).ToBinary();
                default:
                    return "0";
            }
        }

        public void OnNumericCommand(INumericCommand command)
        {
            Expression.UpdateNumber(command);
        }

        public void OnOperatorCommand(IOperator opr)
        {
            Expression.ExecutePreviousOperation();
            Expression.AddOrUpdateOperator(opr);
        }

        public void OnControlCommand(ControlCommand command)
        {
            switch (command)
            {
                case ControlCommand.DELETE:
                    Expression.DeleteLastCharacter();
                    break;
                case ControlCommand.EQUAL:
                    Expression.ExecutePreviousOperation();
                    Expression.Complete();
                    Expressions.Add(Expression);
                    ResetExpression("0", Expression.Display);
                    break;
                case ControlCommand.CLEAR:
                    ResetExpression("0", "0");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);
            }
        }
    }
}
