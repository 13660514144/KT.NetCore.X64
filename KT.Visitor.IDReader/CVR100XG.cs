using IDevices;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IdReader.SDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader
{
    public class CVR100XG : IDevice
    {
        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.CVR100XG;
        public Action<object> ResultCallBack { get; set; }

        public int Port { get; set; }

        public CVR100XG()
        {

        }

        /// <summary>
        /// RFID卡认证
        /// </summary>
        /// <returns></returns>
        public bool Authenticate()
        {
            int r = CVR100XGSDK.CVR_Authenticate();
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 阅读器认证失败！");
            }
            return true;
        }

        public bool CloseComm()
        {
            bool r = CVR100XGSDK.IO_StopRFID();
            if (!r)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 关闭连接失败！");
            }
            return true;
        }

        /// <summary>
        /// RFID卡初始化
        /// </summary>
        /// <returns></returns>
        public bool InitComm()
        {
            int iPort, iRetUSB = 0;
            for (iPort = 1001; iPort <= 1016; iPort++)
            {

                iRetUSB = CVR100XGSDK.CVR_InitComm(iPort);
                if (iRetUSB == 1)
                {
                    Port = iPort;

                    break;
                }
            }
            if (iRetUSB == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 初始化连接失败！");
            }
            return true;
        }

        public void StartSignalLamp()
        {
        }

        /// <summary>
        /// RFID 全步骤自动读取
        /// </summary>
        /// <returns></returns>
        public Person ReadContent()
        {
            string path = Directory.GetCurrentDirectory();
            path = path + "\\Picture.bmp";
            File.Delete(path);

            // 启动RFID卡 
            bool rs = CVR100XGSDK.IO_StartRFID();
            if (!rs)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 启动RFID卡失败：status:{rs}");
            }

            // 搜索RFID卡
            var r = CVR100XGSDK.IO_SearchCard();
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 搜索RFID卡失败：status:{r}");
            }

            // 选择RFID卡
            r = CVR100XGSDK.IO_SelectCard();
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 选择RFID卡失败：status:{r}");
            }

            // 读取RFID卡
            CVR100XGSDK.Bar bar;
            r = CVR100XGSDK.IO_ReadCard(out bar);
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 读取RFID卡失败：status:{r}");
            }

            // 关闭RFIF卡
            rs = CVR100XGSDK.IO_StopRFID();
            if (!rs)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 关闭RFIF卡失败：status:{rs}");
            }

            //返回人员信息
            Person person = new Person();

            //返回人员信息
            if (bar.nameCH == null)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 未能正确获取人员信息！");
            }

            //人员属性值
            person.Name = bar.name;
            person.Gender = bar.sex;
            person.Nation = bar.people;
            person.Birthday = bar.birthday;
            person.Address = bar.address;
            person.IdCode = bar.number;
            person.Agency = bar.organs;
            person.ExpireStart = bar.signdate;
            person.ExpireEnd = bar.validterm;

            //获取头像
            Image image = Image.FromFile(path);
            System.Drawing.Image bmp = new Bitmap(image);
            image.Dispose();
            if (bmp != null)
            {
                person.Portrait = bmp;
            }

            //保存头像到文件夹
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Images", "Portraits");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            imagePath = Path.Combine(imagePath, person.IdCode + ".bmp");
            ImageHelper.SaveToFile(person.Portrait, imagePath, true, ImageFormat.Bmp);

            return person;
        }
        public Person ScanContent(string operateIdType)
        {
            throw new NotImplementedException();
        }
    }
}
