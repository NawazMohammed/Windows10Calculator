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

        public Dec(string val)
        {
            value = Convert.ToDecimal(val);
        }

        public override void SetValue(decimal val)
        {
            value = val;
        }

        public override void SetValue(string val)
        {
            value = Convert.ToDecimal(val);
        }

        public override void AddCharacter(char character)
        {
            var display = ToDisplayString();

            if (display.Length > 16)
                return;

            if (character == '.' && display.Contains("."))
                return;


            if (display == "0" && character != '.')
                display = "";

            display = display + character;

            value = Convert.ToDecimal(display);

        }

        public void DeleteLastCharacter()
        {
            
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
