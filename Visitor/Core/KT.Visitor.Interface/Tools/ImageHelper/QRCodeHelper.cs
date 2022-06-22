using CommonUtils;
using System.Drawing;
using ZXing;
using ZXing.Presentation;
using ZXing.QrCode;

namespace KT.Visitor.Interface.Tools.ImageHelper
{
    public class QRCodeHelper
    {
        /// <summary>
        /// 生成二维码方法
        /// </summary>
        /// <param name="text">输入的字符串</param>
        /// <param name="width">二维码宽度</param>
        /// <param name="height">二维码高度</param>
        /// <returns></returns>
        public Bitmap Generate(string text, int width = 500, int height = 500)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //将传来的值赋给二维码的宽度和高度
            options.Width = width;
            options.Height = height;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;
            var map = writer.Write(text);

            return ImageConvert.ImageSourceToBitmap(map);
        }
    }
}

//    public static Bitmap GetQRCode(string webAddr, string codeNum, Int64 encryptPlus, Int64 encryptMultiply, int pixelsPerModule)
//        {
//            //参数转16位
//            string parNum = Convert.ToString((Convert.ToInt64(codeNum) + encryptPlus) * encryptMultiply, 16);
//            // 生成二维码的内容
//            string strCode = webAddr.TrimEnd('/') + "/" + parNum;
//            QRCoder.QRCodeGenerator qrGenerator = new Encoder.QRCodeGenerator();
//            QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(strCode, QRCoder.QRCodeGenerator.ECCLevel.Q);
//            QRCoder.QRCode qrcode = new QRCoder.QRCode(qrCodeData);

//            Bitmap qrCodeImage = qrcode.GetGraphic(pixelsPerModule, System.Drawing.Color.Black, System.Drawing.Color.White, null, 15, 6, true);

//            float fontSize = pixelsPerModule * (13 / 5);
//            //return qrCodeImage;
//            return AddTextToImageButton(qrCodeImage, codeNum, fontSize);
//        }


//        /// <summary>
//        /// 指定图片添加指定文字
//        /// </summary> 
//        /// <param name="bitmap">图片</param> 
//        /// <param name="text">添加的文字</param> 
//        private static Bitmap AddTextToImageButton(Bitmap bitmap, string text, float fontSize)
//        {
//            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height + (int)(fontSize * 2.0F));

//            int i, j;
//            for (i = 0; i < newBitmap.Width; i++)
//            {
//                for (j = 0; j < newBitmap.Height; j++)
//                {
//                    //空白处画白色
//                    if (i >= bitmap.Width || j >= bitmap.Height)
//                    {
//                        newBitmap.SetPixel(i, j, System.Drawing.Color.White);
//                    }
//                    else
//                    {
//                        System.Drawing.Color pixelColor = bitmap.GetPixel(i, j);
//                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
//                        newBitmap.SetPixel(i, j, newColor);
//                    }
//                }
//            }

//            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newBitmap);

//            //文本的长度
//            float textWidth = text.Length * fontSize;
//            //下面定义一个矩形区域，以后在这个矩形里画上白底黑字
//            //float rectWidth = text.Length * (fontSize + 11);
//            float rectWidth = bitmap.Width;
//            float rectHeight = fontSize * 2.5f;//2.5f按实际比例定义

//            float rx = ((float)bitmap.Width - (textWidth * 0.8f)) / 2;//0.8f按实际比例定义
//            float rectX = rx > 0 ? rx : 0;
//            float rectY = (float)(bitmap.Height - (fontSize * 1.2f));//1.2f按实际比例定义

//            //声明矩形域
//            RectangleF textArea = new RectangleF(rectX, rectY, rectWidth, rectHeight);
//            //定义字体
//            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", fontSize, System.Drawing.FontStyle.Bold);

//            //白笔刷，画文字用
//            System.Drawing.Brush blackBrush = new SolidBrush(System.Drawing.Color.Black);
//            g.DrawString(text, font, blackBrush, textArea);
//            g.Dispose();
//            return newBitmap;
//        }

//        public static Bitmap ComposeBitmap(List<Bitmap> bitmaps, int columns, int rows)
//        {
//            if (bitmaps == null || bitmaps.Count == 0)
//            {
//                return null;
//            }

//            int count = bitmaps.Count();
//            int xLen = bitmaps.First().Width;
//            int yLen = bitmaps.First().Height;

//            Bitmap newBitmap = new Bitmap(xLen * columns, yLen * rows);
//            for (int k = 0; k < columns * rows; k++)
//            {
//                for (int i = 0; i < xLen; i++)
//                {
//                    for (int j = 0; j < yLen; j++)
//                    {
//                        int m = (k % columns) * xLen + i;
//                        int n = (k / columns) * yLen + j;
//                        if (count < k + 1)
//                        {
//                            newBitmap.SetPixel(m, n, System.Drawing.Color.White);
//                        }
//                        else
//                        {
//                            System.Drawing.Color pixelColor = bitmaps[k].GetPixel(i, j);
//                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
//                            newBitmap.SetPixel(m, n, newColor);
//                        }
//                    }
//                }
//            }
//            return newBitmap;
//        }

//    }
//}

/* GetGraphic方法参数说明
                 public Bitmap GetGraphic(int pixelsPerModule, Color darkColor, Color lightColor, Bitmap icon = null, int iconSizePercent = 15, int iconBorderWidth = 6, bool drawQuietZones = true)
             * 
                 int pixelsPerModule:生成二维码图片的像素大小 ，我这里设置的是5 
             * 
                 Color darkColor：暗色   一般设置为Color.Black 黑色
             * 
                 Color lightColor:亮色   一般设置为Color.White  白色
             * 
                 Bitmap icon :二维码 水印图标 例如：Bitmap icon = new Bitmap(context.Server.MapPath("~/images/zs.png")); 默认为NULL ，加上这个二维码中间会显示一个图标
             * 
                 int iconSizePercent： 水印图标的大小比例 ，可根据自己的喜好设置 
             * 
                 int iconBorderWidth： 水印图标的边框
             * 
                 bool drawQuietZones:静止区，位于二维码某一边的空白边界,用来阻止读者获取与正在浏览的二维码无关的信息 即是否绘画二维码的空白边框区域 默认为true
   */
