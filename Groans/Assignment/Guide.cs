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
    public partial class Guide : Form
    {
        public Guide()
        {
            InitializeComponent();
        }

        private void Guide_Load(object sender, EventArgs e) //form load
        {
            richTextBox1.AppendText("Hi welcome to Groan!"); //display text and append to the text box
            richTextBox1.AppendText(Environment.NewLine + "\rGroan! is a dice game consist of two player only. It’s a good combination of luck and judgement- A strategy game. ");
            richTextBox1.AppendText(Environment.NewLine + "\rGoal: The objective is to reach the goal of the score which can be set on the left side of main menu before starting the game. (Default value: 50)");
            richTextBox1.AppendText(Environment.NewLine + "\rTimer: Pay attention! Once the timer ends whoever score is more than other side, he/she will win! (Reminder: there is only one winner!)");
            richTextBox1.AppendText(Environment.NewLine + "\rTurns: The random system will decide who get the first turn once the game start. After the winner is decided, the first turn will apply to the loser for fair game.");
            richTextBox1.AppendText(Environment.NewLine + "\rGameplay: Why would you ever bother to pass the dice? Because there is a risk in rolling them. If either of the dice rolls a one (Groan!), the running score is lost, and the dice automatically pass to the opponent.");
            richTextBox1.AppendText(Environment.NewLine + "\rOh, yes, one small point: if you’re silly enough to throw a double one (also known as ‘snake’s eyes’), you lose not only the running score but also your cumulative score. In other words, you start gain from nothing.");
            richTextBox1.AppendText(Environment.NewLine + "\rSuggestion: A goal between 50 and 100 seems to make for a reasonable game.  Goals of less than 50 tend to let the first player win too often, while goals of more than 100 tend to have lots of snake’s eyes, and so go for a long time.");
            richTextBox1.AppendText(Environment.NewLine + "And timer between 60 and 120 is more suitable for 50 and 100 goal respectively. However any adjustment can be set manually to suit to whatever the player desired.");
            richTextBox1.AppendText(Environment.NewLine + "\rGamemode: There is only two gamemode available: PvP (Player vs Player) and Player vs Bots.");
            richTextBox1.AppendText(Environment.NewLine + "\rPvP: Player against player, both player can change their preference name on the left side of main menu. (This gamemode is made for 2 real player)");
            richTextBox1.AppendText(Environment.NewLine + "\rBots: Player against bot, for this gamemode player will play against an AI program.");
            richTextBox1.AppendText(Environment.NewLine + "\rWhat are you waiting for? START PLAYING NOW!");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true; //no input/typing allow
            richTextBox1.SelectionStart = 0; //text start from beginning 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close(); //close form
        }
    }
}
