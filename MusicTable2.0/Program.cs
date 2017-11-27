using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;


namespace MusicTable2._0
{


    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {   

            Sound fuck = new Sound();
            fuck.assignment(0, 1, 2);
            fuck.assignment(1, 2, 4);
            fuck.assignment(2, 3, 6);
            fuck.assignment(3, 4, 0);
            fuck.startRecord();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartScreen());
            

        }
    }
}
