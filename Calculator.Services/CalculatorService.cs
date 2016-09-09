﻿using Calculator.Contracts;
using System.Collections.Generic;
using Calculator.Models;
using Calculator.Contracts.ServiceContracts;

namespace Calculator.Services
{
    using System;

    using Calculator.Models.Expressions;
    using Calculator.Models.Operators;

    public class CalculatorService : ICalculatorService
    {
        private readonly IServiceFactory serviceFactory;
        private Mode currrentMode;
        private int expressionId;
        public CalculatorService()
        {
            this.serviceFactory = serviceFactory;
            CurrentMode = Mode.DEC;
            Expressions = new List<ExpressionBase>();
        }

        public int ExpressionId
        {
            get
            {
                expressionId = expressionId + 1;
                return expressionId;
            }
        }
        public Mode CurrentMode
        {
            get
            {
                return currrentMode;
            }
            set
            {
                currrentMode = value;

                Expression = new DecimalExpression(ExpressionId, new Dec(0));
            }
        }
        public List<ExpressionBase> Expressions { get; }

        public ExpressionBase Expression { get; private set; }
        public string GetResultInFormat(Mode mode)
        {
            switch (mode)
            {
                //case Mode.DEC:
                //    return DisplayResult;
                //case Mode.HEX:
                //    return decimal.Parse(DisplayResult).ToHex(); 
                //case Mode.OCT:
                //    return decimal.Parse(DisplayResult).ToOct();
                //case Mode.BIN:
                //    return decimal.Parse(DisplayResult).ToBinary();
                default:
                    return "0";
            }
        }
        public void OnNumericCommand(Command command)
        {
            Expression.UpdateNumber(command);
        }
        public void OnOperatorCommand(Command command)
        {
            Expression.Execute();
            Expression.StartNewOperation(command);
        }
        public void OnControlCommand(Command command)
        {
            if (!IsValidControlCommand(command))
                return;

            switch (command)
            {
                case Command.DELETE:
                    Expression.DeleteLastCharacter();
                    break;
                case Command.EQUAL:
                    Expression.Execute();
                    Expression.Complete();
                    Expressions.Add(Expression);
                    Expression = new DecimalExpression(ExpressionId, new Dec(Expression.Display));
                    break;
                case Command.CLEAR:
                    Expression.Reset();
                    break;
            }
        }

        private static bool IsValidControlCommand(Command command)
        {
            switch (command)
            {
                case Command.CLEAR:
                case Command.DELETE:
                case Command.EQUAL:
                    return true;
                default:
                    return false;
            }

        }
    }
}
