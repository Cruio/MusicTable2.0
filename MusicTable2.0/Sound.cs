using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace MusicTable2._0
{

    class Sound
    {
        SoundPlayer pianist = new SoundPlayer();
        
        public int duration;

        public void checkSound(int shape, int pitch)
        {
            if (shape == 1)
            {
                duration = 1;
            }
            if (shape == 2)
            {
                duration = 2;
            }
            if (shape == 3)
            {
                duration = 4;
            }
            if (shape == 4)
            {
                duration = 8;
            }


            switch (pitch)
            {
                case 1:
                    pianist.Stream = Properties.Resources.W1;
                    break;
                        
                case 2:
                    pianist.Stream = Properties.Resources.W2;
                    break;

                case 3:
                    pianist.Stream = Properties.Resources.W3;
                    break;

                case 4:
                    pianist.Stream = Properties.Resources.W4;
                    break;

                case 5:
                    pianist.Stream = Properties.Resources.W5;
                    break;

                case 6:
                    pianist.Stream = Properties.Resources.W6;
                    break;

                case 7:
                    pianist.Stream = Properties.Resources.W7;
                    break;

                case 8:
                    pianist.Stream = Properties.Resources.W8;
                    break;

                case 9:
                    pianist.Stream = Properties.Resources.W9;
                    break;
            }

            pianist.Play();
            System.Threading.Thread.Sleep(2000 / duration);
            pianist.Stop();


        }


    }
}
