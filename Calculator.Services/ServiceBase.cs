using System.Collections.Generic;
using Calculator.Contracts.ServiceContracts;
using Calculator.Models;
using Calculator.Models.Operators;

namespace Calculator.Services
{
    public abstract class ServiceBase : IService
    {
        public  ExpressionBase Expression { get; protected set; }
        private int expressionId;
        private CommandType previousCommandType;

        private INumber number;
        public INumber Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public int ExpressionId
        {
            get
            {
                expressionId = expressionId + 1;
                return expressionId;
            }
        }

        public List<ExpressionBase> Expressions { get; protected set; }

        public void OnNumericCommand(Command command)
        {
            if (!IsValidNumericCommand(command))
                return;

            var display = Expression.CurrentOperation.Number.ToDisplayString();

            var commandText = GetNumericCommandCharacter(command);
            UpdateDisplay(commandText);

            Expression.CurrentOperation.Number = GetNumber(display);

            previousCommandType = CommandType.Number;
        }

        public void OnOperatorCommand(Command command)
        {
            if (!IsValidOperatorCommand(command))
                return;
            var display = Expression.CurrentOperation.Number.ToDisplayString();
            var opr = GetOperator(command);
            var number = GetNumber(display);

            ExecuteOperation(opr, number);

            previousCommandType = CommandType.Operator;
        }

        private void ExecuteOperation(IOperator opr, INumber number)
        {
            if (Expression.CurrentOperation != null)
            {
                if (previousCommandType == CommandType.Operator)
                {
                    Expression.CurrentOperation.Operator = opr;
                }
                else
                {
                    Expression.CurrentOperation.Number = number;
                    Expression.ExecuteCurrentOperation(GetNumber);
                    Expression.ResetCurrentOperation();
                }
            }

            if (Expression.CurrentOperation == null)
                Expression.CurrentOperation = new Operation(number);
        }

        protected abstract INumber GetNumber(string value);

        protected abstract INumber GetNumber(decimal value);

        public void OnControlCommand(Command command)
        {
            if (!IsValidControlCommand(command))
                return;

            switch (command)
            {
                case Command.DELETE:
                    DeleteLastCharacter();
                    break;
                case Command.EQUAL:
                    HandleEqualsCommand();
                    break;
                case Command.CLEAR:
                    ClearDisplay();
                    break;
            }
        }

        private void ClearDisplay()
        {
            Expression.Display = "0";
            //Expression = new ExpressionBase(ExpressionId, GetNumber(Expression.Display));
        }

        private void DeleteLastCharacter()
        {
            if (Expression.Display == null)
                return;

            if (Expression.Display.Length > 0)
                Expression.Display = Expression.Display.Remove(Expression.Display.Length - 1);
            if (Expression.Display == "")
                Expression.Display = "0";
        }

        private void HandleEqualsCommand()
        {
            if (Expression == null)
                return;
            var opr = new Equals();
            var number = GetNumber(Expression.Display);
            ExecuteOperation(opr, number);

            Expressions.Add(Expression);

            Expression = new Expression(ExpressionId, number) { Display = "0" };

            previousCommandType = CommandType.Equals;
        }

        protected abstract bool IsValidNumericCommand(Command command);

        protected abstract char GetNumericCommandCharacter(Command command);

        protected abstract void UpdateDisplay(char character);

        protected abstract bool IsValidOperatorCommand(Command command);

        protected abstract IOperator GetOperator(Command command);

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
