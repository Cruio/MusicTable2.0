﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicTable2._0
{
    public partial class Form1 : Form
    {
        BackgroundWorker worker1;
        public Form1()
        {
           InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
 
            noteBox.Parent = showCurrentGameState;

            noteBox2.Parent = showCurrentGameState;
            noteBox22.Parent = showCurrentGameState;

            noteBox3.Parent = showCurrentGameState;
            noteBox32.Parent = showCurrentGameState;
            noteBox33.Parent = showCurrentGameState;
            noteBox34.Parent = showCurrentGameState;

            noteBox4.Parent = showCurrentGameState;
            noteBox42.Parent = showCurrentGameState;
            noteBox43.Parent = showCurrentGameState;
            noteBox44.Parent = showCurrentGameState;

            Random rnd = new Random();
            int randomRow = rnd.Next(0, 8);
            //the first note spawn
            noteBox.Location = new Point(collum[3], row[randomRow]);



        }


        //UI contorl values

        
        int controlValue = 1;
        
        
        int[] collum = new int[4]  { 824, 630,  427, 230  };
        int[] row = new int[9] { 565, 520, 470, 425, 380, 335, 290, 245, 200 };


        //play Button Click event
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            StarControl();
            controlValue++;
            
        }
        // current Niveau label
        private void label6_Click(object sender, EventArgs e)
        {
            
        }
        // Lock click event Start
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            showCurrentLevelLabel.Text = "Niveau 1";

             NiveauSelect(1); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            showCurrentLevelLabel.Text = "Niveau 2";

           

             NiveauSelect(2);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            showCurrentLevelLabel.Text = "Niveau 3";

            

            NiveauSelect(3);
        }
        
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            showCurrentLevelLabel.Text = "Niveau 4";

            NiveauSelect(4);
            
        }
        // Lock click event End



        //this is the over all control function of the UI
        void StarControl()
        {

            if (controlValue == 1)
            {
                //fills in the stars
                star1Level1.Image = Properties.Resources.StarYellow;
               
                //new random note location 
               NiveauSelect(1);
            }
            if (controlValue == 2)
            {
                //fills in the stars
                star2Level1.Image = Properties.Resources.StarYellow;

                //new random note location 
                NiveauSelect(1);
            }
            if (controlValue == 3)
            {
                //fills in the stars
                star3Level1.Image = Properties.Resources.StarYellow;

                //changes the lovk picture to the unlock picture
                niveau2Lock.Image = Properties.Resources.Unlock;

                // changes the Current level label 
                showCurrentLevelLabel.Text = "Niveau 2";


                //makes the niveau lock able to be click 
                niveau2Lock.Enabled = true;
                star3Level1.Enabled = true;

                //Pause the game for 1,5 sec so you can see the star get filled in 
                System.Threading.Thread.Sleep(1500);

                //hide and show the stars that needs to be shown 
                star1Level1.Visible = false;
                star2Level1.Visible = false;
                star3Level1.Visible = false;
                star1Level2.Visible = true;
                star2Level2.Visible = true;
                star3Level2.Visible = true;
                star1Level3.Visible = false;
                star2Level3.Visible = false;
                star3Level3.Visible = false;
                star1Level4.Visible = false;
                star2Level4.Visible = false;
                star3Level4.Visible = false;        

                //new random note location  
                NiveauSelect(2);
            }
            if (controlValue == 4)
            {
                //fills in the stars
                star1Level2.Image = Properties.Resources.StarYellow;

                // new random note location 
                NiveauSelect(2);
            }
            if (controlValue == 5)
            {
                //fills in the stars
                star2Level2.Image = Properties.Resources.StarYellow;

                //new random note location 
                NiveauSelect(2);

            }
            if (controlValue == 6)
            {
                //fills in the stars
                star3Level2.Image = Properties.Resources.StarYellow;

                //changes the lovk picture to the unlock picture
                niveau3Lock.Image = Properties.Resources.Unlock;

                // changes the Current level label 
                showCurrentLevelLabel.Text = "Niveau 3";

                //makes the niveau lock able to be click 
                niveau3Lock.Enabled = true;
                star3Level2.Enabled = true;

                //Pause the game for 1,5 sec so you can see the star get filled in 
                System.Threading.Thread.Sleep(1500);

                //hide and show the stars that needs to be shown 
                star1Level1.Visible = false;
                star2Level1.Visible = false;
                star3Level1.Visible = false;
                star1Level2.Visible = false;
                star2Level2.Visible = false;
                star3Level2.Visible = false;
                star1Level3.Visible = true;
                star2Level3.Visible = true;
                star3Level3.Visible = true;
                star1Level4.Visible = false;
                star2Level4.Visible = false;
                star3Level4.Visible = false;

                //new random note location 
                NiveauSelect(3);
            }
            if (controlValue == 7)
            {
                //fills in the stars
                star1Level3.Image = Properties.Resources.StarYellow;

                //new random note location 
                NiveauSelect(3);
            }
            if (controlValue == 8)
            {
                //fills in the stars
                star2Level3.Image = Properties.Resources.StarYellow;

                //new random note location 
                NiveauSelect(3);
            }
            if (controlValue == 9)
            {
                //fills in the stars
                star3Level3.Image = Properties.Resources.StarYellow;

                //changes the lovk picture to the unlock picture
                niveau4Lock.Image = Properties.Resources.Unlock;

                // changes the Current level label 
                showCurrentLevelLabel.Text = "Niveau 4";

                //makes the niveau lock able to be click 
                niveau4Lock.Enabled = true;
                star3Level3.Enabled = true;

                //Pause the game for 1,5 sec so you can see the star get filled in 
                System.Threading.Thread.Sleep(1500);

                //hide and show the stars that needs to be shown 
                star1Level1.Visible = false;
                star2Level1.Visible = false;
                star3Level1.Visible = false;
                star1Level2.Visible = false;
                star2Level2.Visible = false;
                star3Level2.Visible = false;
                star1Level3.Visible = false;
                star2Level3.Visible = false;
                star3Level3.Visible = false;
                star1Level4.Visible = true;
                star2Level4.Visible = true;
                star3Level4.Visible = true;

                //new random note location 
                NiveauSelect(4);
            }
            if (controlValue == 10)
            {
                //fills in the stars
                star1Level4.Image = Properties.Resources.StarYellow;

                //new random note location 
                NiveauSelect(4);
            }
            if (controlValue == 11)
            {
                //fills in the stars
                star2Level4.Image = Properties.Resources.StarYellow;

                //new random note location 
                NiveauSelect(4);
            }
            if (controlValue == 12)
            {
                //fills in the stars
                star3Level4.Image = Properties.Resources.StarYellow;

                //hide the notes for the fourth lvl
                noteBox4.Visible = false;
                noteBox42.Visible = false;
                noteBox43.Visible = false;
                noteBox44.Visible = false;
            }

        }
        // the exit door button thing.
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //the lvl select function
        void NiveauSelect(int setLvl)
        {
            // makes 4 random int for controling where the note will spawn on a row
            Random rnd = new Random();
            int randomRow = rnd.Next(0, 8);
            int randomRow2 = rnd.Next(0, 8);
            int randomRow3 = rnd.Next(0, 8);
            int randomRow4 = rnd.Next(0, 8);


            if (setLvl == 1)
            {
                ////show the notes for the first lvl
                noteBox.Visible = true;

                //hides the note from second lvl
                noteBox2.Visible = false;
                noteBox22.Visible = false;

                //hides the notes for the third lvl
                noteBox3.Visible = false;
                noteBox32.Visible = false;
                noteBox33.Visible = false;
                noteBox34.Visible = false;

                //hide the notes for the fourth lvl
                noteBox4.Visible = false;
                noteBox42.Visible = false;
                noteBox43.Visible = false;
                noteBox44.Visible = false;

                //hide and show the stars that needs to be shown 
                star1Level1.Visible = true;
                star2Level1.Visible = true;
                star3Level1.Visible = true;
                star1Level2.Visible = false;
                star2Level2.Visible = false;
                star3Level2.Visible = false;
                star1Level3.Visible = false;
                star2Level3.Visible = false;
                star3Level3.Visible = false;
                star1Level4.Visible = false;
                star2Level4.Visible = false;
                star3Level4.Visible = false;

                noteBox.Location = new Point(collum[3], row[randomRow]);

            }
            if (setLvl == 2)
            {
                ////hide the notes for the first lvl
                noteBox.Visible = false;

                //hides the note from second lvl
                noteBox2.Visible = true;
                noteBox22.Visible = true;

                //hides the notes for the thrid lvl
                noteBox3.Visible = false;
                noteBox32.Visible = false;
                noteBox33.Visible = false;
                noteBox34.Visible = false;

                //hide the notes for the fourth lvl
                noteBox4.Visible = false;
                noteBox42.Visible = false;
                noteBox43.Visible = false;
                noteBox44.Visible = false;

                //hide and show the stars that needs to be shown 
                star1Level1.Visible = false;
                star2Level1.Visible = false;
                star3Level1.Visible = false;
                star1Level2.Visible = true;
                star2Level2.Visible = true;
                star3Level2.Visible = true;
                star1Level3.Visible = false;
                star2Level3.Visible = false;
                star3Level3.Visible = false;
                star1Level4.Visible = false;
                star2Level4.Visible = false;
                star3Level4.Visible = false;


                noteBox2.Location = new Point(collum[3] + 20, row[randomRow] - 182);
   
                noteBox22.Location = new Point(collum[1] + 20, row[randomRow2] - 142);
            }
            if (setLvl == 3)
            {
                ////hide the notes for the first lvl
                noteBox.Visible = false;

                //hides the note from second lvl
                noteBox2.Visible = false;
                noteBox22.Visible = false;

                //show the notes for the thrid lvl
                noteBox3.Visible = true;
                noteBox32.Visible = true;
                noteBox33.Visible = true;
                noteBox34.Visible = true;

                //hide the notes for the fourth lvl
                noteBox4.Visible = false;
                noteBox42.Visible = false;
                noteBox43.Visible = false;
                noteBox44.Visible = false;

                //hide and show the stars that needs to be shown 
                star1Level1.Visible = false;
                star2Level1.Visible = false;
                star3Level1.Visible = false;
                star1Level2.Visible = false;
                star2Level2.Visible = false;
                star3Level2.Visible = false;
                star1Level3.Visible = true;
                star2Level3.Visible = true;
                star3Level3.Visible = true;
                star1Level4.Visible = false;
                star2Level4.Visible = false;
                star3Level4.Visible = false;

                noteBox3.Location = new Point(collum[0] + 20, row[randomRow] - 185);
    
                noteBox32.Location = new Point(collum[1] + 20, row[randomRow2] - 185);
              
                noteBox33.Location = new Point(collum[2] + 20, row[randomRow3] - 185);
            
                noteBox34.Location = new Point(collum[3] + 20, row[randomRow4] - 185);
            }
            if (setLvl == 4)
            {
                ////hide the notes for the first lvl
                noteBox.Visible = false;

                //hides the note from second lvl
                noteBox2.Visible = false;
                noteBox22.Visible = false;

                //hides the notes for the thrid lvl
                noteBox3.Visible = false;
                noteBox32.Visible = false;
                noteBox33.Visible = false;
                noteBox34.Visible = false;

                //hide the notes for the fourth lvl
                noteBox4.Visible = true;
                noteBox42.Visible = true;
                noteBox43.Visible = true;
                noteBox44.Visible = true;

                //hide and show the stars that needs to be shown 
                star1Level1.Visible = false;
                star2Level1.Visible = false;
                star3Level1.Visible = false;
                star1Level2.Visible = false;
                star2Level2.Visible = false;
                star3Level2.Visible = false;
                star1Level3.Visible = false;
                star2Level3.Visible = false;
                star3Level3.Visible = false;
                star1Level4.Visible = true;
                star2Level4.Visible = true;
                star3Level4.Visible = true;

                noteBox4.Location = new Point(collum[0] + 25, row[randomRow] - 187);
             
                noteBox42.Location = new Point(collum[1] + 25, row[randomRow2] - 187);
               
                noteBox43.Location = new Point(collum[2] + 25, row[randomRow3] - 187);
              
                noteBox44.Location = new Point(collum[3] + 25, row[randomRow4] - 187);
            }


        }
    }


   
}
