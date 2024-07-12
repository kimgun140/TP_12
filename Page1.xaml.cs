using OpenCvSharp;
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
        ImgFuncs imgFuncs = new ImgFuncs();


        public Page1()
        {
            InitializeComponent();
            //string   save = DateTime.Now.ToString("yyyy-MM-dd-hh시mm분ss초"); // t시간 이름 대입
            Mat frame = new Mat();
        }


        private void asdf()
        {

            VideoCapture cam = new VideoCapture(0);

            while (Cv2.WaitKey(33) != 'q')
            {
                Mat frame = imgFuncs.MakeFrame(cam);
                Mat src = imgFuncs.PreProcessing();
                imgFuncs.OnlyMakeContours(src);
                Cv2.ImShow("frame", imgFuncs.frame);
                this.qwer.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(imgFuncs.frame);
            } // while

            Cv2.WaitKey(0);


            Cv2.DestroyAllWindows();


        }

        private void ProcessContour(Mat frame)
        //파형검사 
        {

            int whitePixelCount = Cv2.CountNonZero(frame);// 흑백사진
            Cv2.ImShow("ffff", frame);
            if (whitePixelCount >= 1000)
            {
                MessageBox.Show("오류" + whitePixelCount);
            }
            else
            {
                MessageBox.Show("정상");
            }
            aaaaa.Text += whitePixelCount.ToString();


        }


     


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            asdf();
        }

        private void save_img_Click(object sender, RoutedEventArgs e)
        {
            imgFuncs.save_img();
            Mat src = imgFuncs.PreProcessing();
            if (imgFuncs.MakeContours(src) == true)
            {
                aaaaa.Text += "정상";
            }
            else
            {
                aaaaa.Text += "에러";
            }
        }
    }
}
