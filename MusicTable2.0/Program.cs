﻿using System;
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
            Sound fuck = new Sound();
            fuck.checkSound(3, 2);
            fuck.checkSound(3, 3);
            fuck.checkSound(1, 4);
            fuck.checkSound(4, 4);
            fuck.checkSound(4, 5);
            fuck.checkSound(4, 6);
            fuck.checkSound(4, 7);
            fuck.checkSound(3, 8);
            fuck.checkSound(1, 4);

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
