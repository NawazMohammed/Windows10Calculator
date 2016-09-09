﻿using System;

namespace Calculator.Models.Expressions
{
    using Operators;

    public class DecimalExpression : ExpressionBase
    {
        public DecimalExpression(int id, INumber initialValue)
            : base(id,initialValue)
        { }

        protected override INumber GetNumber(string value)
        {
            var number = Convert.ToDecimal(value);
            return new Dec(number);
        }

        protected override INumber GetNumber(decimal value)
        {
            return new Dec(value);
        }

        protected override void UpdateDisplay(char character)
        {
            var display = "";
            if (CurrentOperation.Number != null) display = CurrentOperation.Number.ToDisplayString();

            if (display.Length > 16)
                return;

            if (character == '.' && display.Contains("."))
                return;


            if (display == "0" && character != '.')
                display = "";

            display = display + character;

            var updatedNumber = GetNumber(display);
            CurrentOperation.Number = updatedNumber;
            
        }

        protected override char GetNumericCommandCharacter(Command command)
        {
            switch (command)
            {
                case Command.ZERO:
                    return '0';
                case Command.ONE:
                    return '1';
                case Command.TWO:
                    return '2';
                case Command.THREE:
                    return '3';
                case Command.FOUR:
                    return '4';
                case Command.FIVE:
                    return '5';
                case Command.SIX:
                    return '6';
                case Command.SEVEN:
                    return '7';
                case Command.EIGHT:
                    return '8';
                case Command.NINE:
                    return '9';
                case Command.POINT:
                    return '.';
                default:
                    throw new InvalidOperationException();
            }
        }

        protected override bool IsValidOperatorCommand(Command command)
        {
            switch (command)
            {
                case Command.PLUS:
                case Command.MINUS:
                case Command.MULTIPLY:
                case Command.DIVIDE:
                case Command.ROOT:
                case Command.POWER:
                    return true;
                default:
                    return false;
            }
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
                case Command.ROOT:
                    return new Root();
                case Command.POWER:
                    return new Power();
                case Command.EQUAL:
                    return new Equals();
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
                case Command.TWO:
                case Command.THREE:
                case Command.FOUR:
                case Command.FIVE:
                case Command.SIX:
                case Command.SEVEN:
                case Command.EIGHT:
                case Command.NINE:
                case Command.POINT:
                    return true;
                default:
                    return false;
            }
        }

    }
}
