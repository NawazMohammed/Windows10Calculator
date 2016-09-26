using Calculator.Contracts;
using System.Collections.Generic;
using Calculator.Models;
using Calculator.Contracts.ServiceContracts;
using Calculator.Models.Expressions;

namespace Calculator.Services
{
    using System;
    using Calculator.Models.Numbers;

    public class CalculatorService : ICalculatorService
    {
        private Mode currrentMode;
        private int expressionId;
        public CalculatorService()
        {
            currrentMode = Mode.DEC;
            Expression = new DecimalExpression(ExpressionId, new DecimalNumber("0"), new DecimalNumber("0"));
            Expressions = new List<ExpressionBase>();
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
            switch (currrentMode)
            {
                case Mode.DEC:
                    Expression = new DecimalExpression(ExpressionId, new DecimalNumber(defaultNumber), new DecimalNumber(defaultValue));
                    break;
                case Mode.HEX:
                    Expression = new HexExpression(ExpressionId, new HexNumber(defaultNumber), new HexNumber(defaultValue));
                    break;
                case Mode.OCT:
                    Expression = new OctalExpression(ExpressionId, new OctalNumber(defaultNumber), new OctalNumber(defaultValue));
                    break;
                case Mode.BIN:
                    Expression = new BinaryExpression(ExpressionId, new BinaryNumber(defaultNumber), new BinaryNumber(defaultValue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public List<ExpressionBase> Expressions { get; }

        public ExpressionBase Expression { get; private set; }

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

        public void OnNumericCommand(NumericCommand command)
        {
            Expression.UpdateNumber(command);
        }

        public void OnOperatorCommand(OperatorCommand command)
        {
            Expression.ExecutePreviousOperation();
            Expression.StartNewOperation(command);
        }

        public void OnControlCommand(ControlCommand command)
        {
            if (!IsValidControlCommand(command)) return;

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
            }
        }

        private static bool IsValidControlCommand(ControlCommand command)
        {
            switch (command)
            {
                case ControlCommand.CLEAR:
                case ControlCommand.DELETE:
                case ControlCommand.EQUAL:
                    return true;
                default:
                    return false;
            }
        }
    }
}
