namespace Calculator.Models.Bases
{
    public abstract class Number : INumber
    {
        public abstract decimal ToDecimal();

        public abstract string ToDisplayString();

        public abstract void SetValue(decimal val);
    }
}
