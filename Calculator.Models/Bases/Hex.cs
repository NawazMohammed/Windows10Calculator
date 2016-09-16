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
            tempValue = Convert.ToString(this.value, 16);
        }

        public Hex(int value)
        {
            this.value = value;
        }

       
        public override void AddCharacter(char character)
        {
            if (tempValue.Length > 64)
                return;

            if (tempValue == "0" && character == '0')
                return;

            if (tempValue == "0" && character != '0')
                tempValue = "";

            tempValue = tempValue + character;
        }

        public override void Lock()
        {
            isLocked = true;
            value = value = Convert.ToInt32(tempValue, 16);
        }

        public Hex(string b)
        {
            value = Convert.ToInt32(b, 16);
            tempValue = b;
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return isLocked ? Convert.ToString(value, 16).ToUpper() : tempValue.ToUpper();
        }
    }
}
