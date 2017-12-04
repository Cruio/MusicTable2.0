using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Windows.Forms;
using Emgu.CV.XImgproc;
using Emgu.CV.XObjdetect;
using Emgu.CV.Features2D;
using Emgu.CV.UI;
using Emgu.CV.Util;
using System.Runtime.InteropServices;

namespace MusicTable2._0
{
    class Detector
    {
        Sound sound = new Sound();
        Mat capture = new Mat();
        Mat invertedCapture = new Mat();
        Mat captureWithKeypoints = new Mat();
        public delegate void UseForm1();
        private SimpleBlobDetector detector = new SimpleBlobDetector();
        private VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        private Mat hierarchy = new Mat();
        private bool[] hasChild;
        private int[,] notes;
        private int noteAmount = 0;
        private int[] requiredCols;

        public int Looper()
        {
            VideoCapture cap;
            try
            {
                cap = new VideoCapture(2);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e);
                return 1;
            }
            if (!cap.IsOpened) return 2;
            int counter = 0;
            Size size = new Size(2, 2);
            Point point = new Point(1, 1);
            MCvScalar scalar = new MCvScalar(1);
            while (true)
            {
                cap.Read(capture);
                CvInvoke.CvtColor(capture, capture, ColorConversion.Bgr2Gray, 0);
                CvInvoke.MedianBlur(capture, capture, 5);
                CvInvoke.Threshold(capture, capture, 215, 255, ThresholdType.Binary);
                CvInvoke.NamedWindow("source", NamedWindowType.AutoSize);
                CvInvoke.Erode(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), point, 3, BorderType.Default, new MCvScalar(1.0));
                CvInvoke.Dilate(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), point, 5, BorderType.Default, new MCvScalar(1.0));
                CvInvoke.MedianBlur(capture, capture, 5);
                CvInvoke.Imshow("source", capture);
                CvInvoke.FindContours(capture, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple, new Point(0, 0));
                CvInvoke.BitwiseNot(capture, invertedCapture);
                VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(detector.Detect(invertedCapture));
                Features2DToolbox.DrawKeypoints(invertedCapture, keyPoints, captureWithKeypoints, new Bgr(0, 0, 255));
                CvInvoke.NamedWindow("KeyPoints", NamedWindowType.AutoSize);
                CvInvoke.Imshow("KeyPoints", captureWithKeypoints);



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
                            CheckNotes();
                            counter = 0;
                        }
                    }
                }
                if (CvInvoke.WaitKey(10) == 27) break;
            }

            return 0;
        }
        private int GetMatData(Mat mat, int row, int col)
        {
            int[] value = new int[1];
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 1);
            return value[0];
        }
        private void CheckNotes()
        {
            noteAmount = 0;
            Console.WriteLine("I'm here!");
            //notes contains information about the type of note and the index of the blob belonging to that note, which is necessary to properly identify the note.
            //If notes[i,0] is equal to 0, there is no note for that index.
            notes = new int[4, 1];
            notes[0, 0] = 0;
            notes[1, 0] = 0;
            notes[2, 0] = 0;
            notes[3, 0] = 0;
            int noteIndex = 4;
            Mat blobDetectMat = new Mat();
            VectorOfKeyPoint keyPoints;

            int reqNoteType;
            int reqNoteAmount;
            Debug.WriteLine(StartScreen.gameForm.controlValue);
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
            for (int i = 0; i < contours.Size; i++)
            {
                Debug.WriteLine(GetHierarchy(hierarchy, i));
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
                    if (CvInvoke.ContourArea(contours[i]) > 1200 && CvInvoke.ContourArea(contours[i]) < 2600 && CvInvoke.ArcLength(contours[i], true) < 220)
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
            int correctNoteCounter = 0;
            int[] requiredRow = StartScreen.gameForm.rowPos;
            int colWidth = 97;
            blobDetectMat = capture;
            if (reqNoteType == 3 || reqNoteType == 4)
            {
                CvInvoke.Erode(capture, blobDetectMat, CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(27, 27), new Point(13, 13)), new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                CvInvoke.Dilate(capture, blobDetectMat, CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(27, 27), new Point(13, 13)), new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                CvInvoke.BitwiseNot(capture, blobDetectMat);
            }
            keyPoints = new VectorOfKeyPoint(detector.Detect(blobDetectMat));
            Features2DToolbox.DrawKeypoints(blobDetectMat, keyPoints, captureWithKeypoints, new Bgr(0, 0, 255));
            CvInvoke.NamedWindow("KeyPoints2", NamedWindowType.AutoSize);
            CvInvoke.Imshow("KeyPoints2", captureWithKeypoints);
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
                        //if (cols == 0) firstX += 10;
                        if (rows % 2 == 0)
                        {
                            if (x > firstX && x < firstX + line && y > firstY && y < firstY + colWidth)
                            {
                                Debug.WriteLine("There's a note here: " + rows + " " + cols);
                                for (int j = 0; j < requiredCols.Length; j++)
                                {
                                    if (cols == requiredCols[j] && (rows == requiredRow[0] || rows == requiredRow[1] || rows == requiredRow[2] || rows == requiredRow[3]) && notes[cols, 0] == reqNoteType && noteAmount == reqNoteAmount)
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
                            if (x > firstX && x < firstX + space && y > firstY && y < firstY + colWidth)
                            {
                                Debug.WriteLine("There's a note here:" + rows + " " + cols);
                                for (int j = 0; j < requiredCols.Length; j++)
                                {
                                    if (cols == requiredCols[j] && (rows == requiredRow[0] || rows == requiredRow[1] || rows == requiredRow[2] || rows == requiredRow[3]) && notes[cols, 0] == reqNoteType && noteAmount == reqNoteAmount)
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
            if (correctNoteCounter == reqNoteAmount)
            {
                sound.startRecord();
                StartScreen.gameForm.controlValue++;
            }
            StartScreen.gameForm.StartStar();
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
