using IDevices;
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
    public class CVR100U : IDevice
    {
        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.CVR100U;
        public Action<object> ResultCallBack { get; set; }

        public int Port { get; set; }
        public bool IsScanImage { get; set; }

        private int _minPort = 1001;
        private int _maxPort = 1016;

        private ILogger _logger;

        public CVR100U(ILogger logger)
        {
            IsScanImage = false;

            _logger = logger;
        }
        /// <summary>
        /// 卡认证
        /// </summary>
        /// <returns>0失败，1是成功</returns>
        public bool Authenticate()
        {
            int r = CVR100USDK.CVR_Authenticate();
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 卡片认证失败!");
            }

            _logger.LogInformation($"{ ReaderType.Value}卡片认证成功：{r} ");
            return true;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool CloseComm()
        {

            int r = CVR100USDK.CVR_CloseComm();
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 关闭连接失败!");
            }

            _logger.LogInformation($"{ReaderType.Value}设备关闭成功：{r} ");
            return true;
        }

        /// <summary>
        /// 初始化端口，连接设备
        /// </summary>
        /// <param name="Port"></param>
        /// <returns></returns>
        public bool InitComm()
        {
            int iPort, iRetUSB = 0;
            CVR100USDK.CVR_SetComBaudrate(9600);// 设置波特率
            for (iPort = _minPort; iPort <= _maxPort; iPort++)
            {
                iRetUSB = CVR100USDK.CVR_InitComm(iPort);
                if (iRetUSB == 1)
                {
                    Port = iPort;
                    break;
                }
            }
            if (iRetUSB == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 设备连接失败!");
            }

            _logger.LogInformation($"{ReaderType.Value}设备连接成功：retUsb:{iRetUSB} port:{Port} ");
            return true;
        }

        public void StartSignalLamp()
        {
        }

        /// <summary>
        /// 读卡操作
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public Person ReadContent()
        {
            int r = CVR100USDK.CVR_Read_FPContent();
            //int r =CVR100USDK.CVR_Read_Content(4);
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 卡片读取失败：{r} ");
            }

            _logger.LogInformation($"{ReaderType.Value} - 卡片读取成功：{r} ");
            //获取身份证件数据
            Person person = FillData();
            person.CardType = CertificateTypeEnum.ID_CARD.Value;

            ////保存头像到文件夹
            //string imagePath = Path.Combine(AppContext.BaseDirectory, "Files\\Images\\Portraits");
            //if (!Directory.Exists(imagePath))
            //{
            //    Directory.CreateDirectory(imagePath);
            //}
            //imagePath = Path.Combine(imagePath, person.IdCode + ".bmp");
            //ImageHelper.SaveToFile(person.Portrait, imagePath, true, ImageFormat.Bmp);

            return person;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        private Person FillData()
        {
            Person person = new Person();
            byte[] imgData = new byte[40960];  //X86
            int length = 40960; //X86
                                //X64
                                //byte[] imgData = new byte[38862];
                                //int length = 38862;
                                //X64
            /*int r = CVR100USDK.GetBMPData(ref imgData[0], ref length);
            if (r == 0)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 获取证件信息失败!");
            }
            MemoryStream myStream = new MemoryStream();
            for (int i = 0; i < length; i++)
            {
                myStream.WriteByte(imgData[i]);
            }
            Image myImage = Image.FromStream(myStream);
            */
            String szXPPath = "zp.bmp";
            System.Drawing.Image myImage = System.Drawing.Image.FromFile(szXPPath);
            System.Drawing.Image bmp = new System.Drawing.Bitmap(myImage);
            myImage.Dispose();
            person.Portrait = bmp;

            byte[] name = new byte[128];
            length = 128;
            CVR100USDK.GetPeopleName(ref name[0], ref length);
            byte[] cnName = new byte[128];
            length = 128;
            CVR100USDK.GetPeopleChineseName(ref cnName[0], ref length);
            byte[] number = new byte[128];
            length = 128;
            CVR100USDK.GetPeopleIDCode(ref number[0], ref length);
            byte[] peopleNation = new byte[128];
            length = 128;
            CVR100USDK.GetPeopleNation(ref peopleNation[0], ref length);
            byte[] peopleNationCode = new byte[128];
            length = 128;
            CVR100USDK.GetNationCode(ref peopleNationCode[0], ref length);
            byte[] validtermOfStart = new byte[128];
            length = 128;
            CVR100USDK.GetStartDate(ref validtermOfStart[0], ref length);
            byte[] birthday = new byte[128];
            length = 128;
            CVR100USDK.GetPeopleBirthday(ref birthday[0], ref length);
            byte[] address = new byte[128];
            length = 128;
            CVR100USDK.GetPeopleAddress(ref address[0], ref length);
            byte[] validtermOfEnd = new byte[128];
            length = 128;
            CVR100USDK.GetEndDate(ref validtermOfEnd[0], ref length);
            byte[] signdate = new byte[128];
            length = 128;
            CVR100USDK.GetDepartment(ref signdate[0], ref length);
            byte[] sex = new byte[128];
            length = 128;
            CVR100USDK.GetPeopleSex(ref sex[0], ref length);
            byte[] samid = new byte[128];
            CVR100USDK.CVR_GetSAMID(ref samid[0]);

            byte[] certType = new byte[32];
            length = 32;
            CVR100USDK.GetCertType(ref certType[0], ref length);

            person.Name = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(name);
            person.Gender = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(sex).Replace("\0", "").Trim();
            person.Nation = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(peopleNation).Replace("\0", "").Trim();
            //person.NationCode = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(peopleNationCode).Replace("\0", "").Trim();
            person.Birthday = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(birthday).Replace("\0", "").Trim();
            person.IdCode = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(number).Replace("\0", "").Trim();
            person.Address = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(address).Replace("\0", "").Trim();
            person.Agency = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(signdate).Replace("\0", "").Trim();
            person.ExpireStart = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(validtermOfStart).Replace("\0", "").Trim();
            person.ExpireEnd = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(validtermOfEnd).Replace("\0", "").Trim();
            person.SamId = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(samid).Replace("\0", "").Trim();

            return person;
        }

        public Person ScanContent(string operateIdType)
        {
            return ReadContent();
        }
    }
}
