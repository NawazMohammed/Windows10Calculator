namespace Calculator.Models.Numbers
{
    using System;

    public class Dec:Number
    {
        private decimal value;

        public Dec(decimal value)
        {
            this.value = value;
            tempValue = value.ToString("G29");
        }

        public Dec(string val)
        {
            value = Convert.ToDecimal(val);
            tempValue = val;
        }

        public override void AddCharacter(char character)
        {
            if (tempValue.Length > 16)
                return;

            if (character == '.' && tempValue.Contains("."))
                return;


            if (tempValue == "0" && character != '.')
                tempValue = "";

            tempValue = tempValue + character;

        }

        public override void Lock()
        {
            isLocked = true;
            value = Convert.ToDecimal(tempValue);
        }

        public override decimal ToDecimal()
        {         
            return value;
        }

        public override string ToDisplayString()
        {
            return isLocked ? value.ToString("G29") : tempValue;
        }
    }
}
