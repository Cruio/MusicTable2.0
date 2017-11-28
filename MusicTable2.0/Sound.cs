using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using Sanford.Multimedia.Midi;






namespace MusicTable2._0
{
    class Sound
    {
        //https://stackoverflow.com/questions/8109218/playing-piano-tones-using-c-sharp
        //https://www.codeproject.com/Articles/6228/C-MIDI-Toolkit
        ChannelMessageBuilder builder = new ChannelMessageBuilder();


        SoundPlayer pianist = new SoundPlayer();
        int endThread = 1;
        int loopchecker = 0;
        public int duration;
        public int[,] playOrder = new int[4, 2];

        /*public void testingstuff(int pitch)
        {
            using (OutputDevice outDevice = new OutputDevice(0))
            {
                builder.Command = ChannelCommand.NoteOn;
                builder.MidiChannel = 0;
                builder.Data1 = pitch;
                builder.Data2 = 127;
                builder.Build();

                outDevice.Send(builder.Result);

                Thread.Sleep(1000);

                builder.Command = ChannelCommand.NoteOff;
                builder.Data2 = 0;
                builder.Build();

                outDevice.Send(builder.Result);
            }
        }*/

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
                builder.Data2 = 127;

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
                        builder.Data1 = 60;
                        break;
                    case 2:
                        builder.Data1 = 62;
                        break;
                    case 3:
                        builder.Data1 = 64;
                        break;
                    case 4:
                        builder.Data1 = 65;
                        break;
                    case 5:
                        builder.Data1 = 67;
                        break;
                    case 6:
                        builder.Data1 = 69;
                        break;
                    case 7:
                        builder.Data1 = 71;
                        break;
                    case 8:
                        builder.Data1 = 72;
                        break;
                    case 9:
                        builder.Data1 = 74;
                        break;
                    default:
                    builder.Data2 = 0;
                    break;
                }
        }

        public void grammophone()
        {
            loopchecker = 1;
            using (OutputDevice outDevice = new OutputDevice(0))
            {
                for (int i = 0; i < 4; i++)
                {
                    builder.Command = ChannelCommand.NoteOn;
                    builder.MidiChannel = 0;

                    checkSound(playOrder[i, 0], playOrder[i, 1]);

                    builder.Build();
                    outDevice.Send(builder.Result);

                    Thread.Sleep(2000/duration);

                    builder.Command = ChannelCommand.NoteOff;
                    builder.Data2 = 0;
                    builder.Build();

                    outDevice.Send(builder.Result);
                }
                reset();
                loopchecker = 0;
            }
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
