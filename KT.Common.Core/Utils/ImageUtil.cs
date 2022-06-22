using KT.Common.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace KT.Common.Core.Utils
{
    public class ImageUtil
    {

        /// <summary>
        /// 按指定宽高缩放图片
        /// </summary>
        /// <param name="image">原图片</param>
        /// <param name="dstWidth">目标图片宽</param>
        /// <param name="dstHeight">目标图片高</param>
        /// <returns></returns>
        public static Image ScaleImage(Image image, int dstWidth, int dstHeight)
        {
            Graphics g = null;
            try
            {
                //按比例缩放           
                float scaleRate = GetWidthAndHeight(image.Width, image.Height, dstWidth, dstHeight);
                int width = (int)(image.Width * scaleRate);
                int height = (int)(image.Height * scaleRate);

                //将宽度调整为4的整数倍
                if (width % 4 != 0)
                {
                    width = width - width % 4;
                }

                Bitmap destBitmap = new Bitmap(width, height);
                g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);

                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                return destBitmap;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
            }

            return null;
        }

        /// <summary>
        /// 获取图片缩放比例
        /// </summary>
        /// <param name="oldWidth">原图片宽</param>
        /// <param name="oldHeigt">原图片高</param>
        /// <param name="newWidth">目标图片宽</param>
        /// <param name="newHeight">目标图片高</param>
        /// <returns></returns>
        public static float GetWidthAndHeight(int oldWidth, int oldHeigt, int newWidth, int newHeight)
        {
            //按比例缩放           
            float scaleRate = 0.0f;
            if (oldWidth >= newWidth && oldHeigt >= newHeight)
            {
                int widthDis = oldWidth - newWidth;
                int heightDis = oldHeigt - newHeight;
                if (widthDis > heightDis)
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
                else
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
            }
            else if (oldWidth >= newWidth && oldHeigt < newHeight)
            {
                scaleRate = newWidth * 1f / oldWidth;
            }
            else if (oldWidth < newWidth && oldHeigt >= newHeight)
            {
                scaleRate = newHeight * 1f / oldHeigt;
            }
            else
            {
                int widthDis = newWidth - oldWidth;
                int heightDis = newHeight - oldHeigt;
                if (widthDis > heightDis)
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
                else
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
            }
            return scaleRate;
        }

        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="src">原图片</param>
        /// <param name="left">左坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <returns>剪裁后的图片</returns>
        public static Image CutImage(Image src, int left, int top, int right, int bottom)
        {
            try
            {
                Bitmap srcBitmap = new Bitmap(src);
                int width = right - left;
                int height = bottom - top;
                Bitmap destBitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(destBitmap))
                {
                    g.Clear(Color.Transparent);

                    //设置画布的描绘质量         
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(srcBitmap, new Rectangle(0, 0, width, height), left, top, width, height, GraphicsUnit.Pixel);
                }

                return destBitmap;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="src">原图片</param>
        /// <param name="left">左坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <returns>剪裁后的图片</returns>
        public static Bitmap CutImage(Bitmap srcBitmap, int left, int top, int right, int bottom)
        {
            try
            {
                int width = right - left;
                int height = bottom - top;
                Bitmap destBitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(destBitmap))
                {
                    g.Clear(Color.Transparent);

                    //设置画布的描绘质量         
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(srcBitmap, new Rectangle(0, 0, width, height), left, top, width, height, GraphicsUnit.Pixel);
                }

                return destBitmap;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public static Image ReadFromFile(string imageUrl)
        {
            Image img = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(imageUrl, FileMode.Open, FileAccess.Read);
                img = Image.FromStream(fs);
            }
            finally
            {
                fs.Close();
            }
            return img;
        }

        /// <summary>
        ///  保存图像pic到文件fileName中，指定图像保存格式
        /// </summary>
        public static void SaveToFile(Image pic, string fileName, bool replace, ImageFormat format)
        {
            var bitmap = new Bitmap(pic);

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
                bitmap.Save(fileName);
            }
            //按给定格式保存图像
            else
            {
                bitmap.Save(fileName, format);
            }
        }

        /// <summary>
        ///  保存图像pic到文件fileName中，指定图像保存格式
        /// </summary>
        public static void SaveToFile(byte[] bytes, string partPath, string fileName, bool replace, ImageFormat format)
        {
            var path = Path.Combine(AppContext.BaseDirectory, partPath);

            fileName = Path.Combine(path, fileName);

            //若图像已存在，则删除
            if (File.Exists(fileName) && replace)
            {
                File.Delete(fileName);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //根据拓展名获取图像的对应存储类型
            if (format == null)
            {
                format = GetFormat(fileName);
            }

            using (var stream = new MemoryStream(bytes))
            {
                using (Image img = System.Drawing.Image.FromStream(stream))
                {
                    img.Save(fileName, format);
                }
            }
        }

        /// <summary>
        ///  保存图像pic到文件fileName中，指定图像保存格式
        /// </summary>
        public static void SaveToFile(byte[] bytes, string fileFullName, bool replace, ImageFormat format)
        {
            //若图像已存在，则删除
            if (File.Exists(fileFullName) && replace)
            {
                File.Delete(fileFullName);
            }

            var path = Path.GetDirectoryName(fileFullName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //根据拓展名获取图像的对应存储类型
            if (format == null)
            {
                format = GetFormat(fileFullName);
            }

            using (var stream = new MemoryStream(bytes))
            {
                using (Image img = System.Drawing.Image.FromStream(stream))
                {
                    img.Save(fileFullName, format);
                }
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
        public static Image CompressImageQualityLenth(Image source, int lenth)
        {
            Image image;

            try
            {
                //宽度过大剪剪,大于800剪成800
                var sourceWidth = source.Width;
                if (sourceWidth > 800)
                {
                    //var percent = 800D / sourceWidth;
                    source = PercentContractImage(source, 0.9);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var bytes = CompressImageQualityToBytes(source, 90);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                ms.Position = 0;
                image = Image.FromStream(ms);
            }
            if (bytes.Length > lenth)
            {
                image = CompressImageQualityLenth(image, lenth);
            }
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
