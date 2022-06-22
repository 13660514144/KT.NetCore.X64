using KT.Common.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace KT.Common.WpfApp.Utils
{
    public static class ImageUtil
    {
        /// <summary>
        ///  保存图像pic到文件fileName中，指定图像保存格式
        /// </summary>
        public static void SaveToFile(Image pic, string fileName, bool replace, ImageFormat format)
        {
            //若图像已存在，则删除
            if (File.Exists(fileName) && replace)
            {
                File.Delete(fileName);
            }

            //根据拓展名获取图像的对应存储类型
            if (format == null)
            {
                format = GetFormat(fileName);
            }

            if (format == ImageFormat.MemoryBmp)
            {
                pic.Save(fileName);
            }
            //按给定格式保存图像
            else
            {
                pic.Save(fileName, format);
            }
        }

        /// <summary>
        ///  保存图像pic到文件fileName中，指定图像保存格式
        /// </summary>
        public static void SaveToFile(byte[] bytes, string partPath, string fileName, bool replace, ImageFormat format)
        {
            //若图像已存在，则删除
            if (File.Exists(fileName) && replace)
            {
                File.Delete(fileName);
            }
            var path = Path.Combine(AppContext.BaseDirectory, partPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            fileName = Path.Combine(path, fileName);

            //根据拓展名获取图像的对应存储类型
            if (format == null)
            {
                format = GetFormat(fileName);
            }

            using (var stream = new MemoryStream(bytes))
            {
                Image img = System.Drawing.Image.FromStream(stream);
                img.Save(fileName, format);
            }
        }

        /// <summary>
        ///  根据文件拓展名，获取对应的存储类型
        /// </summary>
        public static ImageFormat GetFormat(string filePath)
        {
            ImageFormat format = ImageFormat.MemoryBmp;
            string Ext = System.IO.Path.GetExtension(filePath).ToLower();

            if (Ext.Equals(".png"))
            {
                format = ImageFormat.Png;
            }
            else if (Ext.Equals(".jpg") || Ext.Equals(".jpeg"))
            {
                format = ImageFormat.Jpeg;
            }
            else if (Ext.Equals(".bmp"))
            {
                format = ImageFormat.Bmp;
            }
            else if (Ext.Equals(".gif"))
            {
                format = ImageFormat.Gif;
            }
            else if (Ext.Equals(".ico"))
            {
                format = ImageFormat.Icon;
            }
            else if (Ext.Equals(".emf"))
            {
                format = ImageFormat.Emf;
            }
            else if (Ext.Equals(".exif"))
            {
                format = ImageFormat.Exif;
            }
            else if (Ext.Equals(".tiff"))
            {
                format = ImageFormat.Tiff;
            }
            else if (Ext.Equals(".wmf"))
            {
                format = ImageFormat.Wmf;
            }
            else if (Ext.Equals(".memorybmp"))
            {
                format = ImageFormat.MemoryBmp;
            }
            else
            {
                throw CustomException.Run("未包含要存存的图片格式：【" + Ext + "】");
            }
            return format;
        }


        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image MakeThumbnailImage(byte[] datas, int width, int height)
        {
            if (datas == null)
            {
                return null;
            }

            Bitmap bitmap;
            using (MemoryStream memoryStream = new MemoryStream(datas))
            {
                bitmap = (Bitmap)System.Drawing.Image.FromStream((Stream)memoryStream);
                memoryStream.Close();
                memoryStream.Dispose();
            }
            if (bitmap.Size.Width == 0 || bitmap.Size.Height == 0)
            {
                return null;
            }
            int _newWidth = 0;
            int _newHeight = 0;
            if (bitmap.Size.Width > bitmap.Size.Height)
            {
                _newWidth = width;
                _newHeight = (bitmap.Size.Height * _newWidth) / bitmap.Size.Width;
            }
            else
            {
                _newHeight = height;
                _newWidth = (bitmap.Size.Width * _newHeight) / bitmap.Size.Height;
            }
            System.Drawing.Image image = bitmap.GetThumbnailImage(_newWidth, _newHeight, () => { return true; }, IntPtr.Zero);
            return image;
        }

        /// <summary>
        /// 品质压缩
        /// </summary> 
        public static Image CompressImageQuality(Image iSource, int flag)
        {
            var bytes = CompressImageQualityToBytes(iSource, flag);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                ms.Position = 0;
                image = Image.FromStream(ms);
            }
            return image;
        }

        /// <summary>
        /// 品质压缩
        /// </summary> 
        private static byte[] CompressImageQualityToBytes(Image image, int flag)
        {
            //宽度过大剪剪,大于800剪成五百
            var sourceWidth = image.Width;
            if (sourceWidth > 800)
            {
                var percent = 500D / sourceWidth;
                image = PercentContractImage(image, percent);
            }

            //Graphics gfx = Graphics.FromImage(iSource);
            //Image bm = new Bitmap(500, iSource.Height * (iSource.Width / 500));

            Bitmap ob = new Bitmap(image);
            ImageFormat tFormat = image.RawFormat;
            //以下代码为保存图片时，设置压缩质量  
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100  
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }

                byte[] result;
                using (MemoryStream stream = new MemoryStream())
                {
                    if (jpegICIinfo != null)
                    {
                        ob.Save(stream, jpegICIinfo, ep);
                    }
                    else
                    {
                        ob.Save(stream, tFormat);
                    }
                    result = stream.ToArray();
                }

                image.Dispose();
                ob.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                image.Dispose();
                ob.Dispose();
            }
        }
        /// <summary>
        /// 图片压缩质量
        /// </summary>
        /// <param name="bimg"></param>
        /// <param name="maxKb">最大图片大小</param>
        /// <param name="flag">每次压缩量（1~100）</param>
        /// <returns></returns>
        public static byte[] CompressImageQualityToBytes(byte[] bimg, int maxKb, int flag = 80)
        {
            if (bimg.Length / 1024 > 20 * 1024)
            {
                throw new Exception("图片过大。");
            }

            //图片大于150K压缩 
            if (bimg.Length / 1024 > maxKb)
            {
                Image image = ImageConvert.BytesToImage(bimg);//转换为图片
                try
                {
                    bimg = CompressImageQualityToBytes(image, flag);//压缩图片
                    bimg = CompressImageQualityToBytes(bimg, maxKb);//图片质量过大再执行一次这方法  
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    image.Dispose();
                }
            }
            return bimg;
        }

        /// <summary>
        /// 等比限长宽缩放图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap LimitLengthContractImage(Image image, double maxWidth, double maxHeight)
        {
            try
            {
                if (maxWidth >= image.Width && image.Height >= maxHeight)
                {
                    return image as Bitmap;
                }
                image.Dispose();
                throw new Exception("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                image.Dispose();
            }
        }

        /// <summary>
        /// 等比缩放
        /// </summary>
        /// <param name="image"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static Bitmap PercentContractImage(Image image, double percent)
        {
            // 缩小后的高度 
            int newH = int.Parse(Math.Round(image.Height * percent).ToString());
            // 缩小后的宽度 
            int newW = int.Parse(Math.Round(image.Width * percent).ToString());

            // 要保存到的图片 
            Bitmap b = new Bitmap(newW, newH);
            Graphics g = Graphics.FromImage(b);
            try
            {
                // 插值算法的质量 
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(image, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                return b;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
