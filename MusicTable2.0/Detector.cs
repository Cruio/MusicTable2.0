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
        Mat capture = new Mat();
        Mat invertedCapture = new Mat();
        Mat captureWithKeypoints = new Mat();
        public delegate void UseForm1();
        private SimpleBlobDetector detector = new SimpleBlobDetector();
        private VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        private Mat hierarchy = new Mat();
        private bool[] hasChild;
        private int[] noteType;
        private int noteAmount = 0;
        private int[] requiredCols;
        
        public int Looper()
        {
            VideoCapture cap;
            try
            {
                cap = new VideoCapture(0);
            } catch (NullReferenceException e)
            {
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
                CvInvoke.MedianBlur(capture, capture, 3);
                
                CvInvoke.Threshold(capture, capture, 215, 255, ThresholdType.Binary);
                CvInvoke.NamedWindow("source", NamedWindowType.AutoSize);
                CvInvoke.Erode(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), point, 1, BorderType.Default, new MCvScalar(1.0));
                CvInvoke.Dilate(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), point, 1, BorderType.Default, new MCvScalar(1.0));
                CvInvoke.Imshow("source", capture);
                CvInvoke.FindContours(capture, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple, new Point(0, 0));
                CvInvoke.BitwiseNot(capture, invertedCapture);
                VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(detector.Detect(invertedCapture));
                Features2DToolbox.DrawKeypoints(invertedCapture, keyPoints, captureWithKeypoints, new Bgr(255, 0, 0));
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
            Console.WriteLine("I'm here!");
            noteType = new int[contours.Size];
            
            for (int i = 0; i < contours.Size; i++)
            {
                Debug.WriteLine(GetHierarchy(hierarchy, i));
                if (!CheckChild(i))
                {
                    Debug.WriteLine(CvInvoke.ContourArea(contours[i]));
                    Debug.WriteLine(CvInvoke.ArcLength(contours[i], true));
                    if (CvInvoke.ArcLength(contours[i], true) > 480 && CvInvoke.ArcLength(contours[i], true) < 600)
                    {
                        noteType[i] = 3;
                        Debug.WriteLine("Eighth!");
                    }
                    else if (CvInvoke.ArcLength(contours[i], true) < 480 && CvInvoke.ArcLength(contours[i], true) > 270)
                    {
                        noteType[i] = 2;
                        Debug.WriteLine("Quarter!");
                    }
                    Debug.WriteLine("No child!");
                }
                else if (CheckChild(i))
                {
                    Debug.WriteLine(CvInvoke.ContourArea(contours[i]));
                    Debug.WriteLine(CvInvoke.ArcLength(contours[i], true));
                    if (CvInvoke.ContourArea(contours[i]) > 1200 && CvInvoke.ContourArea(contours[i]) < 2200 && CvInvoke.ArcLength(contours[i], true) < 200)
                    {
                        noteType[i] = 0;
                        Debug.WriteLine("Whole!");
                    }
                    else if (CvInvoke.ArcLength(contours[i], true) > 250)
                    {
                        noteType[i] = 1;
                        Debug.WriteLine("Half!");
                    }
                    Debug.WriteLine("Child!");
                }
            }
            for (int i = 0; i < noteType.Length; i++)
            {
                if (noteType[i] == 3 || noteType[i] == 4)
                {
                    CvInvoke.Erode(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(27, 27), new Point(13, 13)), new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                    CvInvoke.Dilate(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(27, 27), new Point(13, 13)), new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                    CvInvoke.BitwiseNot(capture, capture);
                }
            }
            StartScreen.gameForm.StartStar();
            VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(detector.Detect(capture));
            if (StartScreen.gameForm.controlValue >= 1 || StartScreen.gameForm.controlValue <= 3)
            {
                requiredCols =new int[1] { 3 };
            }
            else if (StartScreen.gameForm.controlValue >= 4 || StartScreen.gameForm.controlValue <= 6)
            {
                requiredCols = new int[2] { 1, 3 };
            }
            else
            {
                requiredCols = new int[4] { 0, 1, 2, 3 };
            }
            int[] requiredRow = StartScreen.gameForm.rowPos;
            int colWidth = 88;
            for (int i = 0; i < keyPoints.Size; i++)
            {
                int x = (int)keyPoints[i].Point.X;
                int y = (int)keyPoints[i].Point.Y;
                int firstY = 120;
                int cols = 0;
                int rows = 0;
                while (cols < 4)
                {
                    int line = 12;
                    int space = 36;
                    int firstX = 85;
                    if (cols == 0)
                    {
                        line = 13;
                        space = 37;
                        firstX = 80;
                    }
                    rows = 0;
                    while (rows < 9)
                    {
                        //if (cols == 0) firstX += 10;
                        if (rows % 2 == 0)
                        {
                            if (x > firstX && x < firstX + line && y > firstY && y < firstY + colWidth)
                            {
                                Debug.WriteLine("There's a note here: " + rows + " " + cols);
                                for(int j = 0; j < requiredCols.Length; j++)
                                {
                                    if (cols == requiredCols[j] && rows == requiredRow[j])
                                    {
                                        Debug.WriteLine("Correct position!");
                                    }
                                    else Debug.WriteLine("Wrong!");
                                }
                            }
                            firstX += line;
                        }
                        else
                        {
                            if (x > firstX && x < firstX + space && y > firstY && y < firstY + colWidth && rows % 2 != 0)
                            {
                                Debug.WriteLine("There's a note here:" + rows + " " + cols);
                                for (int j = 0; j < requiredCols.Length; j++)
                                {
                                    if (cols == requiredCols[j] && rows == requiredRow[j])
                                    {
                                        Debug.WriteLine("Correct position!");
                                    }
                                    else Debug.WriteLine("Wrong!");
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
            //else
            //{
            //    return new int[] { };
            //}

            return ret;
        }
    }

}
