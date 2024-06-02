using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;



namespace Assignment
{

    public partial class Main : Form
    {
        Random random = new Random(); //random variable 
        SoundPlayer winSound = new SoundPlayer("win.wav"); //winning sound variable
        SoundPlayer loseSound = new SoundPlayer("lose.wav"); //losing sound variable
        int p1_Point1 = 0; //player 1 first dice 
        int p1_Point2 = 0; //player 1 second dice 
        int p2_Point1 = 0; //player 2 first dice 
        int p2_Point2 = 0; //player 2 second dice 
        int player1win = 0; //player 1 win count
        int player2win = 0; //player 2 win count
        int botRoll = 0; //bot roll count
        int round = 0; // round turn
        public static Main instance; // set the instance constructor to this form
        public bool bots; // verify bot or player game
        public int runningScore = 0; 
        public int player1Score = 0; //cumulative score
        public int player2Score = 0; 
        public int setTime; //time from menu
        public int seconds = 120; 
        public int coin; // like tossing coin, decide which player turn first when game start
        public int coin1;  //this one for bots
        public int playerTurn = 1; //player's turn , player1 = 1, player = 2
        public string player1 = "Player 1";  
        public string player2 = "Player 2";
        public int goal = 50;
        public Main()
        {
            InitializeComponent();
            instance = this; 


        }

        private void RandomTurn()
        {
            coin = random.Next(1, 3); // random num between 1 and 2
            coin1 = random.Next(1, 3); 
            {

                
                if (coin == 1) //if random num is 1, player 1's turn 
                {
                    playerTurn = 1;
                    label2.Text = player1 + "'s Turn ";
                }
                else if (coin == 2)
                {
                    playerTurn = 2;
                    label2.Text = player2 + "'s Turn ";
                }
                
            }

        }



        private void button1_Click(object sender, EventArgs e) //roll button
        {
            SoundPlayer diceSound = new SoundPlayer("dice.wav"); 
            diceSound.Play();  //play dice rolling sound
            _ = game(); //discard function
        }

        private void button2_Click(object sender, EventArgs e) //pass button
        {
            SoundPlayer passSound = new SoundPlayer("pass.wav"); 
            passSound.Play(); //play passing sound
            round++; 
            pass(); 
            turn();

        }

