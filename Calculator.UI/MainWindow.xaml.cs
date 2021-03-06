﻿using Calculator.UI.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace Calculator.UI
{
    using Calculator.Models.Commands;

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
            buttonA.Command = CalculatorViewModel.NumericCommandClick;
            buttonA.CommandParameter = NumericCommandType.A;
            buttonB.Command = CalculatorViewModel.NumericCommandClick;
            buttonB.CommandParameter = NumericCommandType.B;
            buttonC.Command = CalculatorViewModel.NumericCommandClick;
            buttonC.CommandParameter = NumericCommandType.C;
            buttonD.Command = CalculatorViewModel.NumericCommandClick;
            buttonD.CommandParameter = NumericCommandType.D;
            buttonE.Command = CalculatorViewModel.NumericCommandClick;
            buttonE.CommandParameter = NumericCommandType.E;
            buttonF.Command = CalculatorViewModel.NumericCommandClick;
            buttonF.CommandParameter = NumericCommandType.F;
            button0.Command = CalculatorViewModel.NumericCommandClick;
            button0.CommandParameter = NumericCommandType.ZERO;
            button1.Command = CalculatorViewModel.NumericCommandClick;
            button1.CommandParameter = NumericCommandType.ONE;
            button2.Command = CalculatorViewModel.NumericCommandClick;
            button2.CommandParameter = NumericCommandType.TWO;
            button3.Command = CalculatorViewModel.NumericCommandClick;
            button3.CommandParameter = NumericCommandType.THREE;
            button4.Command = CalculatorViewModel.NumericCommandClick;
            button4.CommandParameter = NumericCommandType.FOUR;
            button5.Command = CalculatorViewModel.NumericCommandClick;
            button5.CommandParameter = NumericCommandType.FIVE;
            button6.Command = CalculatorViewModel.NumericCommandClick;
            button6.CommandParameter = NumericCommandType.SIX;
            button7.Command = CalculatorViewModel.NumericCommandClick;
            button7.CommandParameter = NumericCommandType.SEVEN;
            button8.Command = CalculatorViewModel.NumericCommandClick;
            button8.CommandParameter = NumericCommandType.EIGHT;
            button9.Command = CalculatorViewModel.NumericCommandClick;
            button9.CommandParameter = NumericCommandType.NINE;
            buttonPoint.Command = CalculatorViewModel.NumericCommandClick;
            buttonPoint.CommandParameter = NumericCommandType.POINT;
            

            buttonRoot.Command = CalculatorViewModel.OperatorCommandClick;
            buttonRoot.CommandParameter = OperatorCommandType.ROOT;
            buttonPower.Command = CalculatorViewModel.OperatorCommandClick;
            buttonPower.CommandParameter = OperatorCommandType.POWER;
            buttonPlus.Command = CalculatorViewModel.OperatorCommandClick;
            buttonPlus.CommandParameter = OperatorCommandType.PLUS;
            buttonMinus.Command = CalculatorViewModel.OperatorCommandClick;
            buttonMinus.CommandParameter = OperatorCommandType.MINUS;
            buttonMultiply.Command = CalculatorViewModel.OperatorCommandClick;
            buttonMultiply.CommandParameter = OperatorCommandType.MULTIPLY;
            buttonDivide.Command = CalculatorViewModel.OperatorCommandClick;
            buttonDivide.CommandParameter = OperatorCommandType.DIVIDE;

            buttonEquals.Command = CalculatorViewModel.ControlCommandClick;
            buttonEquals.CommandParameter = ControlCommand.EQUAL;

            buttonClear.Command = CalculatorViewModel.ControlCommandClick;
            buttonClear.CommandParameter = ControlCommand.CLEAR;

            buttonDelete.Command = CalculatorViewModel.ControlCommandClick;
            buttonDelete.CommandParameter = ControlCommand.DELETE;





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
