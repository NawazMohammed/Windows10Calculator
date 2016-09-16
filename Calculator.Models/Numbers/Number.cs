namespace Calculator.Models.Numbers
{

    public abstract class Number : INumber
    {
        protected string tempValue;
        protected bool isLocked;
        public abstract decimal ToDecimal();

        public abstract string ToDisplayString();

        //public abstract void SetValue(decimal val);

        //public abstract void SetValue(string val);

        public abstract void AddCharacter(char car);
        public abstract void Lock();

        public void DeleteLastCharacter()
        {
            if (tempValue == null)
                return;

            if (tempValue.Length > 0)
                tempValue = tempValue.Remove(tempValue.Length - 1);
            if (tempValue == "")
                tempValue = "0";

            //SetValue(tempValue);
        }
    }
}
