using Emgu.CV;
using Emgu.CV.Structure;
using KT.Common.WpfApp.Utils;
using KT.Quanta.ArcDevice.ArcFace.Utils;
using KT.Quanta.Device.ArcFace.Utils;
using Prism.Mvvm;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KT.TestTool.EmguFace.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

            Init();
        }

        private void Init()
        {
            var image = ArcImageUtil.readFromFile("C:/Users/Administrator/Pictures/face/10161421.png");

            //ImageUtil.SaveToFile(image, "C:/Users/Administrator/Pictures/face/bbb.jpg", true, ImageFormat.Jpeg);

            var cascadeFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "haarcascades", "haarcascade_frontalface_default.xml");
            var haarCascade = new CascadeClassifier(cascadeFullName);

            var grayImage = new Image<Bgr, byte>("C:/Users/Administrator/Pictures/face/10161421.png");
            // detect face
            var faceRectangles = haarCascade.DetectMultiScale(grayImage, 1.1, 10);
            // grayImage.de(haarCascade, 1.2, 10, HaarDetectionType.DoCannyPruning, new System.Drawing.Size(20, 20));

            foreach (var faceRectangle in faceRectangles)
            {
                var detectedFace = grayImage.Copy(faceRectangle).Convert<Gray, byte>();
                var faceBitmap = detectedFace.ToBitmap();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    //ImageUtil.SaveToFile(faceBitmap, "C:/Users/Administrator/Pictures/face/aaa.jpg", true, ImageFormat.Jpeg);

                    FaceBitmap = ImageConvert.BitmapToBitmapImage(faceBitmap);
                });
                break;
            }
        }

        private BitmapImage _faceBitmap;
        public BitmapImage FaceBitmap
        {
            get { return _faceBitmap; }
            set { SetProperty(ref _faceBitmap, value); }
        }


    }
}
