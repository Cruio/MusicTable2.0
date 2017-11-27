using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;




namespace MusicTable2._0
{
    class Sound
    {
        //https://stackoverflow.com/questions/8109218/playing-piano-tones-using-c-sharp
        //https://www.codeproject.com/Articles/6228/C-MIDI-Toolkit


        SoundPlayer pianist = new SoundPlayer();
        int endThread = 1;
        int loopchecker = 0;
        public int duration;
        public int[,] playOrder = new int[4, 2];

        public void startRecord()
        {
            if (loopchecker == 0)
            {
                Thread wavPlayer = new Thread(grammophone);
                wavPlayer.Start();
            }
        }

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
                default:
                    break;
            }
            
        }

        public void grammophone()
        {
            loopchecker = 1;
            for (int i = 0; i < 4; i++) {
                checkSound(playOrder[i, 0], playOrder[i, 1]);
                if (playOrder[i, 0] > 0 && playOrder[i, 1] > 0)
                {
                    pianist.Play();
                    System.Threading.Thread.Sleep(2000 / duration);
                    pianist.Stop();
                }
            }
            reset();
            loopchecker = 0;


        }

        public void assignment(int order, int shape, int pitch)
        {
                playOrder[order, 0] = shape;
                playOrder[order, 1] = pitch;
        }

        //Resets the values in playOrder array.
        public void reset()
        {
            for (int i = 0; i < 4; i++)
            {
                playOrder[i, 0] = 0;
                playOrder[i, 1] = 0;
            }
        }
    }
}
