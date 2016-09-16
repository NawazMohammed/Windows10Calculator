namespace Calculator.Models.Numbers
{
    using System;

    public class Octal: Number
    {
        private  int value;

        public Octal(decimal value)
        {
            this.value = Convert.ToInt32(value);
            tempValue = Convert.ToString(this.value, 8);
        }
        public Octal(string val)
        {
            value = Convert.ToInt32(val, 8);
            tempValue = val;
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
            value = value = Convert.ToInt32(tempValue, 8);
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return isLocked ? Convert.ToString(value, 8) : tempValue;
        }
    }

   



}
