using System;
using System.Collections.Generic;
using Chinese;

namespace KT.Common.Core.Utils
{
    public class PinyinUtil
    {
        public static NamePinyinModel GetNamePinyin(string name)
        {
            var namePinyin = new NamePinyinModel();

            if (string.IsNullOrEmpty(name))
            {
                return namePinyin;
            }
            else if (name.Length == 1)
            {
                namePinyin.FirstName = GetPinyin(name);
                namePinyin.LastName = namePinyin.LastName;
            }
            else if (name.Length == 2)
            {
                namePinyin.FirstName = GetPinyin(name.Substring(0, 1));
                namePinyin.LastName = GetPinyin(name.Substring(1));
            }
            else
            {
                if (name.IndexOf(" ") > 0)
                {
                    var index = name.IndexOf(" ");
                    namePinyin.FirstName = GetPinyin(name.Substring(0, index + 1));
                    namePinyin.LastName = GetPinyin(name.Substring(index + 1));
                }
                else if (name.IndexOf("　") > 0)
                {
                    var index = name.IndexOf("　");
                    namePinyin.FirstName = GetPinyin(name.Substring(0, index + 1));
                    namePinyin.LastName = GetPinyin(name.Substring(index + 1));
                }
                else if (name.Length == 4)
                {
                    namePinyin.FirstName = GetPinyin(name.Substring(0, 2));
                    namePinyin.LastName = GetPinyin(name.Substring(2));
                }
                else
                {
                    if (SurnameNames.Contains(name.Substring(0, 2)))
                    {
                        namePinyin.FirstName = GetPinyin(name.Substring(0, 2));
                        namePinyin.LastName = GetPinyin(name.Substring(2));
                    }
                    else
                    {
                        namePinyin.FirstName = GetPinyin(name.Substring(0, 1));
                        namePinyin.LastName = GetPinyin(name.Substring(1));
                    }
                }
            }

            namePinyin.LastName = namePinyin.LastName.Replace(" ", "");
            return namePinyin;
        }

        private static string GetPinyin(string value)
        {
            var pinyin = Pinyin.GetString(value, PinyinFormat.WithoutTone);
            return pinyin.Substring(0, 1).ToUpper() + pinyin.Substring(1);
        }

        private static List<string> SurnameNames = new List<string>() {
            "万俟","司马","上官","欧阳","夏侯","诸葛","闻人","东方","赫连","皇甫","尉迟","公羊",
            "澹台","公冶","宗政","濮阳","淳于","单于","太叔","申屠","公孙","仲孙","轩辕","令狐",
            "钟离","宇文","长孙","慕容","司徒","司空","鲜于","闾丘","元官","司寇","仇都","子车",
            "颛孙","瑞木","巫马","公西","漆雕","乐正","壤驷","公良","拓拔","夹谷","宰父","谷梁",
            "晋楚","闰法","汝鄢","涂钦","段干","百里","呼延","归海","羊舌","微生","梁丘","左丘",
            "东郭","南门","东门","西门","南宫","岳帅","侯亢","况后","有琴","商牟","余饵","伯赏",
            "墨哈","谯亘","年爱","阳佟","第五","言福",
            };
    }

    public class NamePinyinModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }




}
