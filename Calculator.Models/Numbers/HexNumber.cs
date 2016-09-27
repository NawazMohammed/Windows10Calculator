namespace Calculator.Models.Numbers
{
    using System;

    public class HexNumber : NumberBase
    {
        private int value;

        public HexNumber(decimal value)
        {
            this.value = Convert.ToInt32(value);
            TempValue = Convert.ToString(this.value, 16);
        }

        public HexNumber(string value)
        {
            this.value = Convert.ToInt32(value, 16);
            TempValue = value;
        }

        public override void AddCharacter(char character)
        {
            if (TempValue.Length > 64)
                return;

            if (TempValue == "0" && character == '0')
                return;

            if (TempValue == "0" && character != '0')
                TempValue = "";

            TempValue = TempValue + character;
        }

        public override void Lock()
        {
            IsLocked = true;
            value = value = Convert.ToInt32(TempValue, 16);
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return IsLocked ? Convert.ToString(value, 16).ToUpper() : TempValue.ToUpper();
        }
    }
}
