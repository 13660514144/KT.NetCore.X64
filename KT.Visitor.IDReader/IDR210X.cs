using IDevices;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Enums;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IdReader.SDK;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace KT.Visitor.IdReader
{
    /// <summary>
    /// 精轮阅读器
    /// </summary>
    public class IDR210X : IDevice
    {
        private ILogger _logger;

        public IDR210X()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
        }

        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.IDR210;
        public Action<object> ResultCallBack { get; set; }

        public int Port { get; set; }


        public bool Authenticate()
        {
            var result = IDR210SDKX.auth();
            if (result == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 阅读器认证失败：status:{result}");
            }
            return true;
        }

        public bool CloseComm()
        {
            var result = IDR210SDKX.close();
            if (result == 0)
            {
                _logger?.LogError($"{ReaderType.Value} - 阅读器关闭失败：status:{result}");
            }
            return true;
        }

        public bool InitComm()
        {
            var result = IDR210SDKX.IDCardLoading(1001);
            if (result != 1)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 阅读器连接失败：status:{result}");
            }
            return true;
        }

        public void StartSignalLamp()
        {
        }

        public Person ReadContent()
        {
            StringBuilder Name = new StringBuilder(31);
            StringBuilder Gender = new StringBuilder(3);
            StringBuilder Folk = new StringBuilder(10);
            StringBuilder BirthDay = new StringBuilder(9);
            StringBuilder Code = new StringBuilder(19);
            StringBuilder Address = new StringBuilder(71);
            StringBuilder Agency = new StringBuilder(31);
            StringBuilder ExpireStart = new StringBuilder(9);
            StringBuilder ExpireEnd = new StringBuilder(9);

            //var result = IDR210SDKX.ReadBaseInfos(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd);
            StringBuilder per = new StringBuilder(50*1024);
            var result = IDR210SDKX.readCardInfo(per);
            _logger.LogInformation($"result===>{per.ToString()}");
            if (result != 1)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 阅读器读卡失败：status:{result}");
            }
            Person person = new Person();

            person.Name = Name.ToString();
            person.Gender = Gender.ToString();
            person.Nation = Folk.ToString();
            person.Birthday = BirthDay.ToString();
            person.Address = Address.ToString();
            person.IdCode = Code.ToString();
            person.Agency = Agency.ToString();
            person.ExpireStart = ExpireStart.ToString();
            person.ExpireEnd = ExpireEnd.ToString();

            person.CardType = CertificateTypeEnum.ID_CARD.Value;

            try
            {
                //图片读取出来再关闭，避免第二张身份证无法读取 
                var imageUrl = Path.Combine(AppContext.BaseDirectory, "ReferenceFiles/IdReaderSdks/IDR210X/photo.bmp");
                Image image = Image.FromFile(imageUrl);
                if (image == null)
                {
                    throw IdReaderException.Run($"{ReaderType.Value} - 获取头像失败！");
                }
                System.Drawing.Image bmp = new System.Drawing.Bitmap(image);
                image.Dispose();
                person.Portrait = bmp;

                //保存头像到文件夹
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\Images\\Portraits");
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }
                imagePath = Path.Combine(imagePath, person.IdCode + ".bmp");
                ImageHelper.SaveToFile(person.Portrait, imagePath, true, ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ReaderType.Value} - 图片处理出错：{0} ", ex);
            }

            return person;
        }

        public Person ScanContent(string operateIdType)
        {
            throw new NotImplementedException();
        }
    }
}
