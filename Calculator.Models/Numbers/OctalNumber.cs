using System;

namespace Calculator.Models.Numbers
{ 
    public class OctalNumber : NumberBase
    {
        private int value;

        public OctalNumber(decimal value)
        {
            this.value = Convert.ToInt32(value);
            TempValue = Convert.ToString(this.value, 8);
        }

        public OctalNumber(string val)
        {
            value = Convert.ToInt32(val, 8);
            TempValue = val;
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
            value = value = Convert.ToInt32(TempValue, 8);
        }

        public override void SetValue(decimal val)
        {
             value = Convert.ToInt32(val);
            TempValue = Convert.ToString(value, 8);
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return IsLocked ? Convert.ToString(value, 8) : TempValue;
        }
    }
}
