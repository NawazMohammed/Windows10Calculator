using System;
using Calculator.Models.Bases;

namespace Calculator.Models
{
   
    public class Dec:Number
    {
        private decimal value;

        public Dec(decimal value)
        {
            this.value = value;
        }

        public Dec(string b)
        {
            value = Convert.ToDecimal(b);
        }

        public override void SetValue(decimal val)
        {
            value = val;
        }

        public override decimal ToDecimal()
        {
            return value;
        }

        public override string ToDisplayString()
        {
            return value.ToString("G29");
        }

    }
}