        private void button3_Click(object sender, EventArgs e) //back to menu button
        {
            if (MessageBox.Show("Are you sure you want to leave? The current record will not be saved.", "Return to Menu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                restart(); 
                RandomTurn();
                timer1.Stop();
                this.Hide();
            }
            else
            {
            }
        }

        private void button4_Click(object sender, EventArgs e) //restart button
        {
            if (bots)
            {
                aiTimer.Start();
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button4.Hide();
            restart();
            turn();
        }

        private void button5_Click(object sender, EventArgs e) //close event log
        {
            richTextBox1.Hide();
            button5.Hide();
        }

        private void button6_Click(object sender, EventArgs e) //show event log
        {
            richTextBox1.Show();
            button5.Show();
        }

        private void rand() //players' random dice point
        {
            int player1pointOne = random.Next(1, 7);
            int player1pointTwo = random.Next(1, 7);
            int player2pointOne = random.Next(1, 7);
            int player2pointTwo = random.Next(1, 7);

            p1_Point1 = player1pointOne;
            p1_Point2 = player1pointTwo;
            p2_Point1 = player2pointOne;
            p2_Point2 = player2pointTwo;
        }

        private void checkTimer() // verify timer's time and execute event
        {
            if (seconds == 0) //if timer ends, then verify who score is higher and he/she win
            {
                if (player1Score > player2Score)
                {
                    player1win++;
                    playerTurn = 2; //if player 1 win then next game player 2's turn
                    label6.Text = "Win: " + player1win;
                    MessageBox.Show(player1 + " win!");
                    richTextBox1.AppendText(Environment.NewLine + player1 + " win!");
                    winning();
                }
                else if (player2Score > player1Score)
                {
                    player2win++;
                    playerTurn = 1; 
                    label7.Text = "Win: " + player2win;
                    if (bots)
                    {
                        MessageBox.Show("Bot win!");
                        richTextBox1.AppendText(Environment.NewLine + "Bot win!");
                    }
                    else
                    {
                        MessageBox.Show(player2 + " win!");
                        richTextBox1.AppendText(Environment.NewLine + player2 + " win!");
                    }
                    winning();
                }
                else //if both has the same score when timer ends, then add 20 more seconds, only 1 player can be the winner
                {
                    seconds += 20;
                    timer1.Start(); 
                }


            }
        }

        private void timer1_Tick(object sender, EventArgs e) //timer stop if time reach 0 
        {
            label5.Text = "Time: " + seconds--.ToString();
            if (seconds == 0)
            {
                timer1.Stop(); 
            }
            checkTimer();

        }

        private void aiTimer_Tick(object sender, EventArgs e) //Every tick ai will act if it's turn
        {
            if (bots) 
            {
                if (playerTurn == 2)
                {

                    if (botRoll > 1)
                    {
                        button2.PerformClick(); //pass dice if bot roll twice
                        botRoll = 0;
                    }
                    else
                        button1.PerformClick(); //roll dice

                }
                else
                {
                    botRoll = 0;  //reset bot's roll time
                }

            }
        }
        private void animation() //animation for all roll
        {
            pictureBox1.Show(); 
            pictureBox2.Show();
            if (playerTurn == 1)
            {
                Task.Run(async () => //attribution: we search youtube tutorial on this, reference in journal
                {
                    pictureBox1.Image = Properties.Resources.dice_animation; //dice animation
                    pictureBox2.Image = Properties.Resources.dice_animation; 
                    await Task.Delay(2000); // delay 2 seconds and execute tasks
                    switch (p1_Point1) //roll's 6 number possibility and output the right dice number picture
                    {
                        case 1:
                            pictureBox1.Image = Properties.Resources.dice_1;
                            break;
                        case 2:
                            pictureBox1.Image = Properties.Resources.dice_2;
                            break;
                        case 3:
                            pictureBox1.Image = Properties.Resources.dice_3;
                            break;
                        case 4:
                            pictureBox1.Image = Properties.Resources.dice_4;
                            break;
                        case 5:
                            pictureBox1.Image = Properties.Resources.dice_5;
                            break;
                        case 6:
                            pictureBox1.Image = Properties.Resources.dice_6;
                            break;
                    }
                    switch (p1_Point2)
                    {
                        case 1:
                            pictureBox2.Image = Properties.Resources.dice_1;
                            break;
                        case 2:
                            pictureBox2.Image = Properties.Resources.dice_2;
                            break;
                        case 3:
                            pictureBox2.Image = Properties.Resources.dice_3;
                            break;
                        case 4:
                            pictureBox2.Image = Properties.Resources.dice_4;
                            break;
                        case 5:
                            pictureBox2.Image = Properties.Resources.dice_5;
                            break;
                        case 6:
                            pictureBox2.Image = Properties.Resources.dice_6;
                            break;
                    }
                });
            }
            else if (playerTurn == 2)
            {
                Task.Run(async () =>
                {
                    pictureBox1.Image = Properties.Resources.dice_animation;
                    pictureBox2.Image = Properties.Resources.dice_animation;
                    await Task.Delay(2000);
                    switch (p2_Point1)
                    {
                        case 1:
                            pictureBox1.Image = Properties.Resources.dice_1;
                            break;
                        case 2:
                            pictureBox1.Image = Properties.Resources.dice_2;
                            break;
                        case 3:
                            pictureBox1.Image = Properties.Resources.dice_3;
                            break;
                        case 4:
                            pictureBox1.Image = Properties.Resources.dice_4;
                            break;
                        case 5:
                            pictureBox1.Image = Properties.Resources.dice_5;
                            break;
                        case 6:
                            pictureBox1.Image = Properties.Resources.dice_6;
                            break;
                    }
                    switch (p2_Point2)
                    {
                        case 1:
                            pictureBox2.Image = Properties.Resources.dice_1;
                            break;
                        case 2:
                            pictureBox2.Image = Properties.Resources.dice_2;
                            break;
                        case 3:
                            pictureBox2.Image = Properties.Resources.dice_3;
                            break;
                        case 4:
                            pictureBox2.Image = Properties.Resources.dice_4;
                            break;
                        case 5:
                            pictureBox2.Image = Properties.Resources.dice_5;
                            break;
                        case 6:
                            pictureBox2.Image = Properties.Resources.dice_6;
                            break;
                    }
                });
            }
        }


        private void pass() //pass dice
        {
            if (playerTurn == 1)  
            {
                playerTurn = 2;
                roundTurn(); 
                richTextBox1.AppendText(Environment.NewLine + player1 + " adds " + runningScore + " to the score.");
                richTextBox1.AppendText(Environment.NewLine + player1 + " decide to pass dice.");
                player1Score += runningScore; // cumulative score = cumulative score + running score
                label3.Text = "Score = " + player1Score.ToString();
                if (player1Score >= goal) // winning case
                {
                    winSound.Play(); 
                    runningScore = 0; //running score reset to zero
                    aiTimer.Stop(); //stop ai timer
                    player1win++; //player win + 1 
                    playerTurn = 2;
                    label6.Text = "Win: " + player1win;
                    richTextBox1.AppendText(Environment.NewLine + player1 + " won!");
                    MessageBox.Show(player1 + " win!");
                    winning();

                }


            }

            else if (bots) //same function for bots (display bots)
            {
                if (playerTurn == 2)
                {
                    playerTurn = 1;
                    roundTurn();
                    richTextBox1.AppendText(Environment.NewLine + "Bot adds " + runningScore + " to the score.");
                    richTextBox1.AppendText(Environment.NewLine + "Bot decide to pass dice.");
                    player2Score += runningScore;
                    label4.Text = "Score = " + player2Score.ToString();
                    if (player2Score >= goal)
                    {
                        loseSound.Play();
                        runningScore = 0;
                        player2win++;
                        playerTurn = 1;
                        label7.Text = "Win: " + player2win;
                        richTextBox1.AppendText(Environment.NewLine + "Bot won!");
                        MessageBox.Show("Bot win!");
                        winning();
                    }

                }
            }

            else
            {
                if (playerTurn == 2)
                {
                    playerTurn = 1;
                    roundTurn();
                    richTextBox1.AppendText(Environment.NewLine + player2 + " adds " + runningScore + " to the score.");
                    richTextBox1.AppendText(Environment.NewLine + player2 + " decide to pass dice.");
                    player2Score += runningScore;
                    label4.Text = "Score = " + player2Score.ToString();
                    if (player2Score >= goal)
                    {
                        winSound.Play();
                        runningScore = 0;
                        player2win++;
                        playerTurn = 1;
                        label7.Text = "Win: " + player2win;
                        richTextBox1.AppendText(Environment.NewLine + player2 + " won!");
                        MessageBox.Show(player2 + " win!");
                        winning();
                    }
                }
            }
            runningScore = 0;
            label12.Text = "Running score: " + runningScore;

        }

        private void turn() //display player's turn on middle
        {
            if (playerTurn == 1)
            {
                label2.ForeColor = Color.Orange; // orange color for player 1
                label2.Text = player1 + "'s Turn ";
            }

            else if (bots)
            {
                if (playerTurn == 2)
                {
                    label2.ForeColor = Color.DeepSkyBlue; // blue color for player 2 and bots
                    label2.Text = "Bot's Turn ";
                }
            }

            else
            {
                label2.ForeColor = Color.DeepSkyBlue;
                label2.Text = player2 + "'s Turn ";
            }
            
        }   

        private void roundTurn() //display for round count
        {
            label8.Text = "Round: " + round;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText(Environment.NewLine + "--------------------------------------------");
            richTextBox1.AppendText(Environment.NewLine + "Round " + round + ":");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
        }
        private void restart() //reset everything to default
        {

            botRoll = 0; 
            runningScore = 0;
            player1Score = 0;
            player2Score = 0;
            round = 0;
            label1.Text = "Goal to win: " + goal;
            label2.Text = "Running score: ";
            label3.Text = "Score = " + player1Score;
            label4.Text = "Score = " + player2Score;
            label8.Text = "Round: ";
            label9.Text = player1 + "'s \nScoreboard";
            if (bots)
            {
                label10.Text = "Bot's \nScoreboard";
            }
            else
            {
                label10.Text = player2 + "'s \nScoreboard";
            }
            richTextBox1.Text = "Event Log:- ";
            pictureBox1.Hide(); 
            pictureBox2.Hide();
            timer1.Start();
            if (setTime <= 0)
            {
                seconds = 120;
            }
            else
            {
                seconds = setTime;
            }
        }

        private void winning()
        {
            button1.Enabled = false; //disable roll button
            button2.Enabled = false; //disable pass button
            timer1.Stop(); //stop game timer
            aiTimer.Stop();
            if (MessageBox.Show("Play again?", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) //display message box 
            {
                if (bots) //if player choose yes, bot's time tick start
                {
                    aiTimer.Start(); 
                }
                button1.Enabled = true; //enable roll button
                button2.Enabled = true; //enable pass button
                button4.Hide(); //hide restart button
                restart();
                turn();
                loserTurn();

            }
            else
            {
                button4.Show(); //restart button show if player choose No
            }
        }

        private void loserTurn() //display loser turn on event log
        {
            if (playerTurn == 1) 
            {
                richTextBox1.AppendText(Environment.NewLine + "--------------------------------------------");
                richTextBox1.AppendText(Environment.NewLine + player1 + " rolls first. ");
            }
            else
            {
                if (bots)
                {
                    richTextBox1.AppendText(Environment.NewLine + "--------------------------------------------");
                    richTextBox1.AppendText(Environment.NewLine + "Bot rolls first. ");
                }
                else
                {
                    richTextBox1.AppendText(Environment.NewLine + "--------------------------------------------");
                    richTextBox1.AppendText(Environment.NewLine + player2 + " rolls first. ");
                }
            }
        }   
        async Task game() //main game function
        {
            rand(); //players' random dice point
            animation(); //dice animation and picture change
            round++; 

            if (playerTurn == 1)
            {
                runningScore += p1_Point1 + p1_Point2; //running score += 2 random number 
                button1.Enabled = false; 
                button2.Enabled = false;
                await Task.Delay(2000); // delay 2 seconds
                button1.Enabled = true; 
                button2.Enabled = true;
                roundTurn();
                richTextBox1.AppendText(Environment.NewLine + player1 + " rolls " + p1_Point1 + " and " + p1_Point2);
                if (p1_Point1 == 1 || p1_Point2 == 1) //if either roll 1, pass turn
                {
                    playerTurn = 2;
                    runningScore = 0;
                    richTextBox1.AppendText(Environment.NewLine + "Groan! " + player1 + " loses dice ");
                }

                if (p1_Point1 == 1 & p1_Point2 == 1) //if both roll 1, pass turn and reset to zero
                {
                    player1Score = 0;
                    runningScore = 0;
                    richTextBox1.AppendText(Environment.NewLine + "Snake's eyes! " + player1 + " loses everything.");
                    label3.Text = "Score = " + player1Score;

                }
                turn(); 
                label12.Text = "Running score: " + runningScore.ToString(); 


            }

            else if (playerTurn == 2)
            {
                runningScore += p2_Point1 + p2_Point2;
                button1.Enabled = false;
                button2.Enabled = false;
                await Task.Delay(2000);
                button1.Enabled = true;
                button2.Enabled = true;
                label12.Text = "Running score: " + runningScore.ToString();
                roundTurn();
                if (bots)
                {
                    botRoll++;
                    richTextBox1.AppendText(Environment.NewLine + "Bot rolls " + p2_Point1 + " and " + p2_Point2);
                    if (p2_Point1 == 1 || p2_Point2 == 1)
                    {
                        playerTurn = 1;
                        runningScore = 0;
                        richTextBox1.AppendText(Environment.NewLine + "Groan! Bot loses dice");

                    }

                    if (p2_Point1 == 1 & p2_Point2 == 1)
                    {
                        player2Score = 0;
                        runningScore = 0;
                        richTextBox1.AppendText(Environment.NewLine + "Snake's eyes! Bot loses everything.");
                        label4.Text = "Score = " + player2Score;
                    }
                    turn();
                    label12.Text = "Running score: " + runningScore.ToString();

                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    richTextBox1.AppendText(Environment.NewLine + player2 + " rolls " + p2_Point1 + " and " + p2_Point2);
                    if (p2_Point1 == 1 || p2_Point2 == 1)
                    {
                        runningScore = 0;
                        playerTurn = 1;
                        richTextBox1.AppendText(Environment.NewLine + player2 + " loses dice ");

                    }

                    if (p2_Point1 == 1 & p2_Point2 == 1)
                    {
                        player2Score = 0;
                        runningScore = 0;
                        richTextBox1.AppendText(Environment.NewLine + player2 + " loses everything.");
                        label4.Text = "Score = " + player2Score;
                    }
                    turn(); //display player's turn on middle
                    label12.Text = "Running score: " + runningScore.ToString();

                }

            }
                   
        }


        private void Form1_paint(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            //dice
            graph.FillRectangle(Brushes.MediumSeaGreen, 440, 360, 420, 220);
            graph.DrawRectangle(Pens.Black, 439, 359, 421, 221);
            //player's Turn
            graph.FillRectangle(Brushes.Red, 500, 289, 300, 70);
            //player 1
            graph.FillRectangle(Brushes.Orange, new Rectangle(60, 160, 300, 200));
            //player 2
            graph.FillRectangle(Brushes.DeepSkyBlue, new Rectangle(940, 160, 300, 200));
            //Groan Logo
            graph.FillEllipse(Brushes.White, new Rectangle(20, 10, 180, 60));
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
            richTextBox1.ReadOnly = true;
        }

        private void Form1_Shown(object sender, EventArgs e) //when form show execute command
        {
            button1.Enabled = true;
            button2.Enabled = true;
            label6.Text = "Win: ";
            label7.Text = "Win: ";
            button4.Hide();
            RandomTurn();
            restart();
            turn();
            if (bots)
            {
                aiTimer.Start();
                if (playerTurn == 2)
                {
                    MessageBox.Show("Bot roll first.");
                }
            }

        }
    }
}

//============================================= 
// Reference A3: externally sourced algorithm 
// Purpose: Await task execute for 2 seconds to another tasks
// Date: 16 November 2022 
// Source: Youtube Video
// Author: Frank Liu
// url: https://www.youtube.com/watch?v=CrUrvVatAxo&t=591s&ab_channel=FrankLiu 
// Adaptation required: Await task execute for 2 seconds to another tasks in the example 
//============================================= 

/*
Task.Run(async () => //attribution: we search youtube tutorial on this, reference in journal
{
pictureBox1.Image = Properties.Resources.dice_animation; //dice animation
pictureBox2.Image = Properties.Resources.dice_animation;
await Task.Delay(2000); // delay 2 seconds and execute tasks

async Task game()

_ = game();
*/

//============================================= 
// Reference A3: externally sourced algorithm 
// Purpose: change correct picture for right number
// Date: 6 November 2022 
// Source: Youtube Video
// Author: LevelUp
// url: https://www.youtube.com/watch?v=soDX_IeZsqM&ab_channel=LevelUp
// Adaptation required: change correct picture for right number
//============================================= 

/*
switch (p1_Point1) //roll's 6 number possibility and output the right dice number picture
{
case 1:
    pictureBox1.Image = Properties.Resources.dice_1;
    break;
case 2:
    pictureBox1.Image = Properties.Resources.dice_2;
    break;
case 3:
    pictureBox1.Image = Properties.Resources.dice_3;
    break;
case 4:
    pictureBox1.Image = Properties.Resources.dice_4;
    break;
case 5:
    pictureBox1.Image = Properties.Resources.dice_5;
    break;
case 6:
    pictureBox1.Image = Properties.Resources.dice_6;
    break;
}
*/
//============================================= 
// End reference A3  
//=============================================

//============================================= 
// Reference C5: externally sourced code 
// Purpose: Ai TImer Ticks
// Date: 14 November 2022 
// Source: Youtube Video
// Author: CodingWithG
// url: https://www.youtube.com/watch?v=soDX_IeZsqM&ab_channel=LevelUp
// Adaptation required: change correct picture for right number
//============================================= 

/*

aiTImer.Start();
aiTImer.Stop();
*/
//============================================= 
// End reference C5
//=============================================




