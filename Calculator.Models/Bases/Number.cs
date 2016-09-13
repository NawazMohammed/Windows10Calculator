namespace Calculator.Models.Bases
{

    public abstract class Number : INumber
    {
        public abstract decimal ToDecimal();

        public abstract string ToDisplayString();

        public abstract void SetValue(decimal val);

        public abstract void SetValue(string val);

        public abstract void AddCharacter(char car);

        public void DeleteLastCharacter()
        {
            var display = ToDisplayString();
            if (display == null)
                return;

            if (display.Length > 0)
                display = display.Remove(display.Length - 1);
            if (display == "")
                display = "0";

            SetValue(display);
        }
    }
}
