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

        public override void SetValue(string val)
        {
            value = Convert.ToInt32(val, 8);
        }

        public override void AddCharacter(char character)
        {
            var display = ToDisplayString();
            if (display.Length > 64)
                return;

            if (display == "0" && character == '0')
                return;

            if (display == "0" && character != '0')
                display = "";

            display = display + character;

            value = Convert.ToInt32(display, 8);
        }

        public Octal(string b)
        {
            value = Convert.ToInt32(b, 8);
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return Convert.ToString(value, 8);
        }
    }

   



}
