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
        
        #region Operator methods

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            CalculateEquation();
        }

        private void ChangeSignButton_Click(object sender, EventArgs e)
        {
            ChangeSign();
        }
        
        #endregion
        
        private void CalculateEquation()
        {
            //TODO Finish method

            FocusInputText();
        }

        private void ChangeSign()
        {
            //TODO Finish method

            FocusInputText();
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

        private void CButton_Click(object sender, EventArgs e)
        {
            //TODO This is not correct behaviour, edit this
            CEButton_Click(sender, e);
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
            if (this.UserInputText.Text.Length < this.UserInputText.SelectionStart + 1)
                return;

            var selectionStart = this.UserInputText.SelectionStart;
                        
            this.UserInputText.Text = this.UserInputText.Text.Remove(this.UserInputText.SelectionStart, 1);

            this.UserInputText.SelectionStart = selectionStart;

            this.UserInputText.SelectionLength = 0;
        }
        
        #endregion
    }
}