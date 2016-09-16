using Calculator.Contracts;
using System.Collections.Generic;
using Calculator.Models;
using Calculator.Contracts.ServiceContracts;
using Calculator.Models.Expressions;

namespace Calculator.Services
{
    using System;

    using Calculator.Common;
    using Calculator.Models.Numbers;

    public class CalculatorService : ICalculatorService
    {
        private Mode currrentMode;
        private int expressionId;
        public CalculatorService()
        {
            currrentMode = Mode.DEC;
            Expression = new DecimalExpression(ExpressionId, new Dec("0"), new Dec("0"));
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
                    Expression = new DecimalExpression(ExpressionId, new Dec(defaultNumber), new Dec(defaultValue));
                    break;
                case Mode.HEX:
                    Expression = new HexExpression(ExpressionId, new Hex(defaultNumber), new Hex(defaultValue));
                    break;
                case Mode.OCT:
                    Expression = new OctalExpression(ExpressionId, new Octal(defaultNumber), new Octal(defaultValue));
                    break;
                case Mode.BIN:
                    Expression = new BinaryExpression(ExpressionId, new Binary(defaultNumber), new Binary(defaultValue));
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

        public void OnNumericCommand(Command command)
        {
            Expression.UpdateNumber(command);
        }

        public void OnOperatorCommand(Command command)
        {
            Expression.ExecutePreviousOperation();
            Expression.StartNewOperation(command);
        }

        public void OnControlCommand(Command command)
        {
            if (!IsValidControlCommand(command)) return;

            switch (command)
            {
                case Command.DELETE:
                    Expression.DeleteLastCharacter();
                    break;
                case Command.EQUAL:
                    Expression.ExecutePreviousOperation();
                    Expression.Complete();
                    Expressions.Add(Expression);
                    ResetExpression("0", Expression.Display);
                    break;
                case Command.CLEAR:
                    ResetExpression("0", "0");
                    break;
            }
        }

        private static bool IsValidControlCommand(Command command)
        {
            switch (command)
            {
                case Command.CLEAR:
                case Command.DELETE:
                case Command.EQUAL:
                    return true;
                default:
                    return false;
            }
        }
    }
}
