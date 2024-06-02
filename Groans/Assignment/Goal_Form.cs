using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Goal_Form : Form
    {
        public static Goal_Form instance; // set the instance constructor to this form
        public Goal_Form()
        {
            InitializeComponent();
            instance = this;
        }

        public Goal_Form(string Pre, string Title) //public method for goal score input 
        {
            InitializeComponent();
            Text = Title; //title of input form
            label1.Text = Pre; //the objective of input form
        }


        public string InputValue //property method
        {
            get { return textBox1.Text; } //return value of input
            set { textBox1.Text = value; } //assigns value to input

        }

        private void button1_Click(object sender, EventArgs e) //Ok button
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //Cancel button
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //if user type/input
        {
            char input = e.KeyChar; //verify what user inputting

            if (!Char.IsDigit(input) && input != 8) //input only accept digit and backspace
            {
                e.Handled = true; //allow user to input
            }
            else if (InputValue.Length > 2) //input not more than 2 length
            {
                e.Handled = true;
                if (char.IsControl(e.KeyChar)) //control key
                {
                    e.Handled = false;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
