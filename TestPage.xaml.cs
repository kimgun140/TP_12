using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TP_12
{
    /// <summary>
    /// TestPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();
            OCTEST();
        }









        public void OCTEST()
        {
         Mat mat =    Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\testimage.png");

            if (mat == null) {
                MessageBox.Show("이미지 없음");
            }

            Mat grayImg = new Mat();
            Cv2.CvtColor(mat, grayImg, ColorConversionCodes.BGR2GRAY);

            Mat denoisedImg = new Mat();
            Cv2.GaussianBlur(grayImg, denoisedImg, new OpenCvSharp.Size(3, 3), 1); // 노란 바탕의 글은 노이즈 제거하면 되네 


            Mat mat1 = new Mat();
            //CircleSegment[] circleSegments =  Cv2.HoughCircles(denoisedImg, HoughModes.Gradient,1,20,1,50,0,0); // 투 매니


            ////Cv2.CreateTrackbar("track1","img",50);
            ////Cv2.CreateTrackbar("track2", "img", 50);
            ////Cv2.CreateTrackbar("track3", "img", 50);

            ////CvSeq<CircleSegment> circles = Cv2.HoughCircles(grayImg, HoughModes.Gradient,1,100,100,100,20,100);
            ////OpenCvSharp.CircleSegment[] circleSegments  =  Cv2.HoughCircles(grayImg, HoughModes.Gradient, 1, 100);

            //foreach (OpenCvSharp.CircleSegment circleSegment in circleSegments)
            //{
            //    Cv2.Circle(mat, (OpenCvSharp.Point)circleSegment.Center, (int)circleSegment.Radius, Scalar.Blue);

            //}

            Cv2.ImShow("gray", grayImg);

            Cv2.ImShow("img", denoisedImg);



            Cv2.WaitKey(0);

        }
        public void drawcircle()
        {
            //rmin = Cv2.GetTrackbarPos("minRadius", "img");
            //rmax = Cv2.getTrackbarPos('maxRadius', 'img');
            //th = Cv2.getTrackbarPos('threshold', 'img');

        }
    }

}
