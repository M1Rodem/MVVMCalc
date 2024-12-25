using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp3
{
    class MainWindowViewModel : ViewModel
    {
        private string _currentInput = "0"; //Значение на экране
        private double _firstOperand;       //Первое число
        private string _operator;           //операция
        private bool _isNewInput;           //указатель ввода

        public string CurrentInput
        {
            get => _currentInput;
            set => Set(ref _currentInput, value);
        }

        public ICommand NumberCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand ClearCommand { get; }

        public MainWindowViewModel()
        {
            NumberCommand = new LambdaCommand(OnNumberCommandExecute);
            OperatorCommand = new LambdaCommand(OnOperatorCommandExecute);
            EqualsCommand = new LambdaCommand(OnEqualsCommandExecute);
            ClearCommand = new LambdaCommand(OnClearCommandExecute);
        }

        private void OnNumberCommandExecute(object parameter)
        {
            if (_isNewInput)
            {
                CurrentInput = "0";
                _isNewInput = false;
            }

            var number = parameter.ToString();
            if (CurrentInput == "0")
                CurrentInput = number;
            else
                CurrentInput += number;
        }

        private void OnOperatorCommandExecute(object parameter)
        {
            if (double.TryParse(CurrentInput, out double operand))
            {
                _firstOperand = operand;
                _operator = parameter.ToString();
                _isNewInput = true;
            }
        }

        private void OnEqualsCommandExecute(object parameter)
        {
            if (double.TryParse(CurrentInput, out double secondOperand))
            {
                double result = _firstOperand;
                switch (_operator)
                {
                    case "+":
                        result += secondOperand;
                        break;
                    case "-":
                        result -= secondOperand;
                        break;
                    case "*":
                        result *= secondOperand;
                        break;
                    case "/":
                        result /= secondOperand;
                        break;
                }

                CurrentInput = result.ToString();
                _isNewInput = true;
            }
        }
        private void OnClearCommandExecute(object parameter)
        {
            CurrentInput = "0";
            _firstOperand = 0;
            _operator = null;
        }
    }
}
