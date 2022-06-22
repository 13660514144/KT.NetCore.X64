using IDevices;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IdReader.SDK;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Visitor.IdReader
{
    /// <summary>
    /// FS531扫描仪
    /// 特别提示，SDK调用返回0为成功
    /// </summary>
    public class FS531X : IDevice
    {
        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.FS531;
        public Action<object> ResultCallBack { get; set; }

        public int Port { get; set; }
        public bool IsScanImage { get; set; }

        private ILogger _logger;

        public FS531X()
        {
            _logger = ContainerHelper.Resolve<ILogger>();

            _logger.LogInformation("FS531证件阅读器已创建！");
        }

        public bool InitComm()
        {
            

            int ret = FS531SDKX.H531_Init();
            _logger.LogInformation("FS531初始化连接结果：{0} ", ret);

            // 为0成功
            if (0 != ret)
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 识别核心、扫描仪加载失败：status:{ret} ");
            }
            return true;
        }

        public void StartSignalLamp()
        {
        }

        public bool Authenticate()
        {
            return true;
        }

        public Person ReadContent()
        {
            return ScanContent(OperateIdTypeEnum.PASSPORT.Value);
        }

        public bool CloseComm()
        {
            FS531SDKX.H531_Free();
            return true;
        }

        public Person ScanContent(string operateIdType)
        {
            _logger.LogInformation($"开始{ReaderType.Value}证件阅读器扫描：operateIdType:{operateIdType} ");
            Person person;
            if (operateIdType == OperateIdTypeEnum.ID_CARD.Value)
            {
                person = ScanIcCard(operateIdType);
            }
            else if (operateIdType == OperateIdTypeEnum.PASSPORT.Value)
            {
                person = ScanPassport(operateIdType);
            }
            else if (operateIdType == OperateIdTypeEnum.DRIVER_LICENSE.Value)
            {
                person = ScanDriverLicense(operateIdType);
            }
            else
            {
                throw IdReaderException.Run($"{ReaderType.Value} - 没有找到要扫描的设备类型！");
            }
            return person;
        }

        private Person ScanDriverLicense(string operateIdType)
        {
            _logger.LogInformation($"{ReaderType.Value} - 开始扫描：type:{operateIdType} ");
            Person person = new Person();
            //扫描证件
            StringBuilder data = new StringBuilder(8192);
            ScanOCRQueryX scanOCRQuery = new ScanOCRQueryX();
                scanOCRQuery.CardType = 2;
                var json = JsonConvert.SerializeObject(scanOCRQuery);
                _logger.LogInformation($"{ReaderType.Value} - 扫描参数：{json} ");
            string Dir = AppDomain.CurrentDomain.BaseDirectory.ToString();
            
            //开始扫描
            var scanResult = FS531SDKX.H531_ScanOCR(scanOCRQuery);
             _logger.LogInformation($"{ReaderType.Value} - 扫描结果：result:{scanResult}");
            if (scanResult != 0)
            {
                return person;
            }
            /*扫描完成开始识别*/
            int ret = FS531SDKBaseX.discernImage1(scanOCRQuery.chScanImagePath, 
                scanOCRQuery.HeaderPath, 
                5, scanOCRQuery.iJPEG_QUALITY, scanOCRQuery.OCRDATA,data);
            if (ret != 1)
            {
                return person;
            }
            _logger.LogInformation($"识别result==>{data.ToString()}");
            /*扫描完成开始识别*/
            FS531SCANX scan = new FS531SCANX();
            var  ScanJson = scan.ScanImages(scanOCRQuery.chScanImagePath, scanOCRQuery.HeaderPath, 1);            
            
            if (! string.IsNullOrEmpty($"{ScanJson}"))
            {
                //var param = JsonConvert.DeserializeObject<ScanDriverLicense>(ScanJson.ToString());
                JObject param = JObject.Parse($"{ScanJson}");
                person.Name = param["Name"].ToString();
                person.Gender = param["Sex"].ToString();
                person.Nation = param["Nation"].ToString();
                person.Birthday = param["Birthday"].ToString();
                person.Address = param["Address"].ToString();
                person.IdCode = param["CardNo"].ToString();
                person.Agency = string.Empty;
                person.ExpireStart = string.Empty;
                person.ExpireEnd = string.Empty;                
                person.CardType = operateIdType;

                try
                {
                    var headPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.HeaderPath);
                    //加载图片
                    Image image = Bitmap.FromFile(headPath);
                    if (image == null)
                    {
                        throw IdReaderException.Run($"{ReaderType.Value} - 图片未加载成功");
                    }
                    Image bmp = new Bitmap(image);
                    image.Dispose();
                    person.Portrait = bmp;

                    //删除图片
                    File.Delete(headPath);
                    var goPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.chScanImagePath);
                    File.Delete(goPath);
                    var backPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.chBackImagePath);
                    File.Delete(goPath);
                 
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ReaderType.Value} - 头像加载出错：ex:{ex} ");
                }
            }
            return person;
        }

        private Person ScanPassport(string operateIdType)
        {
            _logger.LogInformation($"{ReaderType.Value} - 开始扫描ScanPassport：type:{operateIdType} ");
            Person person = new Person();
            //try
            //{
            //扫描证件
            StringBuilder data = new StringBuilder(8192);
            ScanOCRQueryX scanOCRQuery = new ScanOCRQueryX();
                scanOCRQuery.CardType = 3;
                var json = JsonConvert.SerializeObject(scanOCRQuery);
                _logger.LogInformation($"{ReaderType.Value} - 扫描参数：{json} ");
                string Dir = AppDomain.CurrentDomain.BaseDirectory.ToString();
            var scanResult = FS531SDKX.H531_ScanOCR(scanOCRQuery);
            _logger.LogInformation($"{ReaderType.Value} - 扫描结果：result:{scanResult}");
            if (scanResult != 0)
            {
                return person;
            }
            /*扫描完成开始识别*/
            int ret = FS531SDKBaseX.discernImage1(scanOCRQuery.chScanImagePath,
                scanOCRQuery.HeaderPath,
                6, scanOCRQuery.iJPEG_QUALITY, scanOCRQuery.OCRDATA, data);
            if (ret != 1)
            {
                return person;
            }
            _logger.LogInformation($"识别result==>{data.ToString()}");
            /*扫描完成开始识别*/
            FS531SCANX scan = new FS531SCANX();
            var ScanJson = scan.ScanImages(scanOCRQuery.chScanImagePath, scanOCRQuery.HeaderPath, 2);
            
            
            if (!string.IsNullOrEmpty($"{ScanJson}"))
            {
                JObject param = JObject.Parse($"{ScanJson}");                
                person.Name = string.IsNullOrEmpty(param["NameCh"].ToString()) ? param["Name"].ToString() : param["NameCh"].ToString();
                person.Gender = string.IsNullOrEmpty(param["SexCH"].ToString()) ? param["Sex"].ToString() : param["SexCH"].ToString();
                person.Nation = param["Nation"].ToString();
                person.Birthday = param["Birthday"].ToString();
                person.Address = param["AddressCH"].ToString();
                person.IdCode = param["CardNo"].ToString();
                person.Agency = string.Empty;
                person.ExpireStart = string.Empty;
                person.ExpireEnd = string.Empty;
                person.CardType = operateIdType;
                try
                {
                    var headPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.HeaderPath);
                    //加载图片
                    Image image = Bitmap.FromFile(headPath);
                    if (image == null)
                    {
                        throw IdReaderException.Run($"{ReaderType.Value} - 图片未加载成功");
                    }
                    Image bmp = new Bitmap(image);
                    image.Dispose();
                    person.Portrait = bmp;

                    //删除图片
                    File.Delete(headPath);
                    var goPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.chScanImagePath);
                    File.Delete(goPath);
                    var backPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.chBackImagePath);
                    File.Delete(goPath);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ReaderType.Value} - 头像加载出错：ex:{ex} ");
                }
            } 
            return person;
        }

        private Person ScanIcCard(string operateIdType)
        {
            _logger.LogInformation($"{ReaderType.Value} - 开始扫描：type:{operateIdType} ");
            Person person = new Person();
            //扫描证件参数
            StringBuilder data = new StringBuilder(8192);
            ScanOCRQueryX scanOCRQuery = new ScanOCRQueryX();
            scanOCRQuery.CardType = 4;

            var json = JsonConvert.SerializeObject(scanOCRQuery);
            _logger.LogInformation($"{ReaderType.Value} - 扫描参数：{json} ");

            //开始扫描
            var scanResult = FS531SDKX.H531_ScanOCR(scanOCRQuery);
            if (scanResult != 0)
            {
                return person;
            }
            /*扫描完成开始识别*/
            int ret = FS531SDKBaseX.discernImage1(scanOCRQuery.chScanImagePath,
                scanOCRQuery.HeaderPath,
                4, scanOCRQuery.iJPEG_QUALITY, scanOCRQuery.OCRDATA, data);
            if (ret != 1)
            {
                return person;
            }
            _logger.LogInformation($"识别result==>{data.ToString()}");
            /*扫描完成开始识别*/
            var param = JsonConvert.DeserializeObject<FS351ResultX>(data.ToString());
            if (string.IsNullOrEmpty(param?.Data?.ID))
            {
                throw CustomException.Run($"{ReaderType.Value} - 扫描错误：cardId:{param?.Data?.ID} ");
            }
            person.Name = param.Data.Name;
            person.Gender = param.Data.Sex;
            person.Nation = param.Data.People;
            person.Birthday = param.Data.Birthday;
            person.Address = param.Data.Address;
            person.IdCode = param.Data.ID;
            person.Agency = string.Empty;
            person.ExpireStart = string.Empty;
            person.ExpireEnd = string.Empty;

            person.CardType = operateIdType;

            try
            {
                var headPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.HeaderPath);
                //加载图片
                Image image = Bitmap.FromFile(headPath);
                if (image == null)
                {
                    throw IdReaderException.Run($"{ReaderType.Value} - 图片未加载成功");
                }
                Image bmp = new Bitmap(image);
                image.Dispose();
                person.Portrait = bmp;

                //删除图片
                File.Delete(headPath);
                var goPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.chScanImagePath);
                File.Delete(goPath);
                var backPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scanOCRQuery.chBackImagePath);
                File.Delete(goPath);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ReaderType.Value} - 头像加载出错：ex:{ex} ");
            }
            return person;
        }
    }

    public class FS531SCANX
    {       
        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/ImportDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PaperOCR(string acOcrDataPath, string acConfigPath, string inFileName, StringBuilder outchar, string strHeadPath, int nType, int nOutSize);
        
        public  string ScanImages(string SourceImg,string HeadImg,int ScanType)
        {
            string m_ocrdatapath = "";//ocrdata路径
            string m_ocrconfigpath_id = "";//身份证config路径
            string m_ocrconfigpath_jz = "";//驾照

            string m_ocrconfigpath_hz = "";//护照

            string OCRDATA = "ReferenceFiles/IdReaderSdks/FS531X/ocr_data";
            string OCRCONFIG_ID = "ReferenceFiles/IdReaderSdks/FS531X/IDCardScanBcr.cfg";
            string OCRCONFIG_JZ = "ReferenceFiles/IdReaderSdks/FS531X/DriverScanBcr.cfg";

            string OCRCONFIG_HZ = "ReferenceFiles/IdReaderSdks/FS531X/PlateScanBcr.cfg";

            StringBuilder m_sbresult = new StringBuilder(8192);//识别结果。
            string Json = string.Empty;
            /*ScanType  0 scanid 1 scanjz 2 scan passbort*/
            m_ocrdatapath = System.Windows.Forms.Application.StartupPath.ToString() + OCRDATA;//取目录下
            m_ocrconfigpath_id = System.Windows.Forms.Application.StartupPath.ToString() + OCRCONFIG_ID;//取目录下
            m_ocrconfigpath_jz = System.Windows.Forms.Application.StartupPath.ToString() + OCRCONFIG_JZ;

            m_ocrconfigpath_hz = System.Windows.Forms.Application.StartupPath.ToString() + OCRCONFIG_HZ;

            string m_tmpOcrImagepath = System.Windows.Forms.Application.StartupPath.ToString() + "\\temp.jpg";
            int ret = -12;
            try
            {                
                string ocrconfigpath = "";
                switch (ScanType)
                {
                    case 0:
                        ocrconfigpath = m_ocrconfigpath_id;
                        break;
                    case 1:
                        ocrconfigpath = m_ocrconfigpath_jz;
                        break;
                    case 2:
                        ocrconfigpath = m_ocrconfigpath_hz;
                        break;

                    default:
                        break;
                }
                ret = PaperOCR(m_ocrdatapath, ocrconfigpath, SourceImg, m_sbresult, HeadImg, ScanType, 8192);
            }
            catch(Exception E)
            {
                //this.textBox1.Text = "识别异常！";
            }

            if (ret == 1)
            {
                Json = m_sbresult.ToString();
            }
            else
            {
                //this.textBox1.Text = "识别异常！请登录下方的官网联系客服！";
            }
            return Json;
        }
    }
}
