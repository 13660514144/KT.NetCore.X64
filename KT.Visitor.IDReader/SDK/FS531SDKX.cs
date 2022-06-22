using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using KT.Common.WpfApp.Helpers;
using log4net;
using Microsoft.Extensions.Logging;
using System.IO;

namespace KT.Visitor.IdReader.SDK
{
    /// <summary>
    /// H531证件扫描仪SDK
    /// </summary>
    public class FS531SDKX
    {
        private static ILogger _logger;
        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = ContainerHelper.Resolve<ILogger>();
                }
                return _logger;
            }
        }

        // 初始化扫描仪 
        public static int H531_Init()
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_Init");
            var result = FS531SDKBaseX.findScanner();
            Logger.LogInformation($"H531证件扫描仪完成：H531_Init result:{result}");
            return result;
        }

        // 释放扫描仪 
        public static void H531_Free()
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_Free");
            FS531SDKBaseX.closeScanner();
            Logger.LogInformation($"H531证件扫描仪完成：H531_Free");
        }

        // 获取动态库版本号 
        /*public static int H531_getVersion(StringBuilder json)
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_getVersion");
            var result = FS531SDKBaseX.H531_getVersion(json); 
            Logger.LogInformation($"H531证件扫描仪完成：H531_getVersion result:{result}");
            return result;
        }*/

        // 扫描识别 
        public static int H531_ScanOCR(ScanOCRQueryX cfg)
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_ScanOCR");
            var result = FS531SDKBaseX.startScan(cfg.chResolution,cfg.chScanFace,cfg.chBackImagePath,
                cfg.chScanImagePath,cfg.sP1,cfg.sP2,cfg.iContrlType,cfg.iContrlType,cfg.iJPEG_QUALITY);
            Logger.LogInformation($"H531证件扫描仪完成：H531_ScanOCR result:{result}");
            return result;
        }

        // 校准扫描仪 
        public static int H531_CalibrateScanner()
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_CalibrateScanner");
            var result = FS531SDKBaseX.caliScanner();
            Logger.LogInformation($"H531证件扫描仪完成：H531_CalibrateScanner result:{result}");
            return result;
        }
        /*
        // 获取扫描仪按键 
        public static int H531_GetButtonDownType()
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_GetButtonDownType");
            var result = FS531SDKBaseX.H531_GetButtonDownType();
            Logger.LogInformation($"H531证件扫描仪完成：H531_GetButtonDownType result:{result}");
            return result;
        }*/
        /*
        // 回程 
        public static int H531_FeedBackScanner()
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_FeedBackScanner");
            var result = FS531SDKBaseX.H531_FeedBackScanner();
            Logger.LogInformation($"H531证件扫描仪完成：H531_FeedBackScanner result:{result}");
            return result;
        }*/
        /*
        // 获取错误代码含义 
        public static string H531_GetError(int code)
        {
            Logger.LogInformation("H531证件扫描仪执行：H531_GetError");
            var result = FS531SDKBaseX.H531_GetError(code);
            Logger.LogInformation($"H531证件扫描仪完成：H531_GetError result:{result}");
            return result;
        }*/
        /*
        // 获取错误代码含义
        public static int OcrCamera_OCR(int Type, string PhotoPath, string outHeaderPath, StringBuilder outData, int outDataLen)
        {
            Logger.LogInformation("H531证件扫描仪执行：OcrCamera_OCR");
            var result = FS531SDKBaseX.OcrCamera_OCR(Type, PhotoPath, outHeaderPath, outData, outDataLen);
            Logger.LogInformation($"H531证件扫描仪完成：OcrCamera_OCR result:{result}");
            return result;
        }*/
    }

    public class FS531SDKBaseX
    {
        #region FS531
        /*
        // 初始化扫描仪
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_Init", CharSet = CharSet.Ansi)]
        public static extern int H531_Init();

        // 释放扫描仪
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_Free", CharSet = CharSet.Ansi)]
        public static extern void H531_Free();

        // 获取动态库版本号
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_getVersion", CharSet = CharSet.Ansi)]
        public static extern int H531_getVersion(StringBuilder json);

        // 扫描识别
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_ScanOCR", CharSet = CharSet.Ansi)]
        public static extern int H531_ScanOCR(string configJson, StringBuilder json);

        // 校准扫描仪
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_CalibrateScanner", CharSet = CharSet.Ansi)]
        public static extern int H531_CalibrateScanner();

        // 获取扫描仪按键
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_GetButtonDownType", CharSet = CharSet.Ansi)]
        public static extern int H531_GetButtonDownType();

        // 回程
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_FeedBackScanner", CharSet = CharSet.Ansi)]
        public static extern int H531_FeedBackScanner();

        // 获取错误代码含义
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/H531.dll", EntryPoint = "H531_GetError", CharSet = CharSet.Ansi)]
        public static extern string H531_GetError(int code);

        // 图片扫描
        [DllImport("ReferenceFiles/IdReaderSdks/FS531/hOcrCamera.dll", EntryPoint = "OcrCamera_OCR", CharSet = CharSet.Ansi)]
        public static extern int OcrCamera_OCR(int Type, string PhotoPath, string outHeaderPath, StringBuilder outData, int outDataLen);
        */
        #endregion

        #region FS531X
        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/H531.dll", EntryPoint = "findScanner", CharSet = CharSet.Ansi)]
        public static extern int findScanner();
        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/H531.dll", EntryPoint = "findScanner", CharSet = CharSet.Ansi)]
        public static extern int closeScanner();
        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/H531.dll", EntryPoint = "findScanner", CharSet = CharSet.Ansi)]
        public static extern int caliScanner();
        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/H531.dll", EntryPoint = "findScanner", CharSet = CharSet.Ansi)]
        public static extern int startScan(string chResolution,
                                            string chScanFace,
                                            string chBackImagePath,
                                            string chScanImagePath,
                                            string sP1,
                                            string sP2,
                                            int lCardType,
                                            int lContrlType,
                                            int lJPEG_QUALITY
                                            );

        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/H531.dll", EntryPoint = "findScanner", CharSet = CharSet.Ansi)]
        public static extern int resetScanner();

        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/H531.dll", EntryPoint = "findScanner", CharSet = CharSet.Ansi)]
        public static extern int discernImage(string pchFilePath,
                                             string HeadImgFName,
                                            byte[] rcgCard,
                                            int lCardType,
                                            int lJPEG_QUALITY,
                                            string ocrdata,
                                            string cfgPath,
                                            StringBuilder outData
                                            );

        [DllImport("ReferenceFiles/IdReaderSdks/FS531X/H531.dll", EntryPoint = "findScanner", CharSet = CharSet.Ansi)]
        public static extern int discernImage1(string pchFilePath,
                                                 string HeadImgFName,
                                                int lCardType,
                                                int lJPEG_QUALITY,
                                                string ocrdata,
                                                StringBuilder outData
                                                );
        #endregion
    }

    /// <summary>
    /// 直接扫描人员信息数据模型
    /// </summary>
    public class FS351PersonX
    {
        public string Name { get; set; }
        public string SurnameCH { get; set; }
        public string nameCH { get; set; }
        public string Sex { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string People { get; set; }
        public string Signdate { get; set; }
        public string Validterm { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Organs { get; set; }
        public string SurnameEN { get; set; }
        public string NameEN { get; set; }
        public string ENfullname { get; set; }
        public string Nationality { get; set; }
        public string ID { get; set; }
        public string Leavetime { get; set; }
        public string PlaceCH { get; set; }
        public string PlaceEN { get; set; }
        public string BirthplaceCH { get; set; }
        public string BirthplaceEN { get; set; }
        public string CodeOne { get; set; }
        public string CodeTwo { get; set; }
        public string CodeThree { get; set; }
        public string PermitnumberNumber { get; set; }
        public string Vocational { get; set; }
        public string DocumentsCategory { get; set; }
        public string Other { get; set; }
        public string SignDep { get; set; }
        public string ICSerial { get; set; }
    };

    /// <summary>
    /// 护照图片扫描人员信息数据结构模型
    /// </summary>
    public class ScanPassportPersonX
    {
        //"Name":"CHEN ZHIMIN",                                                                                                
        public string Name { get; set; }
        //"NameCh":"陈志敏",                                                                                                   
        public string NameCh { get; set; }
        //"EnFir":"CHEN",                                                                                                      
        public string EnFir { get; set; }
        //"EnSen":"ZHIMIN",                                                                                                    
        public string EnSen { get; set; }
        //"NameFir":"陈",                                                                                                      
        public string NameFir { get; set; }
        //"NameSen":"志敏",                                                                                                    
        public string NameSen { get; set; }
        //"CardNo":"EC8053756",                                                                                                
        public string CardNo { get; set; }
        //"Sex":"F",                                                                                                           
        public string Sex { get; set; }
        //"SexCH":"女",                                                                                                        
        public string SexCH { get; set; }
        //"Birthday":"19981030",                                                                                               
        public string Birthday { get; set; }
        //"Address":"HEILONGJIANG",                                                                                            
        public string Address { get; set; }
        //"AddressCH":"黑龙江",                                                                                                
        public string AddressCH { get; set; }
        //"IssueDate":"20180328",                                                                                              
        public string IssueDate { get; set; }
        //"ValidPeriod":"20280327",                                                                                            
        public string ValidPeriod { get; set; }
        //"Nation":"CHN",                                                                                                      
        public string Nation { get; set; }
        //"IssueAddress":"GUANGDONG",                                                                                          
        public string IssueAddress { get; set; }
        //"IssueAddressCH":"广东",                                                                                             
        public string IssueAddressCH { get; set; }
        //"personalNo":"LDMCNGLOMDPE49",                                                                                       
        public string personalNo { get; set; }
        //"MRZ":"POCHNCHEN<<ZHIMIN<<<<<<<<<<<<<<<<<<<<<<<<<<<EC80537560CHN9810307F2803272LDMCNGLOMDPE4954",                    
        public string MRZ { get; set; }
        //"BIDC_MRZ1":"POCHNCHEN<<ZHIMIN<<<<<<<<<<<<<<<<<<<<<<<<<<<",                                                          
        public string BIDC_MRZ1 { get; set; }
        //"BIDC_MRZ2":"EC80537560CHN9810307F2803272LDMCNGLOMDPE4954"                                                           
        public string BIDC_MRZ2 { get; set; }
    }

    /// <summary>
    /// 驾照图片扫描人员信息数据结构模型
    /// </summary>
    public class ScanDriverLicenseX
    {
        // "Name": "陈钰贤",                               
        public string Name { get; set; }
        // "CardNo": "1441426199410200929",                
        public string CardNo { get; set; }
        // "Sex": "女",                                    
        public string Sex { get; set; }
        // "Birthday": "1994年10月20日",                   
        public string Birthday { get; set; }
        // "Address": "广东省平远县仁居镇居委圩镇",        
        public string Address { get; set; }
        // "IssueDate": "2016-07-13",                      
        public string IssueDate { get; set; }
        // "Nation": "中国",                               
        public string Nation { get; set; }
        // "drivingType": "C1",                            
        public string drivingType { get; set; }
        // "registerDate": "2016-07-13至2022-07-13"        
        public string registerDate { get; set; }
    }

    /// <summary>
    /// 直接扫描结果
    /// </summary>
    public class FS351ResultX
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public FS351PersonX Data { get; set; }
    }

    /// <summary>
    /// 直接扫描参数
    /// </summary>
    public class ScanOCRQueryX
    {
        [JsonProperty("goPath")]
        public string GoPath { get; set; }

        [JsonProperty("backPath")]
        public string BackPath { get; set; }

        /// <summary>
        /// 头像存储路径
        /// </summary>
        [JsonProperty("headerPath")]
        public string HeaderPath { get; set; }
        public string chResolution { get; set; }
        public string chScanFace { get; set; }
        public string chBackImagePath { get; set; }
        public string chScanImagePath { get; set; }
        public string sP1 { get; set; }
        public string sP2 { get; set; }
        public int iContrlType { get; set; }
        public int iJPEG_QUALITY { get; set; }
        public string OCRDATA { get; set; }
        /// <summary>
        /// 设备类型 4：身份证 3：护照 2：驾照
        /// </summary>
        [JsonProperty("CardType")]
        public int CardType { get; set; }

        public ScanOCRQueryX()
        {
            GoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "goPath.jpg");
            BackPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backPath.jpg");
            HeaderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "headerPath.jpg");
            chResolution = "3";
            chScanFace = "0";
            chBackImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackImage.jpg");
            chBackImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScanImage.jpg");
            iContrlType = 0;
            iJPEG_QUALITY = 15;
            OCRDATA = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReferenceFiles/IdReaderSdks/FS531X/ocr_data");
        }
    }
}
