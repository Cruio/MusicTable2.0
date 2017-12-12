using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using MusicTable2._0;
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
            Sound intro = new Sound();
            intro.assignment(0, 1, 9);
            intro.assignment(1, 3, 5);
            intro.assignment(2, 4, 3);
            intro.assignment(3, 3, 7);
            intro.startRecord();

            Thread t1;
            Detector detector = new Detector();
            t1 = new Thread(() => detector.Looper()); ;
            t1.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartScreen());

            
        }
    }
}
