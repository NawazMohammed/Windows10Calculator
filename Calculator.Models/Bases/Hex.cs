using System;
using Calculator.Models.Bases;

namespace Calculator.Models
{
    public class Hex:Number
    {
        private  int value;

        public Hex(decimal value)
        {
            this.value = Convert.ToInt32(value);
        }

        public Hex(int value)
        {
            this.value = value;
        }

        public override void SetValue(decimal val)
        {
            value = Convert.ToInt32(val);
        }

        public Hex(string b)
        {
            value = Convert.ToInt32(b, 16);
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
             return Convert.ToString(value, 16);
        }
    }
}
