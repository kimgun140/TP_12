using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TP_12
{
    internal class ImgFuncs
    {
        List<Double> contourlist = new List<Double>();
        double kk = 0;
        List<OpenCvSharp.Rect> opcv = new List<OpenCvSharp.Rect>();
        public Mat frame { get; set; }

        public ImgFuncs()
        {
            frame = new Mat();
        }


        public OpenCvSharp.Mat MakeFrame(OpenCvSharp.VideoCapture videoCapture)
        {
            videoCapture.Read(frame);


            return frame;
        }


        public OpenCvSharp.Mat PreProcessing ()
        {

            Mat gray = new Mat();
            Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
            //Cv2.ImShow("gray", gray);
            Mat gaus = new Mat();

            Cv2.GaussianBlur(gray, gaus, new OpenCvSharp.Size(1, 1), 9);

            Mat binary = new Mat();

            Cv2.Threshold(gaus, binary, 100, 255, ThresholdTypes.Binary);
            //Cv2.Threshold(binary, binary, 254, 255, ThresholdTypes.Binary);

            //Cv2.Threshold(gray, binary, 254, 255, ThresholdTypes.Binary);



            Mat element = new Mat();

            Mat Ero = new Mat(1, 1, MatType.CV_8UC1);
            Mat dil = new Mat();

            Cv2.Erode(binary, Ero, element);
            Cv2.Erode(Ero, Ero, element);
            Cv2.Dilate(Ero, dil, element);
            Cv2.Erode(dil, Ero, element);
            Cv2.Dilate(Ero, dil, element);
            Cv2.Erode(dil, Ero, element);
            Cv2.Dilate(Ero, dil, element);
            Cv2.Erode(dil, Ero, element);
            Cv2.Dilate(Ero, dil, element);
            Cv2.Erode(dil, Ero, element);
            Cv2.Dilate(Ero, dil, element);
            Cv2.Erode(dil, Ero, element);
            Cv2.Dilate(Ero, dil, element);
            Cv2.Dilate(dil, dil, element);


            Mat src = new Mat();
            Cv2.Threshold(dil, src, 0, 255, ThresholdTypes.BinaryInv);
            Cv2.ImShow("src", src);

            return src;


      
        }

        public  bool MakeContours(OpenCvSharp.Mat src)
        {
            Mat mat;
            OpenCvSharp.Point[][] contours;
            OpenCvSharp.HierarchyIndex[] hierarchy;
            //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89KCOS);
            Mat asdf = new OpenCvSharp.Mat(3, 3, MatType.CV_8UC1);
            Cv2.Canny(src, src, 50, 255);
            Cv2.ImShow("canny", src);
            Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            int cnt = 0;

            //if (contours.Length != 7)
            //{
            //    return false;
            //}

            for (cnt = 0; cnt < contours.Length; cnt++)
            {


                //OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[1]);
                //OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[cnt]);
                double areaSize = Cv2.ContourArea(contours[cnt]);
                if (areaSize > 900)
                {
                    OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[cnt]);
                    Cv2.DrawContours(frame, contours, cnt, Scalar.AliceBlue, 2);

                    Cv2.Rectangle(frame, boundingRect12, Scalar.Yellow, 2);
                    mat = new Mat(src, boundingRect12);
                    Cv2.ImShow("judgframe", frame);
                    if (JudgeErr(mat) == false)
                    {
                        return false;
                    }

                }

            }

            //for (int j = 0; j < contours[cnt].Length; j++)
            //{
            //    if (j == 3)
            //        break;
            //    else
            //    {

            //        //Cv2.Line(frame, contours[cnt][j], contours[cnt][j + 1], Scalar.Red, 2);
            //    }
            //}
            //if (contours[cnt].Length == 4)
            //{
            //    //RotatedRect rotatedRect = Cv2.MinAreaRect(contours[cnt]);
            //    //Cv2.DrawContours(frame, contours, cnt, Scalar.Black, 5);

            //}
            //if (contours[cnt].Length == 0)
            //{
            //    //Cv2.DrawContours(frame, contours, cnt, Scalar.Red, 5);

            //}
            //else
            //{
            //    //Cv2.DrawContours(frame, contours, cnt, Scalar.Green, 5);



            //    //OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[1]);
            //    OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[cnt]);

            //    Cv2.Rectangle(frame, boundingRect12, Scalar.Yellow, 2);
            //    Mat mat = new Mat(frame, boundingRect12);

            //    //Cv2.ImShow("asd" + cnt, mat);

            //}



            return true;
        }
        public void OnlyMakeContours(OpenCvSharp.Mat src)
        {
            Mat mat;
            OpenCvSharp.Point[][] contours;
            OpenCvSharp.HierarchyIndex[] hierarchy;
            //Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89KCOS);
            Mat asdf = new OpenCvSharp.Mat(3, 3, MatType.CV_8UC1);
            Cv2.Canny(src, src, 50, 255);
            Cv2.ImShow("canny", src);
            Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            int cnt = 0;

            //if (contours.Length != 7)
            //{
            //    return false;
            //}

            for (cnt = 0; cnt < contours.Length; cnt++)
            {


                //OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[1]);
                //OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[cnt]);
                OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[cnt]);
                Cv2.DrawContours(frame, contours, cnt, Scalar.AliceBlue, 2);

                Cv2.Rectangle(frame, boundingRect12, Scalar.Yellow, 2);
                mat = new Mat(src, boundingRect12);
                Cv2.ImShow("frame", frame);
                //if (JudgeErr(mat) == false)
                //{
                //    return false;
                //}



            }

            //for (int j = 0; j < contours[cnt].Length; j++)
            //{
            //    if (j == 3)
            //        break;
            //    else
            //    {

            //        //Cv2.Line(frame, contours[cnt][j], contours[cnt][j + 1], Scalar.Red, 2);
            //    }
            //}
            //if (contours[cnt].Length == 4)
            //{
            //    //RotatedRect rotatedRect = Cv2.MinAreaRect(contours[cnt]);
            //    //Cv2.DrawContours(frame, contours, cnt, Scalar.Black, 5);

            //}
            //if (contours[cnt].Length == 0)
            //{
            //    //Cv2.DrawContours(frame, contours, cnt, Scalar.Red, 5);

            //}
            //else
            //{
            //    //Cv2.DrawContours(frame, contours, cnt, Scalar.Green, 5);



            //    //OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[1]);
            //    OpenCvSharp.Rect boundingRect12 = Cv2.BoundingRect(contours[cnt]);

            //    Cv2.Rectangle(frame, boundingRect12, Scalar.Yellow, 2);
            //    Mat mat = new Mat(frame, boundingRect12);

            //    //Cv2.ImShow("asd" + cnt, mat);

            //}



            //return true;
        }

        public bool JudgeErr (OpenCvSharp.Mat mat)
        {


            int whitePixelCount = Cv2.CountNonZero(mat);// 흑백사진

            if (whitePixelCount >= 1000)
            {
                //MessageBox.Show("오류" + whitePixelCount);
                return false;
            }
            else
            {
                //MessageBox.Show("정상");
                return true;
            }

            //return false;
        }
        public void save_img() //저장
        {
            string local_address = "C:\\Users\\iot\\Source\\Repos\\kimgun140\\TP_12\\images\\save_img.jpg";
            Cv2.ImWrite(local_address, frame);

        }
    }
}
