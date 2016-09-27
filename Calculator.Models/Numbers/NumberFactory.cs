using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.Numbers
{
    public interface INumberFactory
    {
        INumber GetNumber(Mode mode, string value);
    }

    public class NumberFactory : INumberFactory
    {
        public INumber GetNumber(Mode mode, string value)
        {
            switch (mode)
            {
                case Mode.DEC:
                    return new DecimalNumber(value);
                case Mode.HEX:
                    return new HexNumber(value);
                case Mode.OCT:
                    return new OctalNumber(value);
                case Mode.BIN:
                    return new BinaryNumber(value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
