using System;
using System.Collections.Generic;

using Calculator.Contracts;
using Calculator.Models;
using Calculator.Models.Commands;
using Calculator.Models.Expressions;
using Calculator.Models.Operators;

namespace Calculator.Services
{
    using Calculator.Models.Numbers;

    public class CalculatorService : ICalculatorService
    {
        private readonly INumberFactory numberFactory;

        private readonly ICommandFactory commandFactory;

        private Mode currrentMode;
        private int expressionId;
        public CalculatorService(INumberFactory numberFactory)
        {
            this.numberFactory = numberFactory;
            currrentMode = Mode.DEC;
            Expression = GetExpression("0", "0");
            Expressions = new List<Expression<INumber>>();
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
            Expression = GetExpression(defaultNumber, defaultValue);
        }

        private Expression<INumber> GetExpression(string defaultNumber, string defaultValue)
        {
            return new Expression<INumber>(
                ExpressionId,
                numberFactory.GetNumber(currrentMode, defaultNumber),
                numberFactory.GetNumber(currrentMode, defaultValue));
        }

        public List<Expression<INumber>> Expressions { get; }

        public Expression<INumber> Expression { get; private set; }

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
            Expression.Execute();
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
                    Expression.Execute();
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
