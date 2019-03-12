using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            CalculateEquation();
        }
        
        private void CalculateEquation()
        {
            var userInput = UserInputText.Text;

            this.CalculationResultText.Text = ParseOperation();

            FocusInputText();
        }

        private string ParseOperation()
        {
            try
            {
                var userInput = this.UserInputText.Text;

                userInput = userInput.Replace(" ", "");

                var operation = new Operation();
                var leftSide = true;

                var usedSymbols = "0123456789.";
                var usedOperators = "+-*/";

                for (int i = 0; i < userInput.Length; ++i)
                {
                    if(usedSymbols.Any(c => userInput[i] == c))
                    {
                        if (leftSide)
                            operation.LeftSide = AddNumberPart(operation.LeftSide, userInput[i]);
                        else
                            operation.RightSide = AddNumberPart(operation.RightSide, userInput[i]);
                    }
                    else if (usedOperators.Any(c => userInput[i] == c))
                    {
                        if (!leftSide)
                        {
                            var operatorType = GetOperationType(userInput[i]);

                            if (operation.RightSide.Length == 0)
                            {
                                if (operatorType != OperationType.Minus)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an right side number");

                                operation.RightSide += userInput[i];
                            }
                            else
                            {
                                operation.LeftSide = CalculateOperation(operation);

                                operation.OperationType = operatorType;

                                operation.RightSide = string.Empty;
                            }
                        }
                        else
                        {
                            var operatorType = GetOperationType(userInput[i]);

                            if (operation.LeftSide.Length == 0)
                            {
                                if (operatorType != OperationType.Minus)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an left side number");

                                operation.LeftSide += userInput[i];
                            }
                            else
                            {
                                operation.OperationType = operatorType;

                                leftSide = false;
                            }
                        }
                    }
                }

                return CalculateOperation(operation);
            }
            catch(Exception e)
            {
                return $"Invalid equation. {e.Message}";
            }
        }

        private string CalculateOperation(Operation operation)
        {
            if (string.IsNullOrEmpty(operation.LeftSide) || !decimal.TryParse(operation.LeftSide, out decimal left))
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.LeftSide}");

            if (string.IsNullOrEmpty(operation.RightSide) || !decimal.TryParse(operation.RightSide, out decimal right))
                throw new InvalidOperationException($"Right side of the operation was not a number. {operation.RightSide}");

            try
            {
                switch(operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();

                    case OperationType.Minus:
                        return (left - right).ToString();

                    case OperationType.Multiply:
                        return (left * right).ToString();

                    case OperationType.Divide:
                        return (left / right).ToString();

                    default:
                        throw new InvalidOperationException($"Unknown operator type during the calculation. {operation.OperationType}");
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException($"Failed to calculate operation {operation.LeftSide} {operation.OperationType} {operation.RightSide}. {e.Message}");
            }
        }

        private OperationType GetOperationType(char character)
        {
            switch(character)
            {
                case '+':
                    return OperationType.Add;

                case '-':
                    return OperationType.Minus;

                case '/':
                    return OperationType.Divide;

                case '*':
                    return OperationType.Multiply;

                default:
                    throw new InvalidOperationException($"Unknown operator type { character }");
            }
        }

        private string AddNumberPart(string currentNumber, char newCharacter)
        {
            if (newCharacter == '.' && currentNumber.Contains('.'))
                throw new InvalidOperationException($"Number {currentNumber} already contains a . and another cannot be added");

            return currentNumber + newCharacter;
        }
       
        #region Text manipulating methods
        private void InputSymbolButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
                return;

            string s = (sender as Button).Text;
            InsertTextValue(s);

            FocusInputText();
        }

        private void CEButton_Click(object sender, EventArgs e)
        {
            this.UserInputText.Clear();
            FocusInputText();
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            DeleteTextValue();
            FocusInputText();
        }

        private void FocusInputText()
        {
            this.UserInputText.Focus();
        }

        private void InsertTextValue(string value)
        {
            var selectionStart = this.UserInputText.SelectionStart;

            this.UserInputText.Text = this.UserInputText.Text.Insert(this.UserInputText.SelectionStart, value);

            this.UserInputText.SelectionStart = selectionStart + value.Length;

            this.UserInputText.SelectionLength = 0;
        }

        private void DeleteTextValue()
        {
            if (this.UserInputText.SelectionStart == 0 || (this.UserInputText.SelectionStart > this.UserInputText.Text.Length))
                return;
            
            var selectionStart = this.UserInputText.SelectionStart;

            this.UserInputText.Text = this.UserInputText.Text.Remove(this.UserInputText.SelectionStart - 1, 1);

            this.UserInputText.SelectionStart = selectionStart;

            this.UserInputText.SelectionLength = 0;
        }
        
        #endregion
    }
}