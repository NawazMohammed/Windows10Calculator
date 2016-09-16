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
            tempValue = value;
        }

        public Binary(decimal value)
        {
            this.value = Convert.ToInt32(value);
            tempValue = Convert.ToString(this.value, 2);
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
            value = value = Convert.ToInt32(tempValue, 2);
        }

        public override  decimal ToDecimal()
        {
            return Convert.ToDecimal(value);
        }

        public override string ToDisplayString()
        {
            return isLocked ? Convert.ToString(value, 2) : tempValue;
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
    }

}
