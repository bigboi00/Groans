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
    public partial class Name_Form : Form
    {
        public static Name_Form instance; // set the instance constructor to this form
        public Name_Form()
        {
            InitializeComponent();
            instance = this;
        }

        public Name_Form(string sPre, string sTitle) //public method for name input 
        {
            
            InitializeComponent();
            Text = sTitle; //title of input form
            label1.Text = sPre; //the objective of input form
        }

        public string sInputValue //property method
        {
            get { return textBox1.Text; }  //return value of input
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
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)) //input only letter/string and backspace only
                e.Handled = true; // allow user to input
            else if (char.IsWhiteSpace(e.KeyChar))   //no space input allow
            {
                e.Handled = true;
            }
            else if (sInputValue.Length > 10) //input not more than 10 length
            {
                e.Handled = true;
                if (char.IsControl(e.KeyChar)) //control key
                {
                    e.Handled = false;
                }
            }
        }

        private void Name_Form_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
