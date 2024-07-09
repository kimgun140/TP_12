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
            VideoCapture cam = new VideoCapture(0);
            Mat frame = new Mat();

            while (Cv2.WaitKey(33) != 'q')
            {
                cam.Read(frame);
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
                            Cv2.Rectangle(frame, boundingRect, Scalar.Green, 5);
                        }else if (hierarchy[i].Previous >= 0)
                        {

                        }
                    }

                    //    if (length < 50 || area < 1000 || items.Length < 5)
                    //{

                    //    //    OpenCvSharp.Point center = new OpenCvSharp.Point((items[0].X + items[3].X) / 2, (items[1].Y + items[2].Y) / 2 + 10);
                    //    //    Cv2.Circle(frame, center, 10, Scalar.Green, 5);

                    //}
                    //////else
                    //////{
                    ////OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
                    ////Cv2.Rectangle(frame, boundingRect, Scalar.Red, 5);
                    //////}
                    //else if (length > 400 && length < 430) // 작은 사이즈 사각형 
                    //{
                    //    reccount += 1;
                    //    aaaaa.Text = reccount.ToString();

                    //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
                    //    Cv2.Rectangle(frame, boundingRect, Scalar.Blue, 5); // 
                    //    Mat img1 = new Mat(frame, boundingRect);
                    //    //Cv2.ImShow("img1", img1); // 이거랑 비교 


                    //}
                    //else if (length > 450 && length < 520) // 작은 사이즈 사각형 
                    //{
                    //    reccount += 1;
                    //    aaaaa.Text = reccount.ToString();

                    //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
                    //    Cv2.Rectangle(frame, boundingRect, Scalar.Green, 5); // 이거 찾았으니까 여기서 이 영역에서 다시 검색 ㄱ 

                    //    Mat img1 = new Mat(frame, boundingRect);
                    //    //Cv2.ImShow("img1", img1); // 이거랑 비교 

                    //}
                    //else if (length > 500 && length < 1500) // 가운데꺼 
                    //{
                    //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
                    //    Cv2.Rectangle(frame, boundingRect, Scalar.Red, 5);
                    //    Mat img2 = new Mat(frame, boundingRect);
                    //    //Cv2.ImShow("img2", img2);// 이거랑 비교 

                    //}
                    ////else if (length > 1000 && length < 1500) // 손상된 이미지 가운데 큰거 
                    ////{
                    ////    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
                    ////    Cv2.Rectangle(img, boundingRect, Scalar.Red, 5);
                    ////    Mat img2 = new Mat(img, boundingRect);
                    ////    Cv2.ImShow("img2", img2);// 이거랑 비교 

                    ////}

                }
                Cv2.ImShow("frame", frame);
                //Cv2.WaitKey(0);


            }
            //Cv2.ImWrite(@"C:\Users\lms\Desktop\TP_12 - 복사본\images\ttttt.jpg", frame);

            Cv2.DestroyAllWindows();


            //Mat targetimg = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\targetimg.png");
            //Mat img = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\pcb_test_2.png");
            //Mat img = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\pcb_test_test.png");
            //Mat img1 = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\testimage.png");



            //Cv2.ImShow("img", img);
            //Task k = Task.Run(async () =>
            //{
            //Mat gray = new Mat();
            //Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

            //Cv2.ImShow("gray", gray);

            //gaus = new Mat();
            //Cv2.GaussianBlur(gray, gaus, new OpenCvSharp.Size(1, 1), 9);

            //Cv2.ImShow("gaus", gaus);

            //binary = new Mat();
            //Cv2.Threshold(gaus, binary, 100, 255, ThresholdTypes.Binary);

            ////Cv2.ImShow("binary", binary);
            ////window = new OpenCvSharp.Window("binary", binary, WindowFlags.AutoSize);
            ////Cv2.CreateTrackbar("Thresh", "binary", ref threshBinary, 255, TrackBarEvent);
            ////Cv2.SetMouseCallback("binary", onMouse2);
            //Mat element = new Mat();

            //Mat Ero = new Mat(1, 1, MatType.CV_8UC1);
            //Cv2.Erode(binary, Ero, element);

            //// erode는 신이야
            //Cv2.Erode(Ero, Ero, element);
            //Cv2.Erode(Ero, Ero, element);
            //Cv2.Erode(Ero, Ero, element);
            //Cv2.Erode(Ero, Ero, element);
            //Cv2.Erode(Ero, Ero, element);
            //Cv2.Erode(Ero, Ero, element);

            //Cv2.ImShow("erode", Ero);

            //Mat dil = new Mat();
            //Cv2.Dilate(Ero, dil, element);


            //Cv2.ImShow("dil", dil);

            //OpenCvSharp.Point[][] contours;
            //OpenCvSharp.HierarchyIndex[] hierarchy;


            //Mat src = new Mat();
            //Cv2.Threshold(dil, src, 0, 255, ThresholdTypes.BinaryInv);


            //Cv2.ImShow("inv", src);

            //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);// 모드를 라인으로 하고
            ////Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89L1);// 모드를 라인으로 하고
            ////Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree ,ContourApproximationModes.ApproxTC89KCOS);// 모드를 라인으로 하고



            //Cv2.ImShow("contour", dil);


            //Mat res = new Mat();
            //Cv2.MatchTemplate(img, targetimg, res, TemplateMatchModes.CCoeffNormed);


            // 원본, 찾을 이미지 
            //foreach (OpenCvSharp.Point[] items in contours)
            //{
            //    // 여기서 라인으로한걸 찾아서  사각형, 원인것들이랑 아닌것들이 있을테니까 그걸 찾기 
            //    //// 영역안에서 찾기? 이러면 비교 
            //    double length = Cv2.ArcLength(items, true); // 찾은 윤곽선 길이 
            //    double area = Cv2.ContourArea(items, true); // 넓이

            //    if (length < 50 || area < 1000 || items.Length < 5)
            //    {

            //        OpenCvSharp.Point center = new OpenCvSharp.Point((items[0].X + items[3].X) / 2, (items[1].Y + items[2].Y) / 2 + 10);
            //        Cv2.Circle(img, center, 10, Scalar.Green, 5);

            //    }
            //    //else
            //    //{
            //    //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
            //    //    Cv2.Rectangle(img, boundingRect, Scalar.Red, 5);
            //    //}
            //    else if (length > 400 && length < 430) // 작은 사이즈 사각형 
            //    {
            //        reccount += 1;
            //        aaaaa.Text = reccount.ToString();

            //        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
            //        Cv2.Rectangle(img, boundingRect, Scalar.Blue, 5); // 
            //        Mat img1 = new Mat(img, boundingRect);
            //        Cv2.ImShow("img1", img1); // 이거랑 비교 


            //    }
            //    else if (length > 450 && length < 520) // 작은 사이즈 사각형 
            //    {
            //        reccount += 1;
            //        aaaaa.Text = reccount.ToString();

            //        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
            //        Cv2.Rectangle(img, boundingRect, Scalar.Green, 5); // 이거 찾았으니까 여기서 이 영역에서 다시 검색 ㄱ 

            //        Mat img1 = new Mat(img, boundingRect);
            //        Cv2.ImShow("img1", img1); // 이거랑 비교 


            //    }
            //    else if (length > 500 && length < 1500) // 가운데꺼 
            //    {
            //        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
            //        Cv2.Rectangle(img, boundingRect, Scalar.Red, 5);
            //        Mat img2 = new Mat(img, boundingRect);
            //        Cv2.ImShow("img2", img2);// 이거랑 비교 

            //    }
            //    //else if (length > 1000 && length < 1500) // 손상된 이미지 가운데 큰거 
            //    //{
            //    //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(items); // 사각형 찾은거
            //    //    Cv2.Rectangle(img, boundingRect, Scalar.Red, 5);
            //    //    Mat img2 = new Mat(img, boundingRect);
            //    //    Cv2.ImShow("img2", img2);// 이거랑 비교 

            //    //}

            //}

            //Cv2.ImShow("img", img);
            //Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();
            //});
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
