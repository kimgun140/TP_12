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
        //private void asdf()
        //{
        //    VideoCapture cam = new VideoCapture(0);
        //    Mat frame = new Mat();

        //    while (Cv2.WaitKey(33) != 'q')
        //    {
        //        cam.Read(frame);
        //        Mat gray = new Mat();
        //        Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
        //        Cv2.ImShow("gray", gray);

        //        Mat gaus = new Mat();
        //        Cv2.GaussianBlur(gray, gaus, new OpenCvSharp.Size(1, 1), 9);

        //        Mat binary = new Mat();
        //        Cv2.Threshold(gaus, binary, 100, 255, ThresholdTypes.Binary);

        //        Mat element = new Mat();
        //        Mat Ero = new Mat(1, 1, MatType.CV_8UC1);
        //        Cv2.Erode(binary, Ero, element);

        //        for (int i = 0; i < 6; i++)
        //        {
        //            Cv2.Erode(Ero, Ero, element);
        //        }

        //        Cv2.ImShow("erode", Ero);

        //        Mat dil = new Mat();
        //        Cv2.Dilate(Ero, dil, element);

        //        OpenCvSharp.Point[][] contours;
        //        OpenCvSharp.HierarchyIndex[] hierarchy;

        //        Mat src = new Mat();
        //        Cv2.Threshold(dil, src, 0, 255, ThresholdTypes.BinaryInv);

        //        Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

        //        for (int i = 0; i < contours.Length; i++)
        //        {
        //            OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);

        //            // 윤곽선에 따른 색상 지정
        //            Scalar color;
        //            if (hierarchy[i].Parent < 0)
        //            {
        //                color = Scalar.Black;
        //            }
        //            else if (hierarchy[i].Child >= 0)
        //            {
        //                color = Scalar.Blue;
        //            }
        //            else if (hierarchy[i].Next >= 0)
        //            {
        //                color = Scalar.Green;
        //            }
        //            else
        //            {
        //                color = Scalar.Yellow;
        //            }

        //            Cv2.Rectangle(frame, boundingRect, color, 2);

        //            // 윤곽선 중심에 인덱스 표시
        //            OpenCvSharp.Point center = new OpenCvSharp.Point(
        //                boundingRect.X + boundingRect.Width / 2,
        //                boundingRect.Y + boundingRect.Height / 2
        //            );
        //            Cv2.PutText(frame, i.ToString(), center, HersheyFonts.HersheySimplex, 1, color, 2);

        //            // 각 윤곽선에 대해 추가 처리
        //            //ProcessContour(i, contours[i], hierarchy[i], frame);
        //        }

        //        Cv2.ImShow("frame", frame);
        //    }

        //    Cv2.DestroyAllWindows();
        //}
        private void asdf()
        {

            VideoCapture cam = new VideoCapture(0);
            Mat frame = new Mat();
            ////Mat frame = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\ttttt.jpg");
            //Mat frame = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\testimage.png");
            //Mat targetimg = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\original1.jpg");
            ///
            while (Cv2.WaitKey(33) != 'q')
            {
                cam.Read(frame);
                Mat gray = new Mat();
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.ImShow("gray", gray);
                gaus = new Mat();
                Cv2.GaussianBlur(gray, gaus, new OpenCvSharp.Size(1, 1), 9);

                binary = new Mat();
                Cv2.Threshold(gaus, binary, 100, 255, ThresholdTypes.Binary);


                Mat element = new Mat();

                Mat Ero = new Mat(1, 1, MatType.CV_8UC1);
                Mat dil = new Mat();

                Cv2.Erode(binary, Ero, element);

                // erode는 신이야
                Cv2.Erode(Ero, Ero, element);
                Cv2.Dilate(Ero, dil, element);
                Cv2.Erode(dil, Ero, element);
                Cv2.Dilate(Ero, dil, element);
                Cv2.Erode(dil, Ero, element);
                Cv2.Dilate(Ero, dil, element);
                Cv2.Erode(dil, Ero, element);
                Cv2.Dilate(Ero, dil, element);
                Cv2.Erode(dil, Ero, element);
                //    Cv2.Erode(Ero, Ero, element);
                //Cv2.Erode(Ero, Ero, element);
                //Cv2.Erode(Ero, Ero, element);
                //Cv2.Erode(Ero, Ero, element);
                Cv2.Dilate(Ero, dil, element);
                Cv2.Dilate(dil, dil, element);



                Cv2.ImShow("erode", dil);


                //Cv2.Dilate(Ero, dil, element);


                //Cv2.ImShow("dil", dil);

                OpenCvSharp.Point[][] contours;
                OpenCvSharp.HierarchyIndex[] hierarchy;


                Mat src = new Mat();
                Cv2.Threshold(dil, src, 0, 255, ThresholdTypes.BinaryInv);


                //Cv2.ImShow("inv", src);
                //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);

                //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89L1);// 
                //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree ,ContourApproximationModes.ApproxTC89KCOS);// 
                //Cv2.ImShow("contour", dil);
                int cnt = 0;
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
                    for (int i = 1; i < contours.Length; i++)
                    {                                             //items;

                        ////for(int j = 1; j < 6; j++)
                        ////{
                        //    OpenCvSharp.Rect R = Cv2.BoundingRect(contours[i]);
                        //    Cv2.Rectangle(frame, R, Scalar.Red, 3);
                        ////}
                        if (hierarchy[i].Parent < 0)
                        {
                         
                            //if(length > 1000)
                            //MessageBox.Show(length.ToString());





                            //ProcessContour(mat);
                        }
                        //else if (hierarchy[i].Child >= 0)
                        //{
                        //    // 자식이 있는 윤곽선 (중첩된 윤곽선 처리 예시)
                        //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                        //    Cv2.Rectangle(frame, boundingRect, Scalar.Blue, 5);
                        //    Mat child = new Mat(frame, boundingRect);
                        //    Cv2.ImShow("child1", child);
                        //}
                        else if (hierarchy[i].Next >= 0)
                        {
                            // 다음 형제 윤곽선 처리 예시
                            cnt++;
                            OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                            Cv2.Rectangle(frame, boundingRect, Scalar.Green, 1);

                            Cv2.Rectangle(src, boundingRect, Scalar.Black, 5);
                            Cv2.Rectangle(frame, boundingRect, Scalar.Black, 5);
                            RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                            Mat result = new Mat();
                            //for (int j = 0; j < 4; j++)
                            //{
                            //      Cv2.Line(src, new OpenCvSharp.Point(rect.Points()[j].X, rect.Points()[j].Y),
                            //    new OpenCvSharp.Point(rect.Points()[(j + 1) % 4].X, rect.Points()[(j + 1) % 4].Y),
                            //    Scalar.Red, 2, LineTypes.AntiAlias);
                            // }
                             Cv2.ImShow("img" + i, src);


                        }
                        else if (hierarchy[i].Next == -1 && hierarchy[i].Child == -1)
                        {
                            cnt++;
                            OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                            Cv2.Rectangle(frame, boundingRect, Scalar.Green, 5);
                            Cv2.Rectangle(src, boundingRect, Scalar.Black, 5);
                            Mat mat = new Mat(src, boundingRect);
                            Cv2.ImShow("img" + i, mat);
                        }
                    }
                
                    

                }
                Cv2.ImShow("frame", frame);
                //Cv2.WaitKey(0);
            } // while


            Cv2.DestroyAllWindows();


        }

        private void ProcessContour( Mat frame)
        {

           // 계층별로 검출한걸 이함수에 돌려서 비교 한다.  계층별 컨투어를 비교
            Mat targetimg = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\original1.jpg");
            Mat res = new Mat();
            Mat resized = new Mat();

            Cv2.MatchTemplate(frame, targetimg, res, TemplateMatchModes.CCoeffNormed);

            OpenCvSharp.Point minloc, maxloc;
            double minval, maxval;
            Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
            //Cv2.Resize(res, resized, frame.Size().Width - res.Size().Width + 1, img.Size().Height - res.Size().Height + 1);
            var threshold = 0.7;
            if(  maxval >= threshold)
            {
                MessageBox.Show(maxval.ToString());

            }

            Cv2.ImShow("img", frame);
            Cv2.ImShow("targetimg", targetimg);

        }


        private void targetimg() // 비교 테스트 
        {


            Mat img = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\testset1.jpg");
            //Mat img = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\testimage.png");
            Mat targetimg = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\original1.jpg");



            Mat res = new Mat();
            Cv2.MatchTemplate(img, targetimg, res, TemplateMatchModes.CCoeffNormed);

            OpenCvSharp.Point minloc, maxloc;
            double minval, maxval;
            Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

            var threshold = 0.7;
            MessageBox.Show(maxval.ToString());

            Cv2.ImShow("img", img);
            Cv2.ImShow("targetimg", targetimg);


            //if (maxval >= threshold)
            //{
            //    // 서치된 부분을 빨간 테두리로
            //    OpenCvSharp.Rect rect = new OpenCvSharp.Rect(maxloc.X, maxloc.Y, targetimg.Width, targetimg.Height);
            //    Cv2.Rectangle(img, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);

            //    // 표시
            //    Cv2.ImShow("template1_show", img);

            //}
            //else
            //{
            //    // 낫 매칭
            //    MessageBox.Show("못찾았슴돠.");
            //}





        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //camtest();
            asdf();
            //targetimg();
        }
    }
}
