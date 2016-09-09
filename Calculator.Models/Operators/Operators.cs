using System;

namespace Calculator.Models.Operators
{
    public interface IOperator
    {
        decimal Execute(decimal x, decimal y);

        string ToDisplayString();
    }

    public class Add : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            return x + y;
        }

        public string ToDisplayString()
        {
            return "+";
        }
    }


    public class Substract : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            return x - y;
        }

        public string ToDisplayString()
        {
            return "-";
        }
    }

    public class Multiply : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            return x * y;
        }

        public string ToDisplayString()
        {
            return "*";
        }
    }

    public class Divide : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            return x / y;
        }

        public string ToDisplayString()
        {
            return "÷";
        }
    }

    public class Root : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            return (decimal)Math.Pow(Convert.ToDouble(x), 1.0 / Convert.ToInt64(y));
        }

        public string ToDisplayString()
        {
            return "yroot";
        }
    }

    public class Power : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            return (decimal)Math.Pow(Convert.ToDouble(x), Convert.ToDouble(y));
        }

        public string ToDisplayString()
        {
            return "^";
        }
    }

    public class Equals : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            throw new NotImplementedException();
        }

        public string ToDisplayString()
        {
            return "=";
        }
    }

    public class NullOperator : IOperator
    {
        public decimal Execute(decimal x, decimal y)
        {
            return y;
        }

        public string ToDisplayString()
        {
            return "";
        }
    }
}
