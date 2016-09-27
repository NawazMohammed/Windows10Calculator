namespace Calculator.Models.Expressions
{
    using Calculator.Models.Numbers;

    public class OctalExpression : Expression
    {
        public OctalExpression(int id, OctalNumber defaultNumber, OctalNumber defaultValue)
            : base(id, defaultNumber, defaultValue)
        { }

        protected override INumber GetNumber(decimal value)
        {
            return new OctalNumber(value);
        }
    }
}
