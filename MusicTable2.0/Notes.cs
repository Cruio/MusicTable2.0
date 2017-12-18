using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace MusicTable2._0
{
    class Notes
    {
        VectorOfPoint contour = new VectorOfPoint();
        private PointF location=new PointF();
        private Mat blobMat;
        private Mat hierarchy = new Mat();
        private int noteType;
        private bool isNote;
        private bool hasChild;
        private int width;
        private SimpleBlobDetector detector = new SimpleBlobDetector();
        public Notes(VectorOfPoint c, Mat mat, bool cc, int w)
        {
            contour = c;
            blobMat = new Mat(mat, new Range(0, mat.Rows), new Range(0, mat.Cols));
            hasChild = cc;
            width = w/2;
            isNote = false;
            FigureOutWhatNoteItIs();            
        }

        private void FigureOutWhatNoteItIs()
        {
            if (!hasChild)
            {
                if (CvInvoke.ArcLength(contour, true) > 460 && CvInvoke.ArcLength(contour, true) < 600)
                {
                    isNote = true;
                    noteType = 4;
                    location = GetLocation();
                }
                else if (CvInvoke.ArcLength(contour, true) < 460 && CvInvoke.ArcLength(contour, true) > 270)
                {
                    isNote = true;
                    noteType = 3;
                    location = GetLocation();
                }
            }
            else if (hasChild)
            {
                if (CvInvoke.ContourArea(contour) > 1200 && CvInvoke.ContourArea(contour) < 2600
                    && CvInvoke.ArcLength(contour, true) < 220)
                {
                    isNote = true;
                    noteType = 1;
                    location = GetLocation();
                }
                else if (CvInvoke.ArcLength(contour, true) > 250)
                {
                    isNote = true;
                    noteType = 2;
                    location = GetLocation();
                }
            }
        }
        private PointF GetLocation()
        {
            int x = CvInvoke.BoundingRectangle(contour).Right - CvInvoke.BoundingRectangle(contour).Width / 2;
            int y = CvInvoke.BoundingRectangle(contour).Bottom - CvInvoke.BoundingRectangle(contour).Height / 2;
            PointF p = new PointF(x,y);
            Mat usefulMat = new Mat();
            if (noteType > 2)
            {
                CvInvoke.Erode(blobMat, usefulMat,
                    CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(27, 27), new Point(13, 13)),
                    new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                CvInvoke.Dilate(usefulMat, usefulMat,
                    CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(27, 27), new Point(13, 13)),
                    new Point(1, 1), 1, BorderType.Default, new MCvScalar(1));
                CvInvoke.BitwiseNot(usefulMat, usefulMat);
            }
            else usefulMat = blobMat;
            VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(detector.Detect(usefulMat));
            for (int i = 0; i < keyPoints.Size; i++)
            {
                if (keyPoints[i].Point.Y - width < p.Y && keyPoints[i].Point.Y + width > p.Y)
                {
                    p.X = keyPoints[i].Point.X;
                    p.Y = keyPoints[i].Point.Y;
                    break;
                }
            }
            return p;
        }
        public PointF Location { get => location; set => location = value; }
        public int NoteType { get => noteType; set => noteType = value; }
        public bool IsNote { get => isNote; set => isNote = value; }
    }
}