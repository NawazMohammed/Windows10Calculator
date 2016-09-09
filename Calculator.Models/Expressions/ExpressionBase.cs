using Calculator.Models.Operators;
using System.Collections.Generic;
using System;

namespace Calculator.Models
{
    public abstract class ExpressionBase 
    {
        public int Id { get; }

        protected INumber executedValue;
        public Operation CurrentOperation { get; set; }

        public string Display { get; protected set; } 

        public string ExpressionString => ToDisplayString();


        private readonly List<Operation> operations = new List<Operation>();

        protected ExpressionBase(int id, INumber initialValue)
        {
            Id = id;
            executedValue = initialValue;
            CurrentOperation = new Operation(initialValue, new NullOperator());
            Display = initialValue.ToDisplayString();
        }

        public string ToDisplayString()
        {
            //var strin = value.ToDisplayString();

            var strin = "";

            foreach (var operation in operations)
            {
                strin = strin + " " + operation.ToDisplayString();
            }

            if (CurrentOperation != null && CurrentOperation.Operator != null)

                strin = strin + " " + CurrentOperation.Operator.ToDisplayString();

            return strin.TrimStart(' ');
        }

        public void UpdateNumber(Command command)
        {
            if (!IsValidNumericCommand(command))
                return;

            var commandText = GetNumericCommandCharacter(command);
            UpdateDisplay(commandText);
            Display = CurrentOperation.Number.ToDisplayString();
        }

        public void StartNewOperation(Command command)
        {
            if (!IsValidOperatorCommand(command))
                return;
            var opr = GetOperator(command);
            CurrentOperation = new Operation(opr);
        }

        public void Complete()
        {
            var opr = GetOperator(Command.EQUAL);
            CurrentOperation = new Operation(opr);
        }

        protected abstract INumber GetNumber(string value);
        protected abstract INumber GetNumber(decimal value);
        protected abstract bool IsValidNumericCommand(Command command);
        protected abstract char GetNumericCommandCharacter(Command command);
        protected abstract void UpdateDisplay(char character);
        protected abstract bool IsValidOperatorCommand(Command command);
        protected abstract IOperator GetOperator(Command command);
        public void DeleteLastCharacter()
        {
            var display = CurrentOperation.Number.ToDisplayString();
            if (display == null)
                return;

            if (display.Length > 0)
                display = display.Remove(display.Length - 1);
            if (display == "")
                display = "0";

            CurrentOperation.Number = GetNumber(display);
            Display = CurrentOperation.Number.ToDisplayString();
        }
        public void Execute()
        {
            CurrentOperation.Execute(executedValue);
            Display = executedValue.ToDisplayString();
            operations.Add(CurrentOperation);
            
        }
    }










}
