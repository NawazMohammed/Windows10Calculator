namespace Calculator.Models.Numbers
{
    using System;

    public class BinaryNumber : NumberBase
    {
        private int value;
        public BinaryNumber(string value)
        {
            this.value = Convert.ToInt32(value, 2);
            TempValue = value;
        }

        public BinaryNumber(decimal value)
        {
            this.value = Convert.ToInt32(value);
            TempValue = Convert.ToString(this.value, 2);
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
            value = value = Convert.ToInt32(TempValue, 2);
        }

        public override void SetValue(decimal val)
        {
            value = Convert.ToInt32(val);
            TempValue = Convert.ToString(value, 2);
        }

        public override decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return IsLocked ? Convert.ToString(value, 2) : TempValue;
        }
    } 

    public interface INumber
    {
        decimal ToDecimal();
        string ToDisplayString();

        //void SetValue(decimal val);

        //void SetValue(string val);

        void AddCharacter(char car);

        void DeleteLastCharacter();

        void Lock();

        void SetValue(decimal val);
    }

}
