namespace Calculator.Models.Expressions
{
    using System.Collections.Generic;

    using Calculator.Models.Commands;
    using Calculator.Models.Numbers;
    using Calculator.Models.Operators;

    public abstract class Expression 
    {
        public int Id { get; }

        protected INumber ExecutedValue;
        public Operation CurrentOperation { get; set; }

        public string Display { get; protected set; } 

        public string ExpressionString => ToDisplayString();


        private List<Operation> operations = new List<Operation>();

        protected Expression(int id, INumber defaultNumber, INumber defaultValue)
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
                //if(operations.Count == 1  &&  operation.Operator.GetType() == typeof(NullOperator) && CurrentOperation == null)
                //    continue;

                strin = strin + " " + operation.ToDisplayString();
            }

            if (CurrentOperation != null && CurrentOperation.Operator != null)

                strin = strin + " " + CurrentOperation.Operator.ToDisplayString();

            return strin.TrimStart(' ');
        }

        public void UpdateNumber(INumericCommand command)
        {
            if (CurrentOperation.RhsNumber == null)
                CurrentOperation.RhsNumber = GetNumber(command.Value);
            else
                CurrentOperation.RhsNumber.AddCharacter(command.Name);

            Display = CurrentOperation.RhsNumber.ToDisplayString();
        }

        public void AddOrUpdateOperator(IOperator opr)
        {
            CurrentOperation = new Operation(ExecutedValue, opr);
        }

        public void Complete()
        {
            var opr = new Equals();
            CurrentOperation = new Operation(ExecutedValue, opr);
        }

        //protected abstract INumber GetNumber(string value);
        protected abstract INumber GetNumber(decimal value);
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
