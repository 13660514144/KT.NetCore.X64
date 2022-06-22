using KT.Visitor.IdReader.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader
{
    public static class ImageHelper
    { 
        /// <summary>
        /// Convert Byte[] to Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            //   byte pir =new byte();1280*1024
            int width = 1280, heiht = 1024;
            byte[] data = buffer;// data.lengh = 949608
            //int back = GetData(data);
            int len = data.Length;// len = 946704
            /* Bitmap img = new Bitmap(width,heiht, PixelFormat.Format24bppRgb);
             BitmapData data1 = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
             System.Runtime.InteropServices.Marshal.Copy(data, 0, data1.Scan0, data.Length);
             img.UnlockBits(data1);*/

            Bitmap img = new Bitmap(width, heiht, PixelFormat.Format24bppRgb);
            BitmapData data1 = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            System.Runtime.InteropServices.Marshal.Copy(data, 0, data1.Scan0, data.Length);
            img.UnlockBits(data1);
            img.RotateFlip(RotateFlipType.Rotate180FlipX);
            return img;
        }

        /// <summary>
        /// Convert Byte[] to a picture and Store it in file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string CreateImageFromBytes(string fileName, byte[] buffer)
        {
            string file = fileName;
            Image image = BytesToImage(buffer);
            ImageFormat format = image.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                file += ".jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                file += ".png";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                file += ".bmp";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                file += ".gif";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                file += ".icon";
            }
            System.IO.FileInfo info = new System.IO.FileInfo(file);
            System.IO.Directory.CreateDirectory(info.Directory.FullName);
            File.WriteAllBytes(file, buffer);
            return file;
        }

        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        /// <summary>
        ///  保存图像pic到文件fileName中，指定图像保存格式
        /// </summary>
        public static void SaveToFile(Image pic, string fileName, bool replace, ImageFormat format)
        {
            //若图像已存在，则删除
            if (System.IO.File.Exists(fileName) && replace)
            {
                System.IO.File.Delete(fileName);
            }

            //若不存在则创建
            if (!System.IO.File.Exists(fileName))
            {
                //根据拓展名获取图像的对应存储类型
                if (format == null)
                {
                    format = getFormat(fileName);
                }

                if (format == ImageFormat.MemoryBmp)
                {
                    pic.Save(fileName);
                }
                //按给定格式保存图像
                else pic.Save(fileName, format);
            }
        }

        /// <summary>
        ///  根据文件拓展名，获取对应的存储类型
        /// </summary>
        public static ImageFormat getFormat(string filePath)
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
                throw IdReaderException.Run("未包含要存存的图片格式：【" + Ext + "】");
            }
            return format;
        }

        /// <summary>
        /// 读取图片文件
        /// </summary>
        /// <param name="path">图片文件路径</param>
        /// <returns>图片文件</returns>
        public static Bitmap ReadImageFile(String path)
        {
            Bitmap bitmap = null;

            FileStream fileStream = File.OpenRead(path);
            Int32 filelength = 0;
            filelength = (int)fileStream.Length;
            Byte[] image = new Byte[filelength];
            fileStream.Read(image, 0, filelength);
            System.Drawing.Image result = System.Drawing.Image.FromStream(fileStream);
            fileStream.Close();
            bitmap = new Bitmap(result);

            return bitmap;
        }
    }

}
