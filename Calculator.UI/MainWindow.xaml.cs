using Calculator.UI.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using Calculator.Models;
using System.Windows.Controls;

namespace Calculator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CalculatorViewModel CalculatorViewModel;

        public MainWindow(CalculatorViewModel calculatorViewModel)
        {
            DataContext = calculatorViewModel;
            InitializeComponent();
            CalculatorViewModel = calculatorViewModel;
            SetButtonCommands();
            ResetMode();
            EnableDecMode();
         
        }

        private void SetButtonCommands()
        {
            buttonA.Command = CalculatorViewModel.ClickCommand;
            buttonA.CommandParameter = Command.A;
            buttonB.Command = CalculatorViewModel.ClickCommand;
            buttonB.CommandParameter = Command.B;
            buttonC.Command = CalculatorViewModel.ClickCommand;
            buttonC.CommandParameter = Command.C;
            buttonD.Command = CalculatorViewModel.ClickCommand;
            buttonD.CommandParameter = Command.D;
            buttonE.Command = CalculatorViewModel.ClickCommand;
            buttonE.CommandParameter = Command.E;
            buttonF.Command = CalculatorViewModel.ClickCommand;
            buttonF.CommandParameter = Command.F;
            button0.Command = CalculatorViewModel.ClickCommand;
            button0.CommandParameter = Command.ZERO;
            button1.Command = CalculatorViewModel.ClickCommand;
            button1.CommandParameter = Command.ONE;
            button2.Command = CalculatorViewModel.ClickCommand;
            button2.CommandParameter = Command.TWO;
            button3.Command = CalculatorViewModel.ClickCommand;
            button3.CommandParameter = Command.THREE;
            button4.Command = CalculatorViewModel.ClickCommand;
            button4.CommandParameter = Command.FOUR;
            button5.Command = CalculatorViewModel.ClickCommand;
            button5.CommandParameter = Command.FIVE;
            button6.Command = CalculatorViewModel.ClickCommand;
            button6.CommandParameter = Command.SIX;
            button7.Command = CalculatorViewModel.ClickCommand;
            button7.CommandParameter = Command.SEVEN;
            button8.Command = CalculatorViewModel.ClickCommand;
            button8.CommandParameter = Command.EIGHT;
            button9.Command = CalculatorViewModel.ClickCommand;
            button9.CommandParameter = Command.NINE;
            buttonPoint.Command = CalculatorViewModel.ClickCommand;
            buttonPoint.CommandParameter = Command.POINT;
            

            buttonRoot.Command = CalculatorViewModel.OperationClickCommand;
            buttonRoot.CommandParameter = Command.ROOT;
            buttonPower.Command = CalculatorViewModel.OperationClickCommand;
            buttonPower.CommandParameter = Command.POWER;
            buttonPlus.Command = CalculatorViewModel.OperationClickCommand;
            buttonPlus.CommandParameter = Command.PLUS;
            buttonMinus.Command = CalculatorViewModel.OperationClickCommand;
            buttonMinus.CommandParameter = Command.MINUS;
            buttonMultiply.Command = CalculatorViewModel.OperationClickCommand;
            buttonMultiply.CommandParameter = Command.MULTIPLY;
            buttonDivide.Command = CalculatorViewModel.OperationClickCommand;
            buttonDivide.CommandParameter = Command.DIVIDE;

            buttonEquals.Command = CalculatorViewModel.ControlClickCommand;
            buttonEquals.CommandParameter = Command.EQUAL;

            buttonClear.Command = CalculatorViewModel.ControlClickCommand;
            buttonClear.CommandParameter = Command.CLEAR;

            buttonDelete.Command = CalculatorViewModel.ControlClickCommand;
            buttonDelete.CommandParameter = Command.DELETE;





        }

        private void hexTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetMode();
            EnableHexMode();
        }

        private void decTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetMode();
            EnableDecMode();
        }

        private void octTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetMode();
            EnableOctMode();
        }

        private void binTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetMode();
            EnableBinMode();
        }

        private void EnableHexMode()
        {        
            hexLbl.Foreground = new SolidColorBrush(Colors.Blue);
            hexValue.Foreground = new SolidColorBrush(Colors.Blue);
            EnableAlphabetButtons(true);
            EnableNumberButtons(true);
            EnableRootAndPowerButtons(false);
        }

        private void EnableDecMode()
        {
            decLbl.Foreground = new SolidColorBrush(Colors.Blue);
            decValue.Foreground = new SolidColorBrush(Colors.Blue);
            EnableAlphabetButtons(false);
            EnableNumberButtons(true);
            EnableRootAndPowerButtons(true);

        }

        private void EnableOctMode()
        {
            octLbl.Foreground = new SolidColorBrush(Colors.Blue);
            octValue.Foreground = new SolidColorBrush(Colors.Blue);
            EnableAlphabetButtons(false);
            EnableNumberButtons(true);
            button8.IsEnabled = false;
            button9.IsEnabled = false;
            EnableRootAndPowerButtons(false);
        }

        private void EnableBinMode()
        {
            binLbl.Foreground = new SolidColorBrush(Colors.Blue);
            binValue.Foreground = new SolidColorBrush(Colors.Blue);
            EnableAlphabetButtons(false);
            EnableNumberButtons(false);
            button1.IsEnabled = true;
            EnableRootAndPowerButtons(false);
        }

        private void ResetMode()
        {
            hexLbl.Foreground = new SolidColorBrush(Colors.Black);
            hexValue.Foreground = new SolidColorBrush(Colors.Black);
            decLbl.Foreground = new SolidColorBrush(Colors.Black);
            decValue.Foreground = new SolidColorBrush(Colors.Black);
            octLbl.Foreground = new SolidColorBrush(Colors.Black);
            octValue.Foreground = new SolidColorBrush(Colors.Black);
            binLbl.Foreground = new SolidColorBrush(Colors.Black);
            binValue.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void EnableAlphabetButtons(bool value)
        {
            buttonA.IsEnabled = value;
            buttonB.IsEnabled = value;
            buttonC.IsEnabled = value;
            buttonD.IsEnabled = value;
            buttonE.IsEnabled = value;
            buttonF.IsEnabled = value;
        }

        private void EnableNumberButtons(bool value)
        {
            button1.IsEnabled = value;
            button2.IsEnabled = value;
            button3.IsEnabled = value;
            button4.IsEnabled = value;
            button5.IsEnabled = value;
            button6.IsEnabled = value;
            button7.IsEnabled = value;
            button8.IsEnabled = value;
            button9.IsEnabled = value;

        }


        private void EnableRootAndPowerButtons(bool value)
        {
            buttonRoot.IsEnabled = value;
            buttonPower.IsEnabled = value;
            buttonPoint.IsEnabled = value;
        }

        private void AddExpression()
        {
            foreach(var expression in CalculatorViewModel.Expressions)
            {
                StackPanel panel = new StackPanel();
                panel.Style = Resources["TextBlockMouseOverStyle"] as Style;
                panel.VerticalAlignment = VerticalAlignment.Center;

                Label expressionLbl = new Label();
                expressionLbl.Content = expression.ToString();
                expressionLbl.FontSize = 18;

                Label resultLbl = new Label();
                resultLbl.Content = expression.Display;
                resultLbl.FontSize = 24;

                panel.Children.Add(expressionLbl);
                panel.Children.Add(resultLbl);

                expressionsPanel.Children.Add(panel);
            }
           



        }

     
    }
}
