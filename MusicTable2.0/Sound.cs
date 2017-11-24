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
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W1.wav";
                    break;

                case 2:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W2.wav";
                    break;

                case 3:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W3.wav";
                    break;

                case 4:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W4.wav";
                    break;

                case 5:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W5.wav";
                    break;

                case 6:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W6.wav";
                    break;

                case 7:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W7.wav";
                    break;

                case 8:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W8.wav";
                    break;

                case 9:
                    pianist.SoundLocation = @"C:\Users\Anders\Documents\GitHub\MusicTable2.0\MusicTable2.0\Resources\wav\W9.wav";
                    break;
            }

            pianist.Play();
            System.Threading.Thread.Sleep(2000 / duration);
            pianist.Stop();


        }


    }
}
