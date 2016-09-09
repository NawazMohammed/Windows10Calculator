using System;
using Calculator.Contracts.ServiceContracts;
using Calculator.Models;

namespace Calculator.Services
{
    public class ServiceFactory : IServiceFactory
    {
        public IService GetService(Mode mode)
        {
            switch (mode)
            {
                case Mode.DEC:
                    return new DecimalService();                   
                case Mode.BIN:
                    return new BinaryService();
                case Mode.HEX:
                    return new HexadecimalService();
                case Mode.OCT:
                    return new OctalService();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}
