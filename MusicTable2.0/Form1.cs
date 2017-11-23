using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

 
        //UI contorl values
        int lvlUnlock = 1;
        int controlValue = 1;
     

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

            lvlUnlock = 1;

            showCurrentGameState.Image = Properties.Resources.Lvl1Star1;

            if (lvlUnlock == 1)
            {
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
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            showCurrentLevelLabel.Text = "Niveau 2";

            lvlUnlock = 2;

            showCurrentGameState.Image = Properties.Resources.Lvl2Star1;

            if (lvlUnlock == 2)
            {
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
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            showCurrentLevelLabel.Text = "Niveau 3";

            lvlUnlock = 3;

            showCurrentGameState.Image = Properties.Resources.Lvl3Star1;

            if (lvlUnlock == 3)
            {
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
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            showCurrentLevelLabel.Text = "Niveau 4";

            lvlUnlock = 4;

            showCurrentGameState.Image = Properties.Resources.Lvl4Star1;

            if (lvlUnlock == 4)
            {
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
            }
        }
        // Lock click event End



        //this is the over all control function of the UI
        void StarControl()
        {
            if (controlValue == 1)
            {
                star1Level1.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl1Star2;

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

            }
            if (controlValue == 2)
            {
                star2Level1.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl1Star3;

            }
            if (controlValue == 3)
            {
                star3Level1.Image = Properties.Resources.StarYellow;
                niveau2Lock.Image = Properties.Resources.Unlock;
                showCurrentGameState.Image = Properties.Resources.Lvl2Star1;

                showCurrentLevelLabel.Text = "Niveau 2";

                niveau2Lock.Enabled = true;
                star3Level1.Enabled = true;

                System.Threading.Thread.Sleep(1500);

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

            }
            if (controlValue == 4)
            {
                star1Level2.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl2Star2;
            }
            if (controlValue == 5)
            {
                star2Level2.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl2Star3;

            }
            if (controlValue == 6)
            {
                star3Level2.Image = Properties.Resources.StarYellow;
                niveau3Lock.Image = Properties.Resources.Unlock;
                showCurrentGameState.Image = Properties.Resources.Lvl3Star1;

                showCurrentLevelLabel.Text = "Niveau 3";

                niveau3Lock.Enabled = true;
                star3Level2.Enabled = true;

                System.Threading.Thread.Sleep(1500);

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

            }
            if (controlValue == 7)
            {
                star1Level3.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl3Star2;

            }
            if (controlValue == 8)
            {
                star2Level3.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl3Star3;

            }
            if (controlValue == 9)
            {
                star3Level3.Image = Properties.Resources.StarYellow;
                niveau4Lock.Image = Properties.Resources.Unlock;
                showCurrentGameState.Image = Properties.Resources.Lvl4Star1;

                showCurrentLevelLabel.Text = "Niveau 4";

                niveau4Lock.Enabled = true;
                star3Level3.Enabled = true;

                System.Threading.Thread.Sleep(1500);

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

            }
            if (controlValue == 10)
            {
                star1Level4.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl4Star2;

            }
            if (controlValue == 11)
            {
                star2Level4.Image = Properties.Resources.StarYellow;
                showCurrentGameState.Image = Properties.Resources.Lvl4Star3;

            }
            if (controlValue == 12)
            {
                star3Level4.Image = Properties.Resources.StarYellow;
                

            }

        }

       
    }


   
}
