﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TestTool.HikvisionIdReader.Views
{
    public partial class DSK5022SDK
    {
        public static Dictionary<string, string> NationalDictionary = new Dictionary<string, string>()
        {
            {"01","汉"},
            {"02","蒙古"},
            {"03","回"},
            {"04","藏"},
            {"05","维吾尔"},
            {"06","苗"},
            {"07","彝"},
            {"08","壮"},
            {"09","布依"},
            {"10","朝鲜"},
            {"11","满"},
            {"12","侗"},
            {"13","瑶"},
            {"14","白"},
            {"15","土家"},
            {"16","哈尼"},
            {"17","哈萨克"},
            {"18","傣"},
            {"19","黎"},
            {"20","傈僳"},
            {"21","佤"},
            {"22","畲"},
            {"23","高山"},
            {"24","拉祜"},
            {"25","水"},
            {"26","东乡"},
            {"27","纳西"},
            {"28","景颇"},
            {"29","柯尔克孜"},
            {"30","土"},
            {"31","达斡尔"},
            {"32","仫佬"},
            {"33","羌"},
            {"34","布朗"},
            {"35","撒拉"},
            {"36","毛南"},
            {"37","仡佬"},
            {"38","锡伯"},
            {"39","阿昌"},
            {"40","普米"},
            {"41","塔吉克"},
            {"42","怒"},
            {"43","乌孜别克"},
            {"44","俄罗斯"},
            {"45","鄂温克"},
            {"46","德昂"},
            {"47","保安"},
            {"48","裕固"},
            {"49","京"},
            {"50","塔塔尔"},
            {"51","独龙"},
            {"52","鄂伦春"},
            {"53","赫哲"},
            {"54","门巴"},
            {"55","珞巴"},
            {"56","基诺"},
        };

        public static string GetNationality(ref string Num)
        {
            return NationalDictionary[Num];
        }
    }
}
