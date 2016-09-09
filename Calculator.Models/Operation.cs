using Calculator.Models.Operators;

namespace Calculator.Models
{
    using System;

    public class Operation
    {
        public INumber Number { get; set; }
        public IOperator Operator { get; }

        public Operation(IOperator opr)
        {
            Operator = opr;
        }

        public Operation(INumber number, IOperator opr)
        {
            Number = number;
            Operator = opr;
        }

        public string ToDisplayString()
        {
            var str = "";
            if (Operator != null)
                str = Operator.ToDisplayString();
            if (Number != null)
                str = str + " " + Number.ToDisplayString();

            return str;
        }

        public void Execute(INumber total)
        {
            var result = Operator.Execute(total.ToDecimal(), Number.ToDecimal());

            total.SetValue(result);
        }

      
    }

}
