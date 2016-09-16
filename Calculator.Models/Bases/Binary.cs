using System;
using Calculator.Models.Bases;

namespace Calculator.Models
{
    public class Binary : Number
    {
        private int value;
        public Binary(string value)
        {
            this.value = Convert.ToInt32(value, 2);
        }

        public Binary(decimal value)
        {
            this.value = Convert.ToInt32(value);
        }

        public override void SetValue(decimal val)
        {
            value = Convert.ToInt32(value);
        }

        public override void SetValue(string val)
        {
            value = Convert.ToInt32(val, 2);
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

            value = Convert.ToInt32(display, 2);
        }

        public override  decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return Convert.ToString(value, 2);
        }
    } 

    
    public interface INumber
    {
         decimal ToDecimal();
        string ToDisplayString();

        void SetValue(decimal val);

        void SetValue(string val);

        void AddCharacter(char car);

        void DeleteLastCharacter();

    }

}
