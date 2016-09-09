using System;
using Calculator.Models.Bases;

namespace Calculator.Models
{
    public class Octal: Number
    {
        private  int value;

        public Octal(decimal value)
        {
            this.value = Convert.ToInt32(value);
        }

        public Octal(int value)
        {
            this.value = value;
        }

        public override void SetValue(decimal val)
        {
            value = Convert.ToInt32(val);
        }

        public Octal(string b)
        {
            value = Convert.ToInt32(b, 8);
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(this.value);
        }

        public override string ToDisplayString()
        {
            return Convert.ToString(value, 8);
        }
    }

   



}
