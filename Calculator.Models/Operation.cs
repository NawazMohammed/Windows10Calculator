using Calculator.Models.Operators;

namespace Calculator.Models
{
    using System;

    using Calculator.Models.Numbers;

    public class Operation
    {
        public INumber RhsNumber { get; set; }
        public IOperator Operator { get; set; }
        public INumber LhsNumber { get; }

        public Operation(INumber lhsNumber , IOperator opr)
        {
            Operator = opr;
            LhsNumber = lhsNumber;
        }

        public string ToDisplayString()
        {
            var str = "";
            if (Operator != null)
                str = Operator.ToDisplayString();
            if (RhsNumber != null)
                str = str + " " + RhsNumber.ToDisplayString();

            return str;
        }

        public decimal Execute() 
        {
            RhsNumber.Lock();

            return Operator.Execute(LhsNumber.ToDecimal(), RhsNumber.ToDecimal());
        }
    }
}
