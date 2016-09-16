using Calculator.Models.Operators;
using System.Collections.Generic;

namespace Calculator.Models
{
    using System.Linq;

    public abstract class ExpressionBase 
    {
        public int Id { get; }

        protected INumber ExecutedValue;
        public Operation CurrentOperation { get; set; }

        public string Display { get; protected set; } 

        public string ExpressionString => ToDisplayString();


        private List<Operation> operations = new List<Operation>();

        protected ExpressionBase(int id, INumber defaultNumber, INumber defaultValue)
        {
            Id = id;
            CurrentOperation = new Operation(defaultValue, new NullOperator()) { RhsNumber = defaultNumber };
            ExecutedValue = defaultValue;
            Display = ExecutedValue.ToDisplayString();
        }

        public string ToDisplayString()
        {
            var strin = "";

            foreach (var operation in operations)
            {
                if(operations.Count == 1  &&  operation.Operator.GetType() == typeof(NullOperator) && CurrentOperation == null)
                    continue;

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

            if (CurrentOperation.RhsNumber == null)
                CurrentOperation.RhsNumber = GetNumber(commandText.ToString());
            else
                CurrentOperation.RhsNumber.AddCharacter(commandText);

            Display = CurrentOperation.RhsNumber.ToDisplayString();
        }

        public void StartNewOperation(Command command)
        {
            if (!IsValidOperatorCommand(command))
                return;
            var opr = GetOperator(command);

            CurrentOperation = new Operation(ExecutedValue, opr);
        }

        public void Complete()
        {
            var opr = GetOperator(Command.EQUAL);
            CurrentOperation = new Operation(ExecutedValue,opr);
        }

        protected abstract INumber GetNumber(string value);
        protected abstract INumber GetNumber(decimal value);
        protected abstract bool IsValidNumericCommand(Command command);
        protected abstract char GetNumericCommandCharacter(Command command);
        protected abstract bool IsValidOperatorCommand(Command command);
        protected abstract IOperator GetOperator(Command command);
        public void DeleteLastCharacter()
        {
            CurrentOperation.RhsNumber.DeleteLastCharacter();
            Display = CurrentOperation.RhsNumber.ToDisplayString();
        }
        public void ExecutePreviousOperation()
        {
            if (CurrentOperation.Operator == null || CurrentOperation.RhsNumber == null || CurrentOperation.LhsNumber == null)
                return;

            ExecutedValue = CurrentOperation.Execute(GetNumber);
            Display = ExecutedValue.ToDisplayString();   
            operations.Add(CurrentOperation);     
        }
    }










}
