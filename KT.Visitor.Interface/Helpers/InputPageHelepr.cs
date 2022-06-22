using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Helpers
{
    /// <summary>
    /// 操作页面
    /// </summary>
    public class InputPageHelepr
    {
        /// <summary>
        /// 当前输入页面
        /// </summary>
        public static string CurrentInputName { get; set; } = VISTIOR_SELECT_COMPANY;

        /// <summary>
        /// 选择公司
        /// </summary>
        public static string VISTIOR_SELECT_COMPANY => "VISTIOR_SELECT_COMPANY";

        /// <summary>
        /// 访客记录
        /// </summary>
        public static string VISITOR_RECORD => "VISITOR_RECORD";

        /// <summary>
        /// 黑名单列表
        /// </summary>
        public static string BLACKLIST => "BLACKLIST";

        /// <summary>
        /// 编辑黑名单
        /// </summary>
        public static string EDIT_BLACKLIST => "EDIT_BLACKLIST";

        /// <summary>
        /// 访客详情
        /// </summary>
        public static string VISITOR_DETAIL => "VISITOR_DETAIL";

        /// <summary>
        /// 访客登记
        /// </summary>
        public static string VISITOR_INPUT => "VISITOR_INPUT";

        /// <summary>
        /// 人脸陪同访客输入
        /// </summary>
        public static string PHOTO_ACCOMPANY_INPUT => "ACCOMPANY_PHOTO_INPUT";

        /// <summary>
        /// 卡号陪同访客输入
        /// </summary>
        public static string CARD_ACCOMPANY_INPUT => "CARD_PHOTO_INPUT";

        /// <summary>
        /// 身份验证
        /// </summary>
        public static string IDENTITY_CHECK => "IDENTITY_CHECK";

        /// <summary>
        /// 身份验证
        /// </summary>
        public static string IDENTITY_ACTIVE => "IDENTITY_ACTIVE";

        /// <summary>
        /// 邀约验证
        /// </summary>
        public static string INVITE_CHECK => "INVITE_CHECK";

        /// <summary>
        /// 邀约验证
        /// </summary>
        public static string INVITE_ACTIVE => "INVITE_ACTIVE";

        /// <summary>
        /// 访客导入详情
        /// </summary>
        public static string VISITOR_IMPORT_DETAIL => "VISITOR_IMPORT_DETAIL";

        /// <summary>
        /// 访客导入详情
        /// </summary>
        public static string VISITOR_IMPORT => "VISITOR_IMPORT";

        public static string CONTROL_DOOR=> "CONTROL_DOOR";
        public static string MESSAGE => "MESSAGE";

        public static bool IsPageInput(string name)
        {
            if (name == CurrentInputName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
