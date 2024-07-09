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
            //OCTEST();
            targetimg();
        }



        public static void OCTEST()
        {
            Mat mat = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\testimage.png");

            if (mat.Empty())
            {
                MessageBox.Show("이미지 없음");
                return;
            }

            Mat grayImg = new Mat();
            Cv2.CvtColor(mat, grayImg, ColorConversionCodes.BGR2GRAY);

            Mat denoisedImg = new Mat();
            Cv2.GaussianBlur(grayImg, denoisedImg, new OpenCvSharp.Size(3, 3), 1); // 노이즈 제거

            // 매개변수 조정
            double dp = 1;
            double minDist = denoisedImg.Rows / 8; // 검출된 원의 중심들 간의 최소 거리
            double param1 = 100; // 캐니 엣지 검출기의 높은 임곗값
            double param2 = 30; // 중심 검출기 임곗값 (값이 낮을수록 더 많은 원이 검출될 수 있음)
            int minRadius = 40; // 검출할 원의 최소 반지름
            int maxRadius = 100; // 검출할 원의 최대 반지름

            CircleSegment[] circleSegments = Cv2.HoughCircles(denoisedImg, HoughModes.Gradient, dp, minDist, param1: param1, param2: param2, minRadius: minRadius, maxRadius: maxRadius);

            foreach (CircleSegment circleSegment in circleSegments)
            {
                Cv2.Circle(mat, (OpenCvSharp.Point)circleSegment.Center, (int)circleSegment.Radius, Scalar.Blue, 2); // 원을 그리기
            }

            Cv2.ImShow("gray", grayImg);
            Cv2.ImShow("img", mat);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }


        private void targetimg()
        {



            Mat img = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\testimage.png"); // 원본
            Mat targetimg = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\pcb_test_2.png"); // 파손이미지 
            //Mat targetimg = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\testimage.png");

            Mat res = new Mat();
            Mat resized = new Mat();
            //Cv2.Resize(res, resized, img.Size().Width - res.Size().Width + 1,  img.Size().Height - res.Size().Height +1 );
               
            Cv2.MatchTemplate(img, targetimg, res, TemplateMatchModes.CCoeffNormed);

            OpenCvSharp.Point minloc, maxloc; // 찾은 이미지의 유사도 및 위치값받기 
            double minval, maxval; // 찾은 이미지의 위치를 담을 포인트형 선언 
            Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

            var threshold = 0.1;

            //if (maxval >= threshold)
            //{
                // 서치된 부분을 빨간 테두리로
                OpenCvSharp.Rect rect = new OpenCvSharp.Rect(maxloc.X, maxloc.Y, targetimg.Width, targetimg.Height);
                Cv2.Rectangle(img, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);
                 aassdd.Text =  maxval.ToString();
                 aassdd.Text = img.Size().ToString();
                // 표시
                Cv2.ImShow("template1_show", img);
                Cv2.ImShow("targetimg", targetimg);


            //}
            //else
            //{
            //    // 낫 매칭
            //    MessageBox.Show("못찾았슴돠.");
            //}





        }

        //public void drawcircle()
        //{
        //    // 이미지 로드
        //    Mat src = Cv2.ImRead(@"C:\Users\iot\Source\Repos\kimgun140\TP_12\images\testimage.png");
        //    if (src.Empty())
        //    {
        //        Console.WriteLine("Image open failed!");
        //        return;
        //    }

        //    // 그레이스케일 변환
        //    Mat gray = new Mat();
        //    Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

        //    // 블러링
        //    Mat blr = new Mat();
        //    Cv2.GaussianBlur(gray, blr, new OpenCvSharp.Size(0, 0), 1.0);

        //    // 트랙바를 사용하여 사용자 입력을 받기 위한 윈도우 생성
        //    Cv2.NamedWindow("img", WindowFlags.AutoSize);

        //    // 트랙바 생성
        //    int v = Cv2.CreateTrackbar("minRadius", "img",  0, 100, OnTrackbar);
        //    Cv2.CreateTrackbar("maxRadius", "img", 0, 150, OnTrackbar);
        //    Cv2.CreateTrackbar("threshold", "img", 0, 100, OnTrackbar);

        //    // 트랙바 초기값 설정
        //    Cv2.SetTrackbarPos("minRadius", "img", 10);
        //    Cv2.SetTrackbarPos("maxRadius", "img", 80);
        //    Cv2.SetTrackbarPos("threshold", "img", 40);

        //    // 트랙바 초기값을 사용하여 원 검출 및 화면에 표시
        //    OnTrackbar(0, IntPtr.Zero);

        //    // 키 입력 대기
        //    Cv2.WaitKey(0);
        //    Cv2.DestroyAllWindows();


        //    void OnTrackbar(int pos, IntPtr userdata)
        //    {
        //        // 트랙바 값 가져오기
        //        int rmin = Cv2.GetTrackbarPos("minRadius", "img");
        //        int rmax = Cv2.GetTrackbarPos("maxRadius", "img");
        //        int th = Cv2.GetTrackbarPos("threshold", "img");

        //        // 원 검출
        //        CircleSegment[] circles = Cv2.HoughCircles(blr, HoughModes.Gradient, 1, 50, param1: 120, param2: th, minRadius: rmin, maxRadius: rmax);

        //        Mat dst = src.Clone();
        //        if (circles.Length > 0)
        //        {
        //            foreach (var circle in circles)
        //            {
        //                Cv2.Circle(dst, (int)circle.Center.X, (int)circle.Center.Y, (int)circle.Radius, Scalar.Red, 2, LineTypes.AntiAlias);
        //            }
        //        }

        //        // 이미지 업데이트
        //        Cv2.ImShow("img", dst);
        //    }
        //}


    }

}
