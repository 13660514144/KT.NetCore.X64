using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KT.Common.WpfApp.Utils
{
    public class ImageConvert
    {
        public static BitmapImage WriteableBitmapToBitmapImage(WriteableBitmap writeableBitmap)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(writeableBitmap));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            return bmImage;
        }

        public static ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(hBitmap);
            return wpfBitmap;
        }

        public static Image BitmapToImage(Bitmap bitmap)
        {
            return bitmap;
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap, ImageFormat imageFormat)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, imageFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            return BitmapToBitmapImage(bitmap, bitmap.RawFormat);
        }

        public static BitmapSource ImageToBitmapSource(Image image)
        {
            IntPtr hbitmap = (image as Bitmap).GetHbitmap();
            BitmapSource sourceFromHbitmap = Imaging.CreateBitmapSourceFromHBitmap(hbitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hbitmap);
            return sourceFromHbitmap;
        }

        public static BitmapImage ImageToBitmapImage(Image image, ImageFormat imageFormat)
        {
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, imageFormat);
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
                return bitmapImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static BitmapImage ImageToBitmapImage(Image image)
        {
            return ImageToBitmapImage(image, image.RawFormat);
        }

        public static byte[] BitmapImageToBytes(BitmapImage bitmapImage)
        {
            byte[] byteArray = null;
            Stream sMarket = bitmapImage.StreamSource;
            if (sMarket != null && sMarket.Length > 0)
            {
                //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。   
                sMarket.Position = 0;
                using (BinaryReader br = new BinaryReader(sMarket))
                {
                    byteArray = br.ReadBytes((int)sMarket.Length);
                }
            }
            return byteArray;
        }

        public static Bitmap ImageSourceToBitmap(ImageSource imageSource)
        {
            BitmapSource m = (BitmapSource)imageSource;
            // 坑点：选Format32bppRgb将不带透明度
            Bitmap bmp = new Bitmap(m.PixelWidth, m.PixelHeight,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            System.Drawing.Imaging.BitmapData data = bmp.LockBits(
                new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);

            return bmp;
        }

        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            return BitmapToBytes(bitmap, ImageFormat.Bmp);
        }

        public static byte[] BitmapToBytes(Bitmap bitmap, ImageFormat imageFormat)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, imageFormat);
                byte[] buffer = memoryStream.GetBuffer();
                memoryStream.Close();
                return buffer;
            }
        }

        public static Bitmap BytesToBitmap(byte[] bytes)
        {
            Bitmap bitmap;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                bitmap = (Bitmap)Image.FromStream(memoryStream);
                memoryStream.Close();
                memoryStream.Dispose();
            }
            return bitmap;
        }


        public static BitmapSource BytesToBitmapSource(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0)
            {
                return null;
            }
            Bitmap bitmap;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                bitmap = (Bitmap)System.Drawing.Image.FromStream((Stream)memoryStream);
                memoryStream.Close();
                memoryStream.Dispose();
            }
            IntPtr hbitmap = bitmap.GetHbitmap();
            BitmapSource sourceFromHbitmap = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hbitmap);
            return sourceFromHbitmap;
        }
        public static byte[] ImageToBytes(Image image)
        {
            byte[] imagebt = BitmapToBytes(image as Bitmap);
            return imagebt;
        }
        public static Bitmap BitmapSourceToBitmap(BitmapSource bitmapSource)
        {
            Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapSource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }
        public static Image BytesToImage(byte[] bytes)
        {
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }

        public static Image BytesConvertToImage(byte[] bytes)
        {
            ImageConverter converter = new ImageConverter();
            Image image = (Image)converter.ConvertFrom(bytes);

            return image;
        }
        public static byte[] BitmapSourceToBytes(BitmapSource bitmapSource)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static BitmapImage BytesToBitmapImage(byte[] bytes)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(bytes);
            bmp.EndInit();
            return bmp;
        }

        [DllImport("gdi32")]
        public static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// base64编码的字符串转为图片
        /// </summary>
        /// <param name="source">base64字符串</param>
        /// <returns>bitmap格式图片</returns>
        public static Bitmap Base64StringToImage(string source)
        {
            byte[] bytes = Convert.FromBase64String(source);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
        }

        /// <summary>
        /// 将bitmapImage对象转换成Bitmap对象
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (System.IO.MemoryStream outStream = new System.IO.MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return bitmap;
            }
        }
    }
}
