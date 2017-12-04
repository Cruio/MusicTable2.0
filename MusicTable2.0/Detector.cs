using System;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using Emgu.CV.Util;


namespace MusicTable2._0
{
    class Detector
    {
        //Define basic necessary variables. Instantiate some objects.
        Sound sound = new Sound();
        Mat capture = new Mat();
        Mat invertedCapture = new Mat();
        Mat captureWithKeypoints = new Mat();
        public delegate void UseForm1();
        private SimpleBlobDetector detector = new SimpleBlobDetector();
        private VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        private Mat hierarchy = new Mat();
        private int[,] notes;
        private int noteAmount = 0;
        private int[] requiredCols;

        public int Looper()
        {
            VideoCapture cap;
            //Try to initialise the VideoCapture with the default camera. Catches NullReferenceException
            try
            {
                cap = new VideoCapture(0);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e);
                return 1;
            }
            //Check if camera is opened. If not, exit the looper with code 2.
            if (!cap.IsOpened) return 2;
            int counter = 0;
            Size size = new Size(2, 2);
            Point point = new Point(1, 1);
            MCvScalar scalar = new MCvScalar(1);
            //Create infinite loop for checking whether there is a blob on the play button
            while (true)
            {
                //Stores image from camera into capture
                cap.Read(capture);
                //Make the image grayscake, use median blur with a kernel size of 5 then threshold it
                CvInvoke.CvtColor(capture, capture, ColorConversion.Bgr2Gray, 0);
                CvInvoke.MedianBlur(capture, capture, 5);
                CvInvoke.Threshold(capture, capture, 215, 255, ThresholdType.Binary);
                //Eroding and dilating to get rid of most of the noise
                CvInvoke.Erode(capture, capture, 
                    CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), 
                    point, 3, BorderType.Default, new MCvScalar(1.0));
                CvInvoke.Dilate(capture, capture, 
                    CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), 
                    point, 5, BorderType.Default, new MCvScalar(1.0));
                //Another median blur with a kernel size of 5 in order to smooth out the image and get rid of any remaining noise.
                CvInvoke.MedianBlur(capture, capture, 5);
                //Show capture in a named window
                CvInvoke.NamedWindow("source", NamedWindowType.AutoSize);
                CvInvoke.Imshow("source", capture);
                //Invert capture to check for blobs. Use SimpleBlobDetector to find them, then show them in a separate window.
                CvInvoke.BitwiseNot(capture, invertedCapture);
                VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(detector.Detect(invertedCapture));
                Features2DToolbox.DrawKeypoints(invertedCapture, keyPoints, captureWithKeypoints, new Bgr(0, 0, 255));
                CvInvoke.NamedWindow("KeyPoints", NamedWindowType.AutoSize);
                CvInvoke.Imshow("KeyPoints", captureWithKeypoints);
                //Loop through all the keypoints found by the SimpleBlobDetector. 
                //If there is one on the play button, it will add 1 to the counter and the thread will sleep for 1/30 of a second.
                for (int i = 0; i < keyPoints.Size; i++)
                {
                    int x;
                    int y;
                    x = (int)keyPoints[i].Point.X;
                    y = (int)keyPoints[i].Point.Y;
                    Debug.WriteLine(x);
                    Debug.WriteLine(y);
                    if (x < 490 && x > 415 && y < 115 && y > 45)
                    {
                        counter++;
                        System.Threading.Thread.Sleep(1000 / 30);
                        if (counter == 30)
                        {
                            //If the counter has reached 30, it'll run the CheckNotes() function and then reset the counter.
                            CheckNotes();
                            counter = 0;
                        }
                    }
                }
                //Exit capture by pressing escape
                if (CvInvoke.WaitKey(10) == 27) break;
            }
            return 0;
        }
        private void CheckNotes()
        {
            noteAmount = 0;
            Console.WriteLine("I'm here!");
            //notes contains information about the type of note, which is necessary to properly identify the note.
            //If notes[i,0] is equal to 0, there is no note for that index.
            notes = new int[4, 1];
            notes[0, 0] = 0;
            notes[1, 0] = 0;
            notes[2, 0] = 0;
            notes[3, 0] = 0;
            //noteIndex starts at 4 and counts down for every note detected.
            int noteIndex = 4;
            Mat blobDetectMat = new Mat();
            VectorOfKeyPoint keyPoints;
            //Find contours.
            CvInvoke.FindContours(capture, contours, hierarchy, RetrType.Tree, 
                ChainApproxMethod.ChainApproxSimple, new Point(0, 0));
            int reqNoteType;
            int reqNoteAmount;
            Debug.WriteLine(StartScreen.gameForm.controlValue);
            //Using a variable from the forms to check requirements
            if (StartScreen.gameForm.controlValue >= 0 && StartScreen.gameForm.controlValue <= 2)
            {
                requiredCols = new int[1] { 3 };
                reqNoteType = 1;
                reqNoteAmount = 1;
                blobDetectMat = capture;

            }
            else if (StartScreen.gameForm.controlValue >= 3 && StartScreen.gameForm.controlValue <= 5)
            {
                requiredCols = new int[2] { 3, 1 };
                reqNoteType = 2;
                reqNoteAmount = 2;
                blobDetectMat = capture;
            }
            else if (StartScreen.gameForm.controlValue >= 6 && StartScreen.gameForm.controlValue <= 8)
            {
                requiredCols = new int[4] { 3, 2, 1, 0 };
                reqNoteType = 3;
                reqNoteAmount = 4;
            }
            else
            {
                requiredCols = new int[4] { 3, 2, 1, 0 };
                reqNoteType = 4;
                reqNoteAmount = 4;
            }
            //for loop to go through all the contours and identify what kind of note it is.
            for (int i = 0; i < contours.Size; i++)
            {
                Debug.WriteLine(GetHierarchy(hierarchy, i));
                //Uses the CheckChild(int) method to check for child contours.
                if (!CheckChild(i))
                {
                    Debug.WriteLine(CvInvoke.ContourArea(contours[i]));
                    Debug.WriteLine(CvInvoke.ArcLength(contours[i], true));

                    if (CvInvoke.ArcLength(contours[i], true) > 460 && CvInvoke.ArcLength(contours[i], true) < 600)
                    {
                        noteIndex--;
                        notes[noteIndex, 0] = 4;
                        noteAmount++;
                        Debug.WriteLine("Eighth!");
                    }
                    else if (CvInvoke.ArcLength(contours[i], true) < 460 && CvInvoke.ArcLength(contours[i], true) > 270)
                    {
                        noteIndex--;
                        notes[noteIndex, 0] = 3;
                        noteAmount++;
                        Debug.WriteLine("Quarter!");
                    }
                    Debug.WriteLine("No child!");
                }
                else if (CheckChild(i))
                {
                    Debug.WriteLine(CvInvoke.ContourArea(contours[i]));
                    Debug.WriteLine(CvInvoke.ArcLength(contours[i], true));
                    if (CvInvoke.ContourArea(contours[i]) > 1200 && CvInvoke.ContourArea(contours[i]) < 2600 
                        && CvInvoke.ArcLength(contours[i], true) < 220)
                    {
                        noteIndex--;
                        notes[noteIndex, 0] = 1;
                        noteAmount++;

                        Debug.WriteLine("Whole!");

                    }
                    else if (CvInvoke.ArcLength(contours[i], true) > 250)
                    {
                        noteIndex--;
                        notes[noteIndex, 0] = 2;
                        noteAmount++;
                        noteIndex--;
                        Debug.WriteLine("Half!");

                    }
                    Debug.WriteLine("Child!");
                }
            }
            //Get variably from the form to check where the notes are supposed to be.
            int correctNoteCounter = 0;
            int[] requiredRow = StartScreen.gameForm.rowPos;
            int colWidth = 97;
            blobDetectMat = capture;
            //If it is a quarter or eighth note, it will first erode it with a 
            //large size and then dilate it to get rid of the stems. It is then inverted.
            if (reqNoteType == 3 || reqNoteType == 4)
            {
                CvInvoke.Erode(capture, blobDetectMat, 
                    CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(27, 27), new Point(13, 13)), 
                    new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                CvInvoke.Dilate(capture, blobDetectMat, 
                    CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(27, 27), new Point(13, 13)), 
                    new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                CvInvoke.BitwiseNot(capture, blobDetectMat);
            }
            //Detecting blobs and displaying keypoints.
            keyPoints = new VectorOfKeyPoint(detector.Detect(blobDetectMat));
            Features2DToolbox.DrawKeypoints(blobDetectMat, keyPoints, captureWithKeypoints, new Bgr(0, 0, 255));
            CvInvoke.NamedWindow("KeyPoints2", NamedWindowType.AutoSize);
            CvInvoke.Imshow("KeyPoints2", captureWithKeypoints);
            //Looping through the keypoints to see whether they're in the right position
            for (int i = 0; i < keyPoints.Size; i++)
            {
                int x = (int)keyPoints[i].Point.X;
                int y = (int)keyPoints[i].Point.Y;
                int firstY = 60;
                int cols = 0;
                int line = 12;
                int space = 36;
                int firstX = 85;
                if (reqNoteType == 3 || reqNoteType == 4)
                {
                    x += 21;
                }
                //Using a double while loop to go through all the columns and rows.
                while (cols < 4)
                {
                    line = 12;
                    space = 36;
                    firstX = 85;
                    if (reqNoteType == 3 || reqNoteType == 4)
                    {
                        line = 13;
                        space = 36;
                        firstX = 80;
                    }
                    if (cols == 0)
                    {
                            line = 14;
                            space = 36;
                            firstX = 80;
                    }
                    int rows = 0;
                    while (rows < 9)
                    {
                        if (rows % 2 == 0)
                        {
                            /*Checking whether detected blobs are on a line. 
                            Note, that if a note is detected, and something else that is perceived as a 
                            blob is placed in the correct position, it would still count as right. In theory.*/
                            if (x > firstX && x < firstX + line && y > firstY && y < firstY + colWidth)
                            {
                                Debug.WriteLine("There's a note here: " + rows + " " + cols);
                                for (int j = 0; j < requiredCols.Length; j++)
                                {
                                    //Checking if the note matches all the criteria. 
                                    //Send values to the Sound class if so, and add 1 to the correct note counter.
                                    if (cols == requiredCols[j] 
                                        && (rows == requiredRow[0] || rows == requiredRow[1] || rows == requiredRow[2] || rows == requiredRow[3]) 
                                        && notes[cols, 0] == reqNoteType && noteAmount == reqNoteAmount)
                                    {
                                        Debug.WriteLine("Correct position and type!");
                                        sound.playOrder[cols, 0] = notes[cols, 0];
                                        sound.playOrder[cols, 1] = rows+1;
                                        correctNoteCounter++;
                                    }
                                }
                            }
                            firstX += line;
                        }
                        else
                        {
                            //Same as above. Just for spaces.
                            if (x > firstX && x < firstX + space && y > firstY && y < firstY + colWidth)
                            {
                                Debug.WriteLine("There's a note here:" + rows + " " + cols);
                                for (int j = 0; j < requiredCols.Length; j++)
                                {
                                    if (cols == requiredCols[j] 
                                        && (rows == requiredRow[0] || rows == requiredRow[1] || rows == requiredRow[2] || rows == requiredRow[3]) 
                                        && notes[cols, 0] == reqNoteType && noteAmount == reqNoteAmount)
                                    {
                                        Debug.WriteLine("Correct position and type!");
                                        sound.playOrder[cols, 0] = notes[cols, 0];
                                        sound.playOrder[cols, 1] = rows+1;
                                        correctNoteCounter++;
                                    }
                                }
                            }
                            firstX += space;
                        }
                        rows++;
                    }
                    firstY += colWidth;
                    cols++;
                }
            }
            //If the excact amount of required notes were placed correctly, start playing the sound and add 1 to the controlValue.
            if (correctNoteCounter == reqNoteAmount)
            {
                sound.startRecord();
                StartScreen.gameForm.controlValue++;
            }
            //Randomise notes whether it was right or not.
            StartScreen.gameForm.StartStar();
            //Put this thread to sleep for 5 seconds to give the user time to clear the table, and to give the music time to play.
            System.Threading.Thread.Sleep(5000);
        }
        bool CheckChild(int i)
        {
            if (GetHierarchy(hierarchy, i)[2] == i + 1 && GetHierarchy(hierarchy, i + 1)[3] == i) return true;
            else return false;
        }


        /// >>>>Based on [joshuanapoli] answer<<<
        /// <summary>
        /// Get a neighbor index in the heirarchy tree.
        /// </summary>
        /// <returns>
        /// A neighbor index or -1 if the given neighbor does not exist.
        /// </returns>
        //public int Get(HierarchyIndex component, int index)
        //public int GetHierarchy(Mat Hierarchy, int contourIdx, int component)
        public int[] GetHierarchy(Mat Hierarchy, int contourIdx)
        {
            int[] ret = new int[] { };

            if (Hierarchy.Depth != Emgu.CV.CvEnum.DepthType.Cv32S)
            {
                throw new ArgumentOutOfRangeException("ContourData must have Cv32S hierarchy element type.");
            }
            if (Hierarchy.Rows != 1)
            {
                throw new ArgumentOutOfRangeException("ContourData must have one hierarchy hierarchy row.");
            }
            if (Hierarchy.NumberOfChannels != 4)
            {
                throw new ArgumentOutOfRangeException("ContourData must have four hierarchy channels.");
            }
            if (Hierarchy.Dims != 2)
            {
                throw new ArgumentOutOfRangeException("ContourData must have two dimensional hierarchy.");
            }
            long elementStride = Hierarchy.ElementSize / sizeof(Int32);
            var offset0 = (long)0 + contourIdx * elementStride;
            if (0 <= offset0 && offset0 < Hierarchy.Total.ToInt64() * elementStride)
            {


                var offset1 = (long)1 + contourIdx * elementStride;
                var offset2 = (long)2 + contourIdx * elementStride;
                var offset3 = (long)3 + contourIdx * elementStride;

                ret = new int[4];

                unsafe
                {
                    //return *((Int32*)Hierarchy.DataPointer.ToPointer() + offset);

                    ret[0] = *((Int32*)Hierarchy.DataPointer.ToPointer() + offset0);
                    ret[1] = *((Int32*)Hierarchy.DataPointer.ToPointer() + offset1);
                    ret[2] = *((Int32*)Hierarchy.DataPointer.ToPointer() + offset2);
                    ret[3] = *((Int32*)Hierarchy.DataPointer.ToPointer() + offset3);
                }


            }
            return ret;
        }
    }

}
