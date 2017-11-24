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
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1();

            // Show the settings form
            gameForm.Show();
        }

        private void AfslutButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
