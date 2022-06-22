using Chinese;
using KT.Common.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ////var json = JsonConvert.SerializeObject(new PassDisplayModel(), JsonUtil.JsonSettings);
            ////GuidTest();

            ////R992Test();
            //NamePinyinTest2();
            //NumberToStringPad0();
            //KoneMasks();
            //ElevatorMaps();
            //var bb = AppDomain.CurrentDomain.FriendlyName;
            //var aa = Convert.ToString(1, 2).TrimStart('0').PadLeft(2, '0');
            var cc = new CleanFileSettings();
            var dd = JsonConvert.SerializeObject(cc, JsonUtil.JsonPrintSettings);
        }

        private static void ElevatorMaps()
        {
            var result = string.Empty;
            for (int i = 1; i <= 12; i++)
            {

                result += $" {{                                   ";
                result += $"   \"BrandModel\": \"KONE - DCS\",       ";
                result += $"   \"Port\": \"2{i.ToString("#00")}\",                    ";
                result += $"   \"ModuleType\": \"RCGIF\"             ";
                result += $" }},                                  ";
                result += $" {{                                   ";
                result += $"   \"BrandModel\": \"KONE-DCS\",         ";
                result += $"   \"Port\": \"3{i.ToString("#00")}\",                    ";
                result += $"   \"ModuleType\": \"ELI\"               ";
                result += $" }},                                  ";
            }
            var bb = result;
        }

        //private static void KoneMasks()
        //{
        //    var ff = new KoneSettings();
        //    ff.Eli.ServerGroupMasks = new List<KoneServerGroupMaskSettings>();
        //    var koneServerGroupMaskSettings = new KoneServerGroupMaskSettings();
        //    koneServerGroupMaskSettings.DopGlobalDefaultAccessConnectedMaskFloors = new List<GlobalFloorSettings>();
        //    GlobalFloorSettings item = new GlobalFloorSettings();
        //    koneServerGroupMaskSettings.DopGlobalDefaultAccessConnectedMaskFloors.Add(item);
        //    koneServerGroupMaskSettings.DopGlobalDefaultAccessDisconnectedMaskFloors = new List<GlobalFloorSettings>();
        //    GlobalFloorSettings item1 = new GlobalFloorSettings();
        //    koneServerGroupMaskSettings.DopGlobalDefaultAccessDisconnectedMaskFloors.Add(item1);
        //    koneServerGroupMaskSettings.DopSpecificDefaultAccessMasks = new List<DopSpecificDefaultAccessMaskSettings>();
        //    DopSpecificDefaultAccessMaskSettings item2 = new DopSpecificDefaultAccessMaskSettings();
        //    item2.ConnectedMaskFloors = new List<SpecificFloorSettings>();
        //    item2.ConnectedMaskFloors.Add(new SpecificFloorSettings());
        //    item2.DisconnectedMaskFloors = new List<SpecificFloorSettings>();
        //    item2.DisconnectedMaskFloors.Add(new SpecificFloorSettings());
        //    koneServerGroupMaskSettings.DopSpecificDefaultAccessMasks.Add(item2);
        //    ff.Eli.ServerGroupMasks.Add(koneServerGroupMaskSettings);

        //    var gg = JsonConvert.SerializeObject(ff);

        //    //DOP 1 1 23 1 5
        //    //DOP 2 12 63 2 5
        //    //DOP 3 3 40
        //    //DOP 4 4 5

        //    var globalFloors = new List<int>() { 1, 2, 5, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 62, 63 };
        //    var globalFloorSettings = new List<GlobalFloorSettings>();
        //    foreach (var floor in globalFloors)
        //    {
        //        var globalFloorSetting = new GlobalFloorSettings();
        //        globalFloorSetting.Floor = floor;
        //        globalFloorSettings.Add(globalFloorSetting);
        //    }
        //    var aa = JsonConvert.SerializeObject(globalFloorSettings);

        //    var specificFloors = new List<int>() { 1, 2, 5, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 62, 63 };
        //    var specificFloorSettings = new List<SpecificFloorSettings>();
        //    foreach (var floor in specificFloors)
        //    {
        //        var specificFloorSetting = new SpecificFloorSettings();
        //        specificFloorSetting.Floor = floor;
        //        specificFloorSettings.Add(specificFloorSetting);
        //    }
        //    var bb = JsonConvert.SerializeObject(specificFloorSettings);
        //}

        private static void NumberToStringPad0()
        {
            var aa = string.Format("P_{0:000}", 20);

            var uri = new Uri("http://127.0.0.1:81/openapi/access/log/push");
            var bb = uri.AbsoluteUri;

            Console.ReadKey();
        }

        private static void NamePinyinTest2()
        {
            var names = PinyinUtil.GetNamePinyin("张三坏");
        }
        private static void NamePinyinTest()
        {
            var firstNames = new List<string>() {
            "万俟","司马","上官","欧阳","夏侯","诸葛","闻人","东方","赫连","皇甫","尉迟","公羊",
            "澹台","公冶","宗政","濮阳","淳于","单于","太叔","申屠","公孙","仲孙","轩辕","令狐",
            "钟离","宇文","长孙","慕容","司徒","司空","鲜于","闾丘","元官","司寇","仇都","子车",
            "颛孙","瑞木","巫马","公西","漆雕","乐正","壤驷","公良","拓拔","夹谷","宰父","谷梁",
            "晋楚","闰法","汝鄢","涂钦","段干","百里","呼延","归海","羊舌","微生","梁丘","左丘",
            "东郭","南门","东门","西门","南宫","岳帅","侯亢","况后","有琴","商牟","余饵","伯赏",
            "墨哈","谯亘","年爱","阳佟","第五","言福",
            };

            var firstNamePinyins = new List<string>();
            foreach (var item in firstNames)
            {
                firstNamePinyins.Add(Pinyin.GetString(item, PinyinFormat.WithoutTone));
            }

            var firstNamePinyinsString = firstNamePinyins.ToCommaString();
        }


        private static void R992Test()
        {
            //var bytes = new byte[13] { 168, 0, 0, 0, 0, 5, 0, 13, 90, 177, 226, 1, 169 };
            //var analyze = new QiacsR992CardDeviceAnalyze();
            //ILoggerFactory AppLoggerFactory =
            //LoggerFactory.Create(buliidder =>
            //{
            //    buliidder.AddLog4Net();
            //});

            ////_logger = new Log4gHelper<App>();
            //var logger = AppLoggerFactory.CreateLogger("Elevator Log");

            //var result = analyze.Analyze(logger, "COM3", bytes);

            //Console.ReadKey();
        }

        private static void GuidTest()
        {
            var id = IdUtil.NewId();
            var bytes = Encoding.UTF8.GetBytes(id);

            var id2 = Guid.NewGuid().ToString();
            var bytes2 = Encoding.UTF8.GetBytes(id2);

            //ToSourceFloors(new List<byte>() {  4, 3 });


            Console.WriteLine($"id:{id} bytes:{bytes.ToCommaPrintString()} length:{bytes.Length} ");
            Console.WriteLine($"id:{id2} bytes:{bytes2.ToCommaPrintString()} length:{bytes2.Length} ");
        }

        /// <summary>
        /// 获取真实楼层数据
        /// </summary>
        /// <param name="realFloors"></param>
        /// <returns></returns>
        public static List<byte> ToSourceFloors(List<byte> realFloors)
        {
            Console.WriteLine($"楼层设置开始：{DateTime.Now} ");
            if (realFloors == null)
            {
                return new List<byte>();
            }

            ////64 * 8 = 512
            //string[,] sourceFloorsBits = new string[64, 8];

            //foreach (var item in realFloors)
            //{
            //    var x = item / 4;
            //    var y = ((item % 4) - 1) * 2;
            //}

            //取字符位
            var sourceFloorsBits = string.Empty;
            for (int i = 1; i <= 255; i++)
            {
                if (realFloors.Contains((byte)i))
                {
                    sourceFloorsBits += "10";
                }
                else
                {
                    sourceFloorsBits += "00";
                }
                Console.WriteLine($"index：{i} ");
            }

            //补位够byte[64]
            sourceFloorsBits += "00";

            var sourceFloorsBytes = new List<byte>();
            var byteLength = sourceFloorsBits.Length / 8;
            for (int i = 0; i < byteLength; i++)
            {
                //获取8bit为1byte
                var byteBit = sourceFloorsBits.Substring(i * 8, 8);
                //楼层倒转
                byteBit = byteBit.Reverse();
                //转换为byte
                var realFloorByte = Convert.ToByte(byteBit, 2);

                sourceFloorsBytes.Add(realFloorByte);
            }

            Console.WriteLine($"楼层设置结束：{DateTime.Now} ");
            Console.WriteLine($"楼层设置结束：{sourceFloorsBytes.ToCommaPrintString()} ");
            return sourceFloorsBytes;
        }

        public static void ByteTest()
        {
            //Console.WriteLine("Hello World!");

            //var aa = uint.MaxValue;
            //var bb = int.MaxValue;

            //var intBytes = new byte[4] { 205, 215, 241, 231 };
            //var intValue = BitConverter.ToInt32(intBytes);
            //Console.WriteLine($"intValue:{intValue}");
            //var uintValue = BitConverter.ToUInt32(intBytes);
            //Console.WriteLine($"uintValue:{uintValue}");

            //var @byte = Convert.ToByte('1');
            //var @intbyte = (byte)Convert.ToInt32('1');
            //var @asciibyte = BitConverter.GetBytes('1');
            //Console.WriteLine($"byte:{@byte} int byte:{@intbyte} ascii byte:{@asciibyte.ToCommaPrintString()} ");
            //var @stringbyte = Convert.ToByte('1'.ToString());
            //var @stringintbyte = (byte)Convert.ToInt32('1'.ToString());
            //var @stringasciibyte = Encoding.ASCII.GetBytes('1'.ToString());
            //Console.WriteLine($"byte:{@stringbyte} int byte:{@stringintbyte} ascii byte:{@stringasciibyte.ToCommaPrintString()} ");

            //var bytes = new byte[] { 2, 1, 1, 9, 0, 210, 203, 229, 13, 155, 255, 143, 95, 5, 48, 155 };

            //var ff = BitConverter.ToString(bytes).Replace("-", " ");


            //Console.WriteLine(aa);

            //Console.ReadKey();

            ////var value = Encoding.ASCII.GetBytes("**");



            ////var cc = string.Empty;
        }
    }



    public class CleanFileSettings
    {
        public CleanFileSettings()
        {
            FileSettingses = new List<FileSettings>() {
                new FileSettings ()
                {
                    DirectoryUrl = "records/images",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "Files/Images/Portraits",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "Files/Images/Certificates",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "logs",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "../logs",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "uploads/hikvision/faces",
                    DaysAgo = 30
                },
            };
        }

        /// <summary>
        /// 间隔时间（分钟）
        /// </summary>
        public decimal ExceuteIntervalMinuteTime { get; set; } = 10;

        /// <summary>
        /// 删除的文件
        /// </summary>
        public List<FileSettings> FileSettingses { get; set; }
    }

    public class FileSettings
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string DirectoryUrl { get; set; }

        /// <summary>
        /// ？天前的文件
        /// </summary>
        public double DaysAgo { get; set; }
    }
}
