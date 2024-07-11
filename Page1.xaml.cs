﻿using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        List<Double> contourlist = new List<Double>();
        double kk = 0;
        List<OpenCvSharp.Rect> opcv = new List<OpenCvSharp.Rect>();
   

        public Page1()
        {
            InitializeComponent();
            //string   save = DateTime.Now.ToString("yyyy-MM-dd-hh시mm분ss초"); // t시간 이름 대입
            Mat frame = new Mat();
        }


        private void asdf()
        {

            VideoCapture cam = new VideoCapture(0);

            //Mat frame = new Mat();
            //Mat frame = Cv2.ImRead(@"C:\Users\iot\Source\Repos\kimgun140\TP_12\images\testimage.png"); // testimage.png 정상
            //Mat frame = Cv2.ImRead(@"C:\Users\LMS\source\repos\TP_12\images\pcb_test_test.png"); // 오류
            //Mat targetimg = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\original1.jpg");
            ///
            ImgFuncs imgFuncs = new ImgFuncs();
            



            while (Cv2.WaitKey(33) != 'q')
            {

                Mat frame = imgFuncs.MakeFrame(cam);

                Mat src = imgFuncs.PreProcessing();
               if( imgFuncs.MakeContours(src) == true)
                {
                    aaaaa.Text += "정상";
                }
                else
                {
                    aaaaa.Text += "에러";
                }

                ////    cam.Read(frame);
                ////    Mat gray = new Mat();
                ////Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                //////Cv2.ImShow("gray", gray);
                ////gaus = new Mat();
                ////Cv2.GaussianBlur(gray, gaus, new OpenCvSharp.Size(1, 1), 9);

                ////binary = new Mat();
                ////Cv2.Threshold(gaus, binary, 100, 255, ThresholdTypes.Binary);
                //////Cv2.Threshold(binary, binary, 254, 255, ThresholdTypes.Binary);

                //////Cv2.Threshold(gray, binary, 254, 255, ThresholdTypes.Binary);



                ////Mat element = new Mat();

                ////Mat Ero = new Mat(1, 1, MatType.CV_8UC1);
                ////Mat dil = new Mat();

                ////    Cv2.Erode(binary, Ero, element);
                ////    //Cv2.Erode(Ero, Ero, element);
                ////    //Cv2.Erode(Ero, Ero, element);
                ////    //Cv2.Erode(Ero, Ero, element);
                ////    //Cv2.Erode(Ero, Ero, element);
                ////    //Cv2.Erode(Ero, Ero, element);
                ////    //Cv2.Erode(Ero, Ero, element);
                ////    //Cv2.Erode(Ero, Ero, element);
                ////    //Cv2.Dilate(Ero, dil, element);


                ////    //비디오
                ////    Cv2.Erode(Ero, Ero, element);
                ////    Cv2.Dilate(Ero, dil, element);
                ////    Cv2.Erode(dil, Ero, element);
                ////    Cv2.Dilate(Ero, dil, element);
                ////    Cv2.Erode(dil, Ero, element);
                ////    Cv2.Dilate(Ero, dil, element);
                ////    Cv2.Erode(dil, Ero, element);
                ////    Cv2.Dilate(Ero, dil, element);
                ////    Cv2.Erode(dil, Ero, element);
                ////    Cv2.Dilate(Ero, dil, element);
                ////    Cv2.Dilate(dil, dil, element);



                ////    Cv2.ImShow("erode", dil);


                //////Cv2.Dilate(Ero, dil, element);


                //////Cv2.ImShow("dil", dil);

                ////OpenCvSharp.Point[][] contours;
                ////OpenCvSharp.HierarchyIndex[] hierarchy;


                ////Mat src = new Mat();
                ////Cv2.Threshold(dil, src, 0, 255, ThresholdTypes.BinaryInv);
                //////Cv2.ImShow("src", src);
                //////전처리 끝

                //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89KCOS);
                ////외곽선 검출
                //int cnt = 0;
                //    //OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[cnt]);
                //    //OpenCvSharp.Rect boundingRect1 = Cv2.BoundingRect(contours[1]);
                //    //OpenCvSharp.Rect boundingRect2 = Cv2.BoundingRect(contours[2]);
                //    //OpenCvSharp.Rect boundingRect3 = Cv2.BoundingRect(contours[3]);
                //    //컨투어 opencvcshrarp.rect 리스트에 담기
                //    //// 담긴 담았는데 구별을 어떻게하지?
                //    //for (int i = 0; i < contours.Length; i++)
                //    //{

                //    //        OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                //    //        opcv.Add(boundingRect);

                //    //}
                //    //


                //    //원구하기
                //    //Point2f center = new Point2f();
                //    //float radius;
                //    //Cv2.MinEnclosingCircle(contours[1], out center, out radius);// 원 찾으려면  넓이별로 저장해서 크기 비교 작은 2개가 원 //
                //    //Cv2.Circle(frame, (int)center.X, (int)center.Y, (int)radius, Scalar.Yellow, 2);
                //    //double area = Cv2.ContourArea(contours[1], true); // 넓이
                //    //double tt = (Math.Sqrt(area));
                //    //double ra = (double)1 / 2 * tt;
                //    //Cv2.Circle(frame, (int)center.X, (int)center.Y, (int)ra, Scalar.Red, 2);

                //    //Cv2.ImShow("yellow", frame);
                //    ///

                //    //Mat mat1 = new Mat(frame, boundingRect1);
                //    //Mat mat2 = new Mat(frame, boundingRect2);
                //    //Mat mat3 = new Mat(src, boundingRect3);

                //    // 오류 검출 


                //    //ProcessContour(mat3);
                //    //int whitePixelCount = Cv2.CountNonZero(mat3);// 흑백사진

                //    //if (whitePixelCount >= 1000)
                //    //{
                //    //    MessageBox.Show("오류");
                //    //}else
                //    //{
                //    //    MessageBox.Show("정상");
                //    //}
                //    //aaaaa.Text = whitePixelCount.ToString();

                //    //Cv2.ImShow("aaasss" , mat1);
                //    //Cv2.ImShow("aaasss1", mat2);
                //    //Cv2.ImShow("aaasss2", mat3);


                //    // 테두리 파형 그리기 
                //    for (cnt = 0; cnt < contours.Length; cnt++)
                //{
                //    //  넓이 넣기   작은거 2개가 원
                //    kk = Cv2.ContourArea(contours[cnt]);

                //    contourlist.Add(kk);



                //    for (int j = 0; j < contours[cnt].Length; j++)
                //    {
                //        if (j == 3)
                //            break;
                //        else
                //        {

                //            //Cv2.Line(frame, contours[cnt][j], contours[cnt][j + 1], Scalar.Red, 2);
                //        }
                //    }
                //    if (contours[cnt].Length == 4)
                //    {
                //        //RotatedRect rotatedRect = Cv2.MinAreaRect(contours[cnt]);
                //        //Cv2.DrawContours(frame, contours, cnt, Scalar.Black, 5);

                //    }
                //    if (contours[cnt].Length == 0)
                //    {
                //        //Cv2.DrawContours(frame, contours, cnt, Scalar.Red, 5);

                //    }
                //    else
                //    {
                //        //Cv2.DrawContours(frame, contours, cnt, Scalar.Green, 5);



                //        //OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[1]);
                //        OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[cnt]);

                //        Cv2.Rectangle(frame, boundingRect12, Scalar.Yellow, 2);
                //        Mat mat = new Mat(frame, boundingRect12);

                //        //Cv2.ImShow("asd" + cnt, mat);

                //    }

                //}
                //Cv2.ImShow("qwe", frame);
                //// 정렬 된건지 확인 
                //contourlist.Sort();
                //for (int i = 0; i < contourlist.Count; i++)
                //{
                //    aaaaa.Text += "\n" + i + " " + contourlist[i].ToString();

                //}

                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[0].ToString();
                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[1].ToString();
                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[2].ToString();
                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[3].ToString();
                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[4].ToString();
                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[5].ToString();
                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[6].ToString();
                //    //aaaaa.Text += "\n" + cnt + " " + contourlist[7].ToString();
                //    ////aaaaa.Text += "\n" + cnt + " " + contourlist[8].ToString();



                //    //aaaaa.Text = "\n"+ list[cnt-1].ToString();
                //    //Cv2.ImShow("wer", frame);
                //    //Vector<OpenCvSharp.Point> poly;
                //    //Mat poly_img, rectangle_img;


                //    //foreach (OpenCvSharp.Point[] items in contours)
                //    ////foreach (var items in hierarchy)
                //    //{

                //    //    //for (int i = 0; i < contours.Length; i++)
                //    //    //{
                //    //    //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                //    //    //    Scalar color = hierarchy[i].Parent < 0 ? Scalar.Red : Scalar.Green; // 최상위 윤곽선은 빨간색, 자식 윤곽선은 초록색
                //    //    //    Cv2.Rectangle(frame, boundingRect, color, 2);
                //    //    //}


                //    //    //여기서 라인으로한걸 찾아서 사각형, 원인것들이랑 아닌것들이 있을테니까 그걸 찾기
                //    //    // 영역안에서 찾기? 이러면 비교 
                //    //    double length = Cv2.ArcLength(items, true); // 찾은 윤곽선 길이 
                //    //    double area = Cv2.ContourArea(items, true); // 넓이
                //    //    for (int i = 1; i < contours.Length; i++)
                //    //    {                                             //items;

                //    //        ////for(int j = 1; j < 6; j++)
                //    //        ////{
                //    //        //    OpenCvSharp.Rect R = Cv2.BoundingRect(contours[i]);
                //    //        //    Cv2.Rectangle(frame, R, Scalar.Red, 3);
                //    //        ////}
                //    //        if (hierarchy[i].Parent < 0)
                //    //        {

                //    //            //if(length > 1000)
                //    //            //MessageBox.Show(length.ToString());



                //    //            //ProcessContour(mat);
                //    //        }
                //    //        //else if (hierarchy[i].Child >= 0)
                //    //        //{
                //    //        //    // 자식이 있는 윤곽선 (중첩된 윤곽선 처리 예시)
                //    //        //    OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                //    //        //    Cv2.Rectangle(frame, boundingRect, Scalar.Blue, 5);
                //    //        //    Mat child = new Mat(frame, boundingRect);
                //    //        //    Cv2.ImShow("child1", child);
                //    //        //}
                //    //        else if (hierarchy[i].Next >= 0)
                //    //        {
                //    //            // 다음 형제 윤곽선 처리 예시
                //    //            cnt++;
                //    //            OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                //    //            Cv2.Rectangle(frame, boundingRect, Scalar.Green, 1);

                //    //            Cv2.Rectangle(src, boundingRect, Scalar.Black, 5);
                //    //            Cv2.Rectangle(frame, boundingRect, Scalar.Black, 5);
                //    //            RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                //    //            Mat result = new Mat();
                //    //            Cv2.DrawContours(src, contours, i, Scalar.Red, 3);
                //    //            //for (int j = 0; j < 4; j++)
                //    //            //{
                //    //            //      Cv2.Line(src, new OpenCvSharp.Point(rect.Points()[j].X, rect.Points()[j].Y),
                //    //            //    new OpenCvSharp.Point(rect.Points()[(j + 1) % 4].X, rect.Points()[(j + 1) % 4].Y),
                //    //            //    Scalar.Red, 2, LineTypes.AntiAlias);
                //    //            // }
                //    //            Cv2.ImShow("img" + i, src);


                //    //        }
                //    //        else if (hierarchy[i].Next == -1 && hierarchy[i].Child == -1)
                //    //        {
                //    //            cnt++;
                //    //            OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contours[i]);
                //    //            Cv2.Rectangle(frame, boundingRect, Scalar.Green, 5);
                //    //            Cv2.Rectangle(src, boundingRect, Scalar.Black, 5);
                //    //            Mat mat = new Mat(src, boundingRect);
                //    //            Cv2.ImShow("img" + i, mat);
                //    //        }
                //    //    }


                Cv2.ImShow("frame", imgFuncs.frame);
                this.qwer.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(imgFuncs.frame);

                //    //}
            } // while

            Cv2.WaitKey(0);


            Cv2.DestroyAllWindows();


        }

        private void ProcessContour(Mat frame)
        //파형검사 
        {

            int whitePixelCount = Cv2.CountNonZero(frame);// 흑백사진
            if (whitePixelCount >= 1000)
            {
                MessageBox.Show("오류" + whitePixelCount);
            }
            else
            {
                MessageBox.Show("정상");
            }
            aaaaa.Text += whitePixelCount.ToString();


            //// 계층별로 검출한걸 이함수에 돌려서 비교 한다.  계층별 컨투어를 비교
            //Mat targetimg = Cv2.ImRead(@"C:\Users\lms\Desktop\TP_12\images\original1.jpg");
            //Mat res = new Mat();
            //Mat resized = new Mat();

            //Cv2.MatchTemplate(frame, targetimg, res, TemplateMatchModes.CCoeffNormed);

            //OpenCvSharp.Point minloc, maxloc;
            //double minval, maxval;
            //Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
            ////Cv2.Resize(res, resized, frame.Size().Width - res.Size().Width + 1, img.Size().Height - res.Size().Height + 1);
            //var threshold = 0.7;
            //if (maxval >= threshold)
            //{

            //}

            //Cv2.ImShow("img", frame);
            //Cv2.ImShow("targetimg", targetimg);

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


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //camtest();
            asdf();
            //targetimg();
        }
    }
}
