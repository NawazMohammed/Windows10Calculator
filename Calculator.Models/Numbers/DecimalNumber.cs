namespace Calculator.Models.Numbers
{
    using System;

    public class DecimalNumber : NumberBase
    {
        private decimal value;

        public DecimalNumber(decimal value)
        {
            this.value = value;
            TempValue = value.ToString("G29");
        }

        public DecimalNumber(string val)
        {
            value = Convert.ToDecimal(val);
            TempValue = val;
        }

        public override void AddCharacter(char character)
        {
            if (TempValue.Length > 16)
                return;

            if (character == '.' && TempValue.Contains("."))
                return;


            if (TempValue == "0" && character != '.')
                TempValue = "";

            TempValue = TempValue + character;

        }

        public override void Lock()
        {
            IsLocked = true;
            value = Convert.ToDecimal(TempValue);
        }

        public override void SetValue(decimal val)
        {
            value = val;
            TempValue = value.ToString("G29");
        }

        public override decimal ToDecimal()
        {         
            return value;
        }

        public override string ToDisplayString()
        {
            return IsLocked ? value.ToString("G29") : TempValue;
        }
    }
}
