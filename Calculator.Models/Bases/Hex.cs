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

        public override void SetValue(string val)
        {
            value = Convert.ToInt32(val, 16);
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

            value = Convert.ToInt32(display, 16);
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
             return Convert.ToString(value, 16).ToUpper();
        }
    }
}
