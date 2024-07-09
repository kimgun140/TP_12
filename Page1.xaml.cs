using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page1 : System.Windows.Controls.Page
    {
        Mat gaus;
        Mat binary;
        int threshBinary = 0;
        OpenCvSharp.Window window;
        int reccount;
        public Page1()
        {
            InitializeComponent();
            //asdf();

        }
        public void camtest()
        {
            VideoCapture cam = new VideoCapture(0);
            Mat frame = new Mat();
            while (Cv2.WaitKey(33) != 'q')
            {
                cam.Read(frame);
                Cv2.ImShow("frame", frame);
                //Cv2.WaitKey(0);

            }
            Cv2.DestroyAllWindows();

        }

        private void asdf()
        {

            //VideoCapture cam = new VideoCapture(0);
            //Mat frame = new Mat();
            Mat frame = Cv2.ImRead(@"C:\Users\iot\Source\Repos\kimgun140\TP_12\images\ttttt.jpg");
            //while (Cv2.WaitKey(33) != 'q')
            //{
            //cam.Read(frame);
            Mat gray = new Mat();
            Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

            gaus = new Mat();
            Cv2.GaussianBlur(gray, gaus, new OpenCvSharp.Size(1, 1), 9);

            binary = new Mat();
            Cv2.Threshold(gaus, binary, 100, 255, ThresholdTypes.Binary);


            Mat element = new Mat();

            Mat Ero = new Mat(1, 1, MatType.CV_8UC1);
            Cv2.Erode(binary, Ero, element);

            // erode는 신이야
            Cv2.Erode(Ero, Ero, element);
            Cv2.Erode(Ero, Ero, element);
            Cv2.Erode(Ero, Ero, element);
            Cv2.Erode(Ero, Ero, element);
            Cv2.Erode(Ero, Ero, element);
            Cv2.Erode(Ero, Ero, element);

            Cv2.ImShow("erode", Ero);


            Mat dil = new Mat();
            Cv2.Dilate(Ero, dil, element);


            //Cv2.ImShow("dil", dil);

            OpenCvSharp.Point[][] contours;
            OpenCvSharp.HierarchyIndex[] hierarchy;


            Mat src = new Mat();
            Cv2.Threshold(dil, src, 0, 255, ThresholdTypes.BinaryInv);


            //Cv2.ImShow("inv", src);
            Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);// 모드를 라인으로 하고
                                                                                                                            //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89L1);// 모드를 라인으로 하고
                                                                                                                            //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree ,ContourApproximationModes.ApproxTC89KCOS);// 모드를 라인으로 하고
                                                                                                                            //Cv2.ImShow("contour", dil);

            foreach (OpenCvSharp.Point[] items in contours)
            //foreach (var items in hierarchy)
            {

                //for (int i = 0; i < contours.Length; i++)
                //{
                //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                //    Scalar color = hierarchy[i].Parent < 0 ? Scalar.Red : Scalar.Green; // 최상위 윤곽선은 빨간색, 자식 윤곽선은 초록색
                //    Cv2.Rectangle(frame, boundingRect, color, 2);
                //}


                //여기서 라인으로한걸 찾아서 사각형, 원인것들이랑 아닌것들이 있을테니까 그걸 찾기
                // 영역안에서 찾기? 이러면 비교 
                double length = Cv2.ArcLength(items, true); // 찾은 윤곽선 길이 
                double area = Cv2.ContourArea(items, true); // 넓이
                for (int i = 0; i < contours.Length; i++)
                {                                             //items;
                    if (hierarchy[i].Parent < 0)
                    {
                        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                        Cv2.Rectangle(frame, boundingRect, Scalar.Black, 5);



                    }
                    else if (hierarchy[i].Child >= 0)
                    {
                        // 자식이 있는 윤곽선 (중첩된 윤곽선 처리 예시)
                        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                        Cv2.Rectangle(frame, boundingRect, Scalar.Blue, 5);


                    }
                    else if (hierarchy[i].Next >= 0)
                    {
                        // 다음 형제 윤곽선 처리 예시
                        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                        OpenCvSharp.Rect boundingRect1 = Cv2.BoundingRect(contours[1]);
                        OpenCvSharp.Rect boundingRect2 = Cv2.BoundingRect(contours[2]);
                        OpenCvSharp.Rect boundingRect3 = Cv2.BoundingRect(contours[3]);
                        OpenCvSharp.Rect boundingRect4 = Cv2.BoundingRect(contours[4]);




                        Cv2.Rectangle(frame, boundingRect, Scalar.Green, 5);
                        Cv2.Rectangle(frame, boundingRect, Scalar.Green, 5);

                        Mat mat1 = new Mat(frame, boundingRect1);
                        Mat mat2 =  new Mat(frame, boundingRect2);
                        Mat mat3 = new Mat(frame, boundingRect3);
                        Mat mat4 = new Mat(frame, boundingRect3);



                        Cv2.ImShow("aa1", mat1);
                        Cv2.ImShow("aa2", mat2);
                        Cv2.ImShow("aa3", mat3);
                        Cv2.ImShow("aa4", mat4);



                    }
                    else if (hierarchy[i].Previous >= 0)
                    {
                        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                        Cv2.Rectangle(frame, boundingRect, Scalar.Yellow, 5);
                    }
                }



            }
            Cv2.ImShow("frame", frame);
            Cv2.WaitKey(0);


         
            Cv2.DestroyAllWindows();


        }

        private void targetimg()
        {



            Mat img = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\testimage.png");
            Mat targetimg = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\targetimg.png");


            Mat res = new Mat();
            Cv2.MatchTemplate(img, targetimg, res, TemplateMatchModes.CCoeffNormed);

            OpenCvSharp.Point minloc, maxloc;
            double minval, maxval;
            Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

            var threshold = 0.7;

            if (maxval >= threshold)
            {
                // 서치된 부분을 빨간 테두리로
                OpenCvSharp.Rect rect = new OpenCvSharp.Rect(maxloc.X, maxloc.Y, targetimg.Width, targetimg.Height);
                Cv2.Rectangle(img, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);

                // 표시
                Cv2.ImShow("template1_show", img);

            }
            else
            {
                // 낫 매칭
                MessageBox.Show("못찾았슴돠.");
            }





        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //camtest();
            asdf();
        }
    }
}
