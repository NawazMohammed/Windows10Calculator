namespace Calculator.Models.Numbers
{

    public abstract class NumberBase : INumber
    {
        protected string TempValue;
        protected bool IsLocked;
        public abstract decimal ToDecimal();

        public abstract string ToDisplayString();

        public abstract void AddCharacter(char car);
        public abstract void Lock();

        public abstract void SetValue(decimal val);
  

        public void DeleteLastCharacter()
        {
            if (TempValue == null)
                return;

            if (TempValue.Length > 0)
                TempValue = TempValue.Remove(TempValue.Length - 1);
            if (TempValue == "")
                TempValue = "0";
        }
    }
}
