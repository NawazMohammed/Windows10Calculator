using Calculator.Contracts;
using Calculator.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator.UI.ViewModels
{
    using Calculator.Models.Expressions;

    public class CalculatorViewModel: INotifyPropertyChanged
    {
        private readonly ICalculatorService calculatorService;
        private ICommand clickCommand;
        private ICommand operationClickCommand;
        private ICommand controlClickCommand;
        private ICommand expressionClickCommand;
        private bool _canExecute = true;
        public Mode Mode { get; set; }
        public Command Command { get; set; }

        public CalculatorViewModel(ICalculatorService calculatorService)
        {
            this.calculatorService = calculatorService;
        }
        
        public ICommand ClickCommand
        {
            get
            {
                return clickCommand ?? (clickCommand = new ClickCommand(param => OnNumericCommandClick(param), _canExecute));
            }
        }

        public ICommand OperationClickCommand
        {
            get
            {
                return operationClickCommand ?? (operationClickCommand = new ClickCommand(param => OnOperatorCommandClick(param), _canExecute));
            }
        }

        public ICommand ControlClickCommand
        {
            get
            {
                return controlClickCommand ?? (controlClickCommand = new ClickCommand(param => OnControlCommandClick(param), _canExecute));
            }
        }

        public ICommand ExpressionClickCommand
        {
            get
            {
                return expressionClickCommand ?? (expressionClickCommand = new ClickCommand(param => OnExpressionClick(param), _canExecute));
            }
        }

        public void OnNumericCommandClick(object command)
        {
            var com = command.ToEnum<Command>();
            calculatorService.OnNumericCommand(com);
            Result = calculatorService.Expression.Display;
        }

        public void OnOperatorCommandClick(object command)
        {
            var com = command.ToEnum<Command>();
            calculatorService.OnOperatorCommand(com);
            Result = calculatorService.Expression.Display;
            Expression = calculatorService.Expression.ExpressionString;
        }

        public void OnControlCommandClick(object command)
        {
            var com = command.ToEnum<Command>();
            calculatorService.OnControlCommand(com);
            Result = calculatorService.Expression.Display;
            Expression = calculatorService.Expression.ExpressionString;

            if (Expressions == null)
                Expressions = new ObservableCollection<ExpressionBase>();
            Expressions.Clear();
            foreach (var expression in calculatorService.Expressions)
            {
                Expressions.Add(expression);
            }
            NotifyPropertyChanged("Expressions");

        }

        public void OnExpressionClick(object id)
        {
            var expressionId = Convert.ToInt32(id);
            //_calculatorService.HandleCommand(com);
            //CalculatorOutput = _calculatorService.DisplayResult;
            //CalculatorOutputBin = _calculatorService.GetResultInFormat(Mode.BIN);
            //CalculatorOutputHex = _calculatorService.GetResultInFormat(Mode.HEX);
            //CalculatorOutputOct = _calculatorService.GetResultInFormat(Mode.OCT);

            //CurrentExpression = _calculatorService.DisplayExpression;

            //if (Expressions == null)
            //    Expressions = new ObservableCollection<Expression>();
            //Expressions.Clear();
            //foreach (var expression in _calculatorService.Expressions)
            //{
            //    Expressions.Add(expression);
            //}
            //NotifyPropertyChanged("Expressions");
        }

       // private List<Expression> _expressions = new List<Expression>() { new Expression() { _display = "8", _items = new List<ExpressionItem> { new ExpressionItem() {Type = ExpressionItemType.NUMBER, ItemString ="8",NumberValue = 8 }, new ExpressionItem() { Type=ExpressionItemType.OPERATOR,OperatorValue = Command.EQUAL} } } };
        private readonly ObservableCollection<ExpressionBase> expressions = new ObservableCollection<ExpressionBase>();

        public ObservableCollection<ExpressionBase> Expressions
        {
            get
            {
                return expressions;
            }

          set
            {
                expressions.Clear();
                foreach (var expression in value)
                {
                    expressions.Add(expression);
                }
                NotifyPropertyChanged();
            }
        }

        public string Expression
        {
            get { return calculatorService.Expression.ExpressionString; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                NotifyPropertyChanged();
            }
        }

        public string Result
        {
            get { return calculatorService.Expression.Display; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                NotifyPropertyChanged();
            }
        }

        private string calculatorOutputHex;
        public string CalculatorOutputHex
        {
            get { return calculatorOutputHex; }
            set
            {
                calculatorOutputHex = value;
                NotifyPropertyChanged();
            }
        }

        private string _calculatorOutputDec;
        public string CalculatorOutputDec
        {
            get { return _calculatorOutputDec; }
            set
            {
                _calculatorOutputDec = value;
                NotifyPropertyChanged();
            }
        }

        private string _calculatorOutputBin;
        public string CalculatorOutputBin
        {
            get { return _calculatorOutputBin; }
            set
            {
                _calculatorOutputBin = value;
                NotifyPropertyChanged();
            }
        }

        private string _calculatorOutputOct;
        public string CalculatorOutputOct
        {
            get { return _calculatorOutputOct; }
            set
            {
                _calculatorOutputOct = value;
                NotifyPropertyChanged();
            }
        }   

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
