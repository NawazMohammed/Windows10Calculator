﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.Expressions
{
    using Calculator.Models.Operators;

    public class HexExpression : ExpressionBase
    {
        public HexExpression(int id)
            : base(id, new Hex(0))
        { }

        protected override INumber GetNumber(string value)
        {
            return new Hex(value);
        }

        protected override INumber GetNumber(decimal value)
        {
            return new Hex(value);
        }

        protected override IOperator GetOperator(Command command)
        {
            switch (command)
            {
                case Command.PLUS:
                    return new Add();
                case Command.MINUS:
                    return new Substract();
                case Command.MULTIPLY:
                    return new Multiply();
                case Command.DIVIDE:
                    return new Divide();
                default:
                    throw new InvalidOperationException();
            }
        }
        protected override bool IsValidNumericCommand(Command command)
        {
            switch (command)
            {
                case Command.ZERO:
                case Command.ONE:
                    return true;
                default:
                    return false;
            }
        }
        protected override char GetNumericCommandCharacter(Command command)
        {
            switch (command)
            {
                case Command.ZERO:
                    return '0';
                case Command.ONE:
                    return '1';
                default:
                    throw new InvalidOperationException();
            }
        }
        protected override void UpdateDisplay(char character)
        {
            var display = CurrentOperation.Number.ToDisplayString();
            if (display.Length > 64)
                return;

            if (display == "0" && character == '0')
                return;

            if (display == "0" && character != '0')
                display = "";

            display = display + character;

            CurrentOperation.Number = GetNumber(display);

        }
        protected override bool IsValidOperatorCommand(Command command)
        {
            switch (command)
            {
                case Command.PLUS:
                case Command.MINUS:
                case Command.MULTIPLY:
                case Command.DIVIDE:
                    return true;
                default:
                    return false;
            }
        }

    }
}
