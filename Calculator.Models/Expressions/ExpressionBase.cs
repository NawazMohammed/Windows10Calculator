namespace Calculator.Models.Expressions
{
    using System.Collections.Generic;

    using Calculator.Models.Commands;
    using Calculator.Models.Numbers;
    using Calculator.Models.Operators;

    public abstract class Expression 
    {
        public int Id { get; }

        private INumber executedValue;
        public Operation CurrentOperation { get; set; }

        public string Display { get; protected set; } 

        public string ExpressionString => ToDisplayString();


        private readonly List<Operation> operations = new List<Operation>();

        protected Expression(int id, INumber defaultNumber, INumber defaultValue)
        {
            Id = id;
            CurrentOperation = new Operation(defaultValue, new NullOperator()) { RhsNumber = defaultNumber };
            executedValue = defaultValue;
            Display = executedValue.ToDisplayString();
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
            CurrentOperation = new Operation(executedValue, opr);
        }

        public void Complete()
        {
            var opr = new Equals();
            CurrentOperation = new Operation(executedValue, opr);
        }

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

            executedValue = CurrentOperation.Execute(GetNumber);
            Display = executedValue.ToDisplayString();
            operations.Add(CurrentOperation);     
        }
    }
}
