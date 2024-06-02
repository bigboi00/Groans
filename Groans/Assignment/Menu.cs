using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Assignment
{
    public partial class Menu : Form
    {
        Main obj1 = new Main(); //game form
        Guide guide = new Guide(); //guide form
        public static Menu instance;  // set the instance constructor to this form
        public Menu()
        {
            InitializeComponent();
            instance = this;
        }

        private void Form3_Load(object sender, EventArgs e) //when this form load
        {
            InitializeComponent();
            label3.Text = "Player 1";
            label2.Text = "Player 2";
            label6.Text = "50";
            label7.Text = "120";
        }

        private void button1_Click(object sender, EventArgs e) //set players' name button
        {
            label3.Text = "";
            label2.Text = "";

            Name_Form CollectData = new Name_Form("First Player's Name?", "Change Name"); //call name form (input form)
            label3.Text = CollectData.sInputValue; //name = input

            if (CollectData.ShowDialog() == DialogResult.Cancel) // If user pressed Esc or the Cancel button
            {
                label3.Text = "Player One";
            }
            else if (CollectData.sInputValue == "") //if input is empty, set to default value
            {
                label3.Text = "Player One";
            }
            else
            {
                label3.Text = CollectData.sInputValue;
            }
    

            Main.instance.player1 = label3.Text;

            CollectData = new Name_Form("Second Player's Name?", "Change Name");
            label2.Text = CollectData.sInputValue;

            if (CollectData.ShowDialog() == DialogResult.Cancel) // If user pressed Esc or the Cancel button
            {
                label2.Text = "Player Two";
            }
            else if (CollectData.sInputValue == "")
            {
                label2.Text = "Player Two";
            }

            else
            {
                label2.Text = CollectData.sInputValue;
            }

            Main.instance.player2 = label2.Text;

        }

        private void button4_Click(object sender, EventArgs e) //set goal button
        {
            label6.Text = "";
            Goal_Form CollectData = new Goal_Form("Goal? ( type any number)", "Change Goal");


            if (CollectData.ShowDialog() == DialogResult.Cancel) // If user pressed Esc or the Cancel button
            {
                label6.Text = "50"; //goal = default value: 50
            }
            else if (CollectData.InputValue == "") //if input is empty, set to default value
            {
                label6.Text = "50";
            }
            else // try convert input to integers
            {
                try
                {
                    var checkInt = Convert.ToInt32(CollectData.InputValue);
                    label6.Text = checkInt.ToString();
                    Main.instance.goal = checkInt;
                }
                catch
                {
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //set timer button
        {
            label7.Text = "";
            Goal_Form CollectData = new Goal_Form("Time Limit?  ( type any number)", "Change Time Limit");


            if (CollectData.ShowDialog() == DialogResult.Cancel) // If user pressed Esc or the Cancel button
            {
                label7.Text = "60";
            }
            else if (CollectData.InputValue == "") //if input is empty, set to default value
            {
                label7.Text = "60";
            }
            else // try convert input to integers
            {
                try
                {
                    var checkInt = Convert.ToInt32(CollectData.InputValue);
                    label7.Text = checkInt.ToString();
                    Main.instance.setTime = Convert.ToInt32(label7.Text);
                }
                catch
                {
                }

            }

        }
        private void button2_Click(object sender, EventArgs e) //player vs player button
        {
            Main.instance.bots = false;
            obj1.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e) //guide button
        {
            guide.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e) //quit button
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e) //player vs bots button 
        {
            Main.instance.bots = true;
            obj1.ShowDialog();
        }
    }
}
