using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Mat capture;
        Mat invertedCapture;
        Mat captureWithKeypoints;
        private SimpleBlobDetector detector;
        private VectorOfVectorOfPoint contours;
        private Mat hierarchy;
        private bool[] hasChild;
        private int[] noteType;
        private int noteAmount;
        public int[,] notePositions = new int[4, 9];

        public int Looper()
        {
            
            VideoCapture cap = new VideoCapture();
            int counter = 0;
            Size size = new Size(3, 3);
            Point point = new Point(1, 1);
            MCvScalar scalar = new MCvScalar(1);
            while (true)
            {
                cap.Read(capture);
                CvInvoke.MedianBlur(capture, capture, 3);
                CvInvoke.CvtColor(capture, capture, ColorConversion.Bgr2Gray, 0);
                CvInvoke.Threshold(capture, capture, 215, 255, ThresholdType.Binary);
                CvInvoke.NamedWindow("source", NamedWindowType.AutoSize);
                CvInvoke.Erode(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), point, 1, BorderType.Default, new MCvScalar(1.0));
                CvInvoke.Dilate(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, size, point), point, 1, BorderType.Default, new MCvScalar(1.0));
                CvInvoke.Imshow("source", capture);
                CvInvoke.FindContours(capture, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple, new Point(0, 0));
                CvInvoke.BitwiseNot(capture, invertedCapture);
                VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(detector.Detect(invertedCapture));
                Features2DToolbox.DrawKeypoints(invertedCapture, keyPoints, captureWithKeypoints, new Bgr(255, 0, 0));
                for (int i = 0; i < keyPoints.Size; i++)
                {
                    int x;
                    int y;
                    x = (int)keyPoints[i].Point.X;
                    y = (int)keyPoints[i].Point.Y;
                    if (x < 100 && x > 90 && y < 100 && y > 90)
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
            hasChild = new bool[contours.Size];
            for (int i = 0; i < contours.Size; i++)
            {
                hasChild[i] = false;
                if (GetMatData(hierarchy, i, 3) == i - 1 && GetMatData(hierarchy, i - 1, 2) == i) hasChild[i] = true;
                else hasChild[i] = false;
            }
            for (int i = 0; i < contours.Size; i++)
            {
                if (!hasChild[i])
                {
                    if (CvInvoke.ArcLength(contours[i], true) > 480 && CvInvoke.ArcLength(contours[i], true) < 600)
                    {
                        noteType[i] = 3;
                    }
                    else if (CvInvoke.ArcLength(contours[i], true) < 480 && CvInvoke.ArcLength(contours[i], true) > 270)
                    {
                        noteType[i] = 2;
                    }
                }
                else if (hasChild[i])
                {
                    if (CvInvoke.ContourArea(contours[i]) > 1200 && CvInvoke.ContourArea(contours[i]) < 2200 && CvInvoke.ArcLength(contours[i], true) < 200)
                    {
                        noteType[i] = 0;
                    }
                    else if (CvInvoke.ArcLength(contours[i], true) > 250)
                    {
                        noteType[i] = 1;
                    }
                }
            }
            int line = 18;
            int space = 34;
            int colWidth = 88;
            for (int i = 0; i < noteType.Length; i++)
            {
                if (noteType[i] == 3 || noteType[i] == 4)
                {
                    CvInvoke.Erode(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(27, 27), new Point(13, 13)), new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                    CvInvoke.Dilate(capture, capture, CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(27, 27), new Point(13, 13)), new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                    CvInvoke.BitwiseNot(capture, capture);
                }
            }
            VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(detector.Detect(capture));
            for (int i = 0; i < keyPoints.Size; i++)
            {
                int x = (int)keyPoints[i].Point.X;
                int y = (int)keyPoints[i].Point.Y;
                int firstY = 120;
                int cols = 0;
                int rows = 0;
                while (cols < 4)
                {
                    int firstX = 80;
                    rows = 0;
                    while (rows < 9)
                    {

                        if (rows % 2 == 0)
                        {
                            if (x > firstX && x < firstX + line && y > firstY && y < firstY + colWidth)
                            {

                            }
                            firstX += line;
                        }
                        else
                        {
                            if (x > firstX && x < firstX + space && y > firstY && y < firstY + colWidth && rows % 2 != 0)
                            {

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
    }

}
