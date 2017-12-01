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
        //The ChannelMessageBuilder comes from using Sanford.Multimedia.Midi. This is needed to generate the different sounds.
        ChannelMessageBuilder builder = new ChannelMessageBuilder();

        //The loopchecker is an int used to check whether or not the system is currenly playing sounds. 
        //It will be used to prohibit certain functions, so as not to fuck it up.
        int loopchecker = 0;

        //duration is used determine how long sounds are played.
        int duration;

        //playOrder is used to store the desired pitch and shape of a note. 

        public int[,] playOrder = new int[4, 2];

        //startRecord is the function that starts playing sounds. If loopchecker = 0, that is no sound is currently being played, it initializes a new thread and makes it run
        //the function grammophone. It then starts said thread.
        public void startRecord()
        {
            if (loopchecker == 0)
            {
                Thread wavPlayer = new Thread(grammophone);
                wavPlayer.Start();
            }
        }

        //checkSound assigns the desired values to the ChannelMessageBuilder object named builder.
        public void checkSound(int shape, int pitch)
        {
                //Data2 is the volume of the sound, with 127 being maximum.
                builder.Data2 = 127;

                if (shape == 1)
                {
                    duration = 1;
                }
                else if (shape == 2)
                {
                    duration = 2;
                }
                else if (shape == 3)
                {
                    duration = 4;
                }
                else if (shape == 4)
                {
                    duration = 8;
                }
                else
                {
                duration = 2000;
                }


                //Data1 is the pitch of the sound.
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
                    //if no pitch has been assigned, the volume is lowered to 0, so as not to play any sound.
                    builder.Data2 = 0;
                    break;
                }
        }

        //grammophone sets loopchecker to 1, to prevent it from being started again before being finished. It then goes through a loop
        //where it uses the values from playOrder to play the different notes in order.
        void grammophone()
        {
            if (loopchecker == 0)
            {
                loopchecker = 1;
                using (OutputDevice outDevice = new OutputDevice(0))
                {
                    for (int i = 3; i >-1; i--)
                    {
                        builder.Command = ChannelCommand.NoteOn;
                        builder.MidiChannel = 0;

                        checkSound(playOrder[i, 0], playOrder[i, 1]);

                        builder.Build();
                        outDevice.Send(builder.Result);

                        Thread.Sleep(2000 / duration);

                        builder.Command = ChannelCommand.NoteOff;
                        builder.Data2 = 0;
                        builder.Build();

                        outDevice.Send(builder.Result);
                    }
                    reset();
                    loopchecker = 0;
                }
            }
        }

        //Use this function outside of this class to set values in playOrder
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
