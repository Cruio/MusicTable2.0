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
            label6.Text = "Niveau 1";

            lvlUnlock = 1;
            if (lvlUnlock == 1)
            {
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                pictureBox17.Visible = false;
                pictureBox18.Visible = false;
                pictureBox19.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            label6.Text = "Niveau 2";

            lvlUnlock = 2;

            if (lvlUnlock == 2)
            {
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                pictureBox13.Visible = true;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                pictureBox17.Visible = false;
                pictureBox18.Visible = false;
                pictureBox19.Visible = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            label6.Text = "Niveau 3";

            lvlUnlock = 3;

            if (lvlUnlock == 3)
            {
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = true;
                pictureBox15.Visible = true;
                pictureBox16.Visible = true;
                pictureBox17.Visible = false;
                pictureBox18.Visible = false;
                pictureBox19.Visible = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            label6.Text = "Niveau 4";

            lvlUnlock = 4;

            if (lvlUnlock == 4)
            {
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                pictureBox17.Visible = true;
                pictureBox18.Visible = true;
                pictureBox19.Visible = true;
            }
        }
        // Lock click event End



        //this is the over all control function of the UI
        void StarControl()
        {
            if (controlValue == 1)
            {
                pictureBox8.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl1Star2;

                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                pictureBox17.Visible = false;
                pictureBox18.Visible = false;
                pictureBox19.Visible = false;

            }
            if (controlValue == 2)
            {
                pictureBox9.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl1Star3;

            }
            if (controlValue == 3)
            {
                pictureBox10.Image = Properties.Resources.StarYellow;
                pictureBox2.Image = Properties.Resources.Unlock;
                pictureBox7.Image = Properties.Resources.Lvl2Star1;

                label6.Text = "Niveau 2";

                pictureBox2.Enabled = true;
                pictureBox10.Enabled = true;

                System.Threading.Thread.Sleep(1500);

                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                pictureBox13.Visible = true;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                pictureBox17.Visible = false;
                pictureBox18.Visible = false;
                pictureBox19.Visible = false;

            }
            if (controlValue == 4)
            {
                pictureBox11.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl2Star2;
            }
            if (controlValue == 5)
            {
                pictureBox12.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl2Star3;

            }
            if (controlValue == 6)
            {
                pictureBox13.Image = Properties.Resources.StarYellow;
                pictureBox3.Image = Properties.Resources.Unlock;
                pictureBox7.Image = Properties.Resources.Lvl3Star1;

                label6.Text = "Niveau 3";

                pictureBox3.Enabled = true;
                pictureBox13.Enabled = true;

                System.Threading.Thread.Sleep(1500);

                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = true;
                pictureBox15.Visible = true;
                pictureBox16.Visible = true;
                pictureBox17.Visible = false;
                pictureBox18.Visible = false;
                pictureBox19.Visible = false;

            }
            if (controlValue == 7)
            {
                pictureBox14.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl3Star2;

            }
            if (controlValue == 8)
            {
                pictureBox15.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl3Star3;

            }
            if (controlValue == 9)
            {
                pictureBox16.Image = Properties.Resources.StarYellow;
                pictureBox4.Image = Properties.Resources.Unlock;
                pictureBox7.Image = Properties.Resources.Lvl4Star1;

                label6.Text = "Niveau 4";

                pictureBox4.Enabled = true;
                pictureBox16.Enabled = true;

                System.Threading.Thread.Sleep(1500);

                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                pictureBox17.Visible = true;
                pictureBox18.Visible = true;
                pictureBox19.Visible = true;

            }
            if (controlValue == 10)
            {
                pictureBox17.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl4Star2;

            }
            if (controlValue == 11)
            {
                pictureBox18.Image = Properties.Resources.StarYellow;
                pictureBox7.Image = Properties.Resources.Lvl4Star3;

            }
            if (controlValue == 12)
            {
                pictureBox19.Image = Properties.Resources.StarYellow;
                

            }

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }
    }


   
}
