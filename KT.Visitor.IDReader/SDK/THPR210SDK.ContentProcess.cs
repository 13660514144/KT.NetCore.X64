using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.IdReader.SDK
{
    public partial class THPR210SDK
    {
        /// <summary>
        /// 获取证件的字段
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void GetContentByCardType(int cardType, string key, string val)
        {
            //居民身份证 - 照片页
            if (cardType == 2)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 出生
                //5 住址
                //6 公民身份号码
            }
            //居民身份证 - 签发机关页
            else if (cardType == 3)
            {
                //0 保留
                //1 签发机关
                //2 有效期限(含签发日期和有效期)
                //3 签发日期
                //4 有效期至
            }
            //临时居民身份证
            else if (cardType == 4)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 出生
                //5 住址
                //6 公民身份号码
                //7 签发机关
                //8 有效期限
                //9 签发日期
                //10 有效期至
            }
            //机动车驾驶证 说明：驾照（一）：已过期，没有添加模板
            else if (cardType == 5)
            {
                if (key == "证号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 证号
                //2 姓名
                //3 性别
                //4 住址
                //5 出生日期
                //6 初始领证日期
                //7 准驾车型
                //8 有效起始日期
                //9 有效期限
                //10 有效截止日期
            }
            //机动车行驶证
            else if (cardType == 6)
            {
                if (key == "号牌号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "所有人")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 号牌号码
                //2 车辆类型
                //3 所有人
                //4 住址
                //5 品牌型号
                //6 车辆识别代号
                //7 发动机号码
                //8 注册日期
                //9 发证日期
                //10 使用性质
            }
            //军官证 1998 版
            else if (cardType == 7)
            {
                if (key == "编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 编号
                //2 姓名
                //3 出生年月
                //4 性别
                //5 籍贯
                //6 民族
                //7 部别
                //8 职务
                //9 衔级
                //10 发证机关
                //11 发证日期
                //12 有效期至
            }
            //士兵证 1998 版
            else if (cardType == 8)
            {
                if (key == "编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 籍贯
                //5 入伍年月
                //6 年龄
                //7 部别
                //8 编号
                //9 发证机关
                //10 发证日期
            }
            //往来港澳通行证 2005版
            else if (cardType == 9)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 类型标识，出现在机读码内的类型 “W”
                //1 证件号码MRZ（机读码导出）
                //2 中文姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 有效期至
                //7 签发国代码
                //8 英文姓
                //9 英文名
                //10 MRZ1
                //11 MRZ2
                //12 持证人国籍代码
                //13 证件号码（直接识别）
                //14 出生地
                //15 签发地点
                //16 签发日期
            }
            //台湾居民往来大陆通行证 1992 版 - 照片页
            else if (cardType == 10)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 证件类型，出现在机读码内的类型 “T”
                //1 证件号码MRZ（机读码导出）
                //2 中文姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 有效期至
                //7 签发国代码
                //8 英文姓
                //9 英文名
                //10 MRZ1
                //11 MRZ2
                //12 持证人国籍代码
                //13 证件号码
                //14 身份证号码
                //15 签发日期
                //16 签发次数(MRZ)
                //17 现住址
                //18 职业
                //19 签发次数(VIZ)
                //20 签发地代码
            }
            //大陆居民往来台湾通行证 1992 版 - 照片页
            else if (cardType == 11)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 类型标识，出现在机读码内的类型，“T”
                //1 证件号码MRZ（MRZ 导出）
                //2 中文姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 有效期至
                //7 签发国代码
                //8 英文姓
                //9 英文名
                //10 MRZ1
                //11 MRZ2
                //12 持证人国籍代码
                //13 证件号码
                //14 身份证号码
                //15 签发日期
                //16 签发次数
                //17 现住址
                //18 职业
            }
            //签证
            else if (cardType == 12)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "本国姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 签证类型， 出现在机读码内的类型
                //1 机读码导出号码（MRZ导出） 
                //2 本国姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 有效期至
                //7 签发国代码
                //8 英文姓
                //9 英文名
                //10 MRZ1
                //11 MRZ2
                //12 持证人国籍代码
                //13 证件号码
                //14 护照号码/通行证号码（直接识别） 
                //15 签发地点
                //16 签发日期
                //17 备注
                //18 来往次数
                //19 居留事由
                //20 出访日期
                //21 离境日期
                //22 停留天数
                //23 签证种类
            }
            //护照
            else if (cardType == 13)
            {
                if (key == "护照号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "本国姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 护照类型（出现在机读码 内的类型）
                //1 护照号码MRZ（MRZ 导出）
                //2 本国姓名（版面识别） 
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 有效期至
                //7 签发国代码
                //8 英文姓
                //9 英文名
                //10 MRZ1
                //11 MRZ2
                //12 持证人国籍代码
                //13 护照号码（直接识别） 
                //14 出生地点（仅限中国护照） 
                //15 签发地点（仅限中国护照） 
                //16 签发日期（仅限中国护照） 
                //17 RFID MRZ
                //18 OCR MRZ
                //19 出生地点拼音（仅限中国护照） 
                //20 签发地点拼音（仅限中国护照） 
                //21 身份号码（仅限台湾及韩国护照） 
                //22 本国姓名拼音OCR
                //23 性别OCR
                //24 持证人国籍代码OCR
                //25 身份证号码OCR
                //26 出生日期OCR
                //27 有效期至OCR
                //28 签发机关OCR
                //29 本国姓
                //30 本国名
                //注意事项 护照号码建议从索引 1 获取，即护照号码 MRZ（MRZ 导出）
            }
            //港澳居民来往内地通行证 - 照片页
            else if (cardType == 14)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 证件号码
                //2 中文姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 本证有效期至
                //7 英文姓
                //8 英文名
                //9 港澳证件号码
                //10 签发日期
                //11 有效期限
                //12 签发机关
                //13 换证次数
                //14 其它姓名
            }
            //港澳居民来往内地通行证 - 机读码页
            else if (cardType == 15)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 证件类型，出现在机读码内的类型，为“C”
                //1 证件号码
                //2 中文姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 本证有效期至
                //7 英文姓
                //8 英文名
                //9 MRZ1
                //10 MRZ2
                //11 MRZ3
                //12 签发国代码
                //13 身份证件号码
                //14 换证次数
            }
            //常住人口登记卡
            else if (cardType == 16)
            {
                if (key == "公民身份证件编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 出生日期
                //5 公民身份证件编号
            }
            //海员证 2009 版 - 照片页
            else if (cardType == 17)
            {
                if (key == "护照号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "本国姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 证件类型
                //1 护照号码MRZ
                //2 本国姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 有效期至
                //7 签发国代码
                //8 英文姓
                //9 英文名
                //10 MRZ1
                //11 MRZ2
                //12 持证人国籍代码
                //13 护照号码
                //14 出生地点
                //15 签发机关
                //16 签发日期
            }
            //军官证 1998 版 - 照片页
            else if (cardType == 18)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 编号
                //2 发证机关
                //3 发证时间
                //4 有效期至
            }
            //军官证 1998 版 - 信息页
            else if (cardType == 19)
            {
                if (key == "衔级")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 出生年月
                //3 性别
                //4 籍贯
                //5 民族
                //6 部别
                //7 职务
                //8 衔级
            }
            //警官证 2006 版 - 照片页
            else if (cardType == 20)
            {
                if (key == "警员证号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 率属公安局
                //3 警员证号
            }
            //警官证 2006 版 - 信息页
            else if (cardType == 21)
            {
                if (key == "警衔")
                {
                    Result.IDCode = val;
                }
                if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 血型
                //4 出生日期
                //5 职务
                //6 警衔
                //7 有效期限
            }
            //往来港澳通行证 2014版 - 照片页
            else if (cardType == 22)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 证件号码
                //2 中文姓名
                //3 英文姓名
                //4 出生日期
                //5 性别
                //6 有效期限
                //7 签发地点
                //8 MRZ1
                //9 MRZ2
                //10 MRZ3
                //11 签发日期
                //12 有效期至
            }
            //边境地区出入境通行证 2014 版 - 照片页
            else if (cardType == 23)
            {
                if (key == "身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "本国姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 本国姓名
                //2 英文姓名
                //3 性别
                //4 出生日期
                //5 身份证号码
                //6 职业
                //7 签发日期
                //8 有效期至
                //9 地址
                //10 MRZ1
                //11 MRZ2
                //12 MRZ证件类型
                //13 MRZ签发国代码
                //14 MRZ英文姓名
                //15 MRZ证件号码
                //16 MRZ持证人国籍代码
                //17 MRZ出生日期
                //18 MRZ性别
                //19 MRZ有效期至
            }
            //中国人民解放军车辆驾驶证 2010 版
            else if (cardType == 24)
            {
                if (key == "证号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 证号
                //4 血型
                //5 出生日期
                //6 部别
                //7 准驾车型
                //8 初次领证日期
                //9 核发日期
                //10 有效期至
            }
            //台湾居民来往大陆通行证 2015 版 - 照片页
            else if (cardType == 25)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 中文姓名
                //2 英文姓名
                //3 出生日期
                //4 性别
                //5 有效期限
                //6 签发地点
                //7 证件号码
                //8 签发次数
                //9 签发机关
            }
            //台湾居民往来大陆通行证 2015 版 - 机读码页
            else if (cardType == 26)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 证件类型
                //1 证件号码
                //2 中文姓名
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 本证有效期至
                //7 英文姓
                //8 英文名
                //9 MRZ1
                //10 MRZ2
                //11 MRZ3
                //12 身份证件号码
                //13 换证次数
            }
            //中国人民解放军行车执照 2012 版
            else if (cardType == 27)
            {
                if (key == "车牌号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "车属单位")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 车属单位
                //2 车牌号码
                //3 厂牌型号
                //4 车体颜色
                //5 发动机型号
                //6 车架型号
                //7 出厂日期
            }
            //往来台湾通行证 2017版 - 照片页
            else if (cardType == 29)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 证件号码
                //2 中文姓名
                //3 英文姓名
                //4 出生日期
                //5 性别
                //6 有效期限
                //7 签发地点
                //8 MRZ1
                //9 MRZ2
                //10 MRZ3
                //11 签发机关
            }
            //机动车行驶证副页
            else if (cardType == 30)
            {
                if (key == "档案编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "号牌号码")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 号牌号码
                //2 档案编号
                //3 行驶证识别代码
            }
            //港澳台居民居住证 - 照片页
            else if (cardType == 31)
            {
                if (key == "公民身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 出生
                //4 住址
                //5 公民身份证号码
            }
            //港澳台居民居住证 - 签发机关页
            else if (cardType == 32)
            {
                if (key == "通行证号码")
                {
                    Result.IDCode = val;
                }

                //0 保留
                //1 签发机关
                //2 有效期限
                //3 签发日期
                //4 有效期至
                //5 通行证号码
            }
            //外国人永久居留身份证 2017 版 - 照片页
            else if (cardType == 33)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 英文姓名
                //2 中文姓名
                //3 性别
                //4 出生
                //5 国籍
                //6 签发机关
                //7 英文签发机关
                //8 公民身份号码
                //9 有效期限
                //10 英文姓
                //11 英文名
            }
            //台湾地区（金马澎）入出境许可证 2015 版 - 照片页
            else if (cardType == 34)
            {
                if (key == "身份证号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 身份证号
                //2 姓名
                //3 出生
                //4 有效日期
                //5 在台地址
                //6 许可证号
                //7 核发日期
                //8 性别
                //9 出生地
            }
            //居住证（广东、广西、东莞）-照片页
            else if (cardType == 1000)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 出生
                //5 住址
                //6 公民身份号码
                //7 签发日期
                //8 有效期
                //9 证号
                //10 服务处所
                //11 国家或地区
                //12 户籍所在地
            }
            //香港居民身份证 - 照片页
            else if (cardType == 1001)
            {
                if (key == "身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 中文姓名
                //2 拼音姓名
                //3 性别
                //4 出生日期
                //5 签发日期
                //6 身份证号码
                //7 符号标记
                //8 中文电码
                //9 电码译文
                //10 电码矫正姓名
            }
            //登机牌（拍照设备目前不支持登机牌的识别）
            else if (cardType == 1002)
            {
                if (key == "航班")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 航班
                //3 到达站
                //4 日期
                //5 座位号
            }
            //边境地区出入境通行证 2005 版 - 照片页
            else if (cardType == 1003)
            {
                if (key == "身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 证件号码
                //2 姓名
                //3 性别
                //4 出生日期
                //5 身份证号码
                //6 MRZ1
                //7 MRZ2
            }
            //边境地区出入境通行证 2005 版 - 信息页
            else if (cardType == 1004)
            {
                if (key == "身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 出生日期
                //4 身份证号码
                //5 地址
            }
            //澳门居民身份证 - 照片页
            else if (cardType == 1005)
            {
                if (key == "身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "中文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 中文姓名
                //2 拼音姓名
                //3 性别
                //4 出生日期
                //5 签发日期
                //6 有效期至
                //7 身份证号码
                //8 首次发证
                //9 中文电码
                //10 电码译文
            }
            //领取凭证
            else if (cardType == 1006)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 受理号
                //2 姓名
                //3 公民身份号码
            }
            //律师执业证 - 签发机关页
            else if (cardType == 1007)
            {
                if (key == "执业证号")
                {
                    Result.IDCode = val;
                }
                else if (key == "执业机构")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 执业机构
                //2 执业证类别
                //3 执业证号
            }
            //律师执业证 - 照片页
            else if (cardType == 1008)
            {
                if (key == "身份证号")
                {
                    Result.IDCode = val;
                }
                else if (key == "持证人")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 持证人
                //2 性别
                //3 身份证号
            }
            //中华人民共和国道路运输证 IC 卡
            else if (cardType == 1009)
            {
                if (key == "道路运输证号")
                {
                    Result.IDCode = val;
                }
                else if (key == "业户名称")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 业户名称
                //2 车辆号牌
                //3 车辆类型
                //4 品牌型号
                //5 核发机关
                //6 道路运输证号
                //7 发证日期
            }
            //名片
            else if (cardType == 1010)
            {
                if (key == "手机")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 姓名
                //1 职务/部门
                //2 手机
                //3 公司
                //4 地址
                //5 电话
                //6 传真
                //7 电
                //8 网
                //9 邮
            }
            //组织机构代码证
            else if (cardType == 1011)
            {
                if (key == "登记号")
                {
                    Result.IDCode = val;
                }
                else if (key == "机构名称")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 代码
                //2 机构名称
                //3 机构类型
                //4 地址
                //5 有效期
                //6 颁发单位
                //7 登记号
            }
            //深圳经济特区居住证 - 2015 版 - 照片页
            else if (cardType == 1013)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 签发日期
                //5 住址
                //6 公民身份号码
            }
            //内蒙古自治区人民法院工作证
            else if (cardType == 1018)
            {
                if (key == "编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 职务
                //3 编号
                //4 签发日期
                //5 有效期
            }
            //内蒙古自治区检察机关工作证
            else if (cardType == 1019)
            {
                if (key == "编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 民族
                //3 出生年月
                //4 职务
                //5 单位
                //6 编号
                //7 有效期至
            }
            //社会保障卡（北京、重庆）-照片页
            else if (cardType == 1021)
            {
                if (key == "社会保障号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 出生日期
                //5 社会保障号码
                //6 卡号
                //7 发卡日期
                //8 有效期限
                //9 银行卡号
            }
            //海船船员健康证书 - 照片页
            else if (cardType == 1022)
            {
                if (key == "证件编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "持证人姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 持证人姓名
                //2 持证人姓名拼音
                //3 国籍
                //4 出生日期
                //5 性别
                //6 部门
                //7 证件编号
                //8 有效期至
                //9 签发日期
                //10 印刷号
            }
            //海船船员健康证书 - 签发机关页
            else if (cardType == 1023)
            {
                //0 保留
                //1 授权机关
                //2 主检医师签名
                //3 签发机关
            }
            //海船船员培训合格证书 - 照片页
            else if (cardType == 1024)
            {
                if (key == "证件编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "持证人姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 持证人姓名
                //2 持证人姓名拼音
                //3 国籍
                //4 出生日期
                //5 性别
                //6 证件编号
                //7 签发日期
                //8 合格证名称列
                //9 签发日期列
                //10 有效期至列
                //11 印刷号
            }
            //海船船员培训合格证书 - 签发机关页
            else if (cardType == 1025)
            {
                if (key == "正式授权官员的姓名")
                {
                    Result.Name = val;
                }
                //0 保留
                //1 正式授权官员的姓名
                //2 授权机关
                //3 合格证名称
                //4 签发日期
                //5 有效期至
            }
            //海船船员适任证书 - 照片页
            else if (cardType == 1026)
            {
                if (key == "证件编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "持证人姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 持证人姓名
                //2 持证人姓名拼音
                //3 国籍
                //4 出生日期
                //5 性别
                //6 证件编号
                //7 有效期至
                //8 签发日期
                //9 职能
                //10 级别
                //11 适用的限制
                //12 印刷号
            }
            //海船船员适任证书 - 签发机关页
            else if (cardType == 1027)
            {
                if (key == "正式授权官员的姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 正式授权官员的姓名
                //2 授权签发机关
                //3 等级与职务
                //4 适用的限制
            }
            //浙江省临时居住证 - 照片页
            else if (cardType == 1029)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 公民身份号码
                //5 现居住地址
            }
            //台湾全民健康保险卡
            else if (cardType == 1030)
            {
                if (key == "身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                //0 保留
                //1 姓名
                //2 身份号码
                //3 出生日期
                //4 卡号
            }
            //台湾地区身份证 - 照片页
            else if (cardType == 1031)
            {
                if (key == "统一编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 出生年月日
                //4 发证日期
                //5 统一编号
            }
            //台湾地区身份证 - 条码页
            else if (cardType == 1032)
            {
                if (key == "号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "父")
                {
                    Result.Name = $"{val} { Result.Name}";
                }
                else if (key == "母")
                {
                    Result.Name = $"{Result.Name} {val}";
                }
                else if (key == "役别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 父
                //2 母
                //3 配偶
                //4 役别
                //5 出生地
                //6 住址
                //7 号码
            }
            //English Name(仅导入识别)
            else if (cardType == 1035)
            {
                if (key == "English Name")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 English Name
            }
            //神煤集团工作证
            else if (cardType == 1037)
            {
                if (key == "编号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 部门
                //3 编号
            }
            //厦门市社会保障卡 - 照片页
            else if (cardType == 1039)
            {
                if (key == "保险号")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 卡号
                //4 保险号
                //5 身份证
            }
            //台湾地区驾驶证
            else if (cardType == 1040)
            {
                if (key == "驾照号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 出生日期
                //4 发证日期
                //5 驾照号码
                //6 住址
                //7 有效日期
                //8 管辖编号
                //9 驾照种类
            }
            //马来西亚身份证 - 照片页
            else if (cardType == 2001)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 公民身份号码
                //2 姓名
                //3 性别
                //4 出生日期
                //5 国籍
                //6 住址
            }
            //美国加利福利亚驾驶证
            else if (cardType == 2002)
            {
                if (key == "驾照号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓")
                {
                    Result.Name = $"{val} { Result.Name}";
                }
                else if (key == "名")
                {
                    Result.Name = $"{Result.Name} {val}";
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 驾照号码
                //2 姓
                //3 名
                //4 性别
            }
            //新西兰驾驶证
            else if (cardType == 2003)
            {
                if (key == "驾驶证ID")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓")
                {
                    Result.Name = $"{val} { Result.Name}";
                }
                else if (key == "名")
                {
                    Result.Name = $"{Result.Name} {val}";
                }

                //0 保留
                //1 姓
                //2 名
                //3 出生日期
                //4 签发日期
                //5 驾驶证ID
                //6 Expiry Date
            }
            //新加坡身份证
            else if (cardType == 2004)
            {
                if (key == "身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 民族
                //4 出生日期
                //5 出生国
                //6 身份证号码
            }

            //TD - 2 型机读旅行证件
            else if (cardType == 2006)
            {
                if (key == "MRZ1")
                {
                    Result.IDCode = val;
                }
                else if (key == "MRZ2")
                {
                    Result.Name = val;
                }

                //0 Reserve
                //1 MRZ1
                //2 MRZ2
            }
            //TD - 1 型机读旅行证件
            else if (cardType == 2009)
            {
                if (key == "身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "英文姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 证件类型
                //1 身份号码
                //2 签发国代码
                //3 英文姓名
                //4 性别
                //5 出生日期
                //6 有效期至
                //7 英文姓
                //8 英文名
                //9 MRZ1
                //10 MRZ2
                //11 MRZ3
                //12 国家代码（本国）
                //13 身份号码（扩展）
            }
            //印度尼西亚居民身份证
            else if (cardType == 2010)
            {
                if (key == "身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 身份号码
                //2 姓名
                //3 出生日期
                //4 出生地
                //5 性别
                //6 地区
                //7 国籍
                //8 民族
                //9 血型
                //10 地址
            }
            //泰国国民身份证
            else if (cardType == 2011)
            {
                if (key == "身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓")
                {
                    Result.Name = $"{val} { Result.Name}";
                }
                else if (key == "名")
                {
                    Result.Name = $"{Result.Name} {val}";
                }

                //0 保留
                //1 名
                //2 姓
                //3 出生日期
                //4 签发日期
                //5 有效期至
                //6 身份号码
            }
            //泰国驾驶证
            else if (cardType == 2012)
            {
                if (key == "身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 驾照号号
                //3 出生日期
                //4 签发日期
                //5 有效期至
                //6 身份号码
            }
            //墨西哥选民证 - 照片页
            else if (cardType == 2013)
            {
                if (key == "身份唯一标识")
                {
                    Result.IDCode = val;
                }
                else if (key == "名字")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 父姓
                //2 母姓
                //3 名字
                //4 街道和号码
                //5 殖民地和邮编
                //6 城市和州
                //7 选民代码
                //8 身份唯一标识
                //9 性别
                //10 年龄
                //11 出生日期
            }
            //墨西哥选民证背面ABC
            else if (cardType == 2014)
            {
                if (key == "身份唯一标识")
                {
                    Result.IDCode = val;
                }

                //0 保留
                //1 身份唯一标识
            }
            //瑞典驾驶证 
            else if (cardType == 2020)
            {
                if (key == "证件号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓")
                {
                    Result.Name = $"{val} { Result.Name}";
                }
                else if (key == "名")
                {
                    Result.Name = $"{Result.Name} {val}";
                }

                //0 保留
                //1 名
                //2 姓
                //3 出生日期
                //4 签发日期
                //5 有效期至
                //6 证件号码
                //7 社保卡号
            }
            //马来西亚驾照
            else if (cardType == 2021)
            {
                if (key == "公民身份号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 民族
                //3 公民身份号码
                //4 证件类型
                //5 有效期至
                //6 住址
            }
            //新加坡驾驶证
            else if (cardType == 2031)
            {
                if (key == "驾照号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 预留
                //1 驾照号码
                //2 姓名
                //3 出生日期
                //4 签发日期
                //5 有效期至
            }
            //印度尼西亚驾驶证
            else if (cardType == 2041)
            {
                if (key == "身份证号码")
                {
                    Result.IDCode = val;
                }
                else if (key == "姓名")
                {
                    Result.Name = val;
                }
                else if (key == "性别")
                {
                    Result.Gender = val;
                }

                //0 保留
                //1 姓名
                //2 性别
                //3 地址
                //4 城市
                //5 出生日期
                //6 身高
                //7 工作
                //8 身份证号码
                //9 有效期至
            }
            //日本驾照
            else if (cardType == 2051)
            {
                if (key == "姓名")
                {
                    Result.Name = val;
                }

                //0 保留
                //1 姓名
                //2 出生日期
                //3 住所
                //4 交付
                //5 有效期至
                //6 免许条件等
            }
        }
    }
}
