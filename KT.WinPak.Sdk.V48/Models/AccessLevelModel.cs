using KT.WinPak.SDK.V48.Entities;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Models
{
    /// <summary>
    /// 访问级别
    /// </summary>
    public class AccessLevelModel
    {
        private List<string> _subAccountNames;

        /// <summary>
        /// 修改门禁时使用
        /// </summary>
        public string AccessLevelOldName { get; set; }

        #region BaseField
        /// <summary>
        /// 主键ID
        /// </summary>
        public int AccessLevelID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string AccessLevelName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string AccessLevelDesc { get; set; }
        /// <summary>
        /// 操作账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 操作账号
        /// </summary>
        public List<string> SubAccountNames
        {
            get => _subAccountNames ?? new List<string>();
            set => _subAccountNames = value ?? new List<string>();
        }
        #endregion


        public static AccessLevelClass ToEntity(AccessLevelClass entity, AccessLevelModel model)
        {
            var subAccountNames = model.SubAccountNames.ToArray();

            entity.AccessLevelName = model.AccessLevelName;
            entity.AccessLevelDesc = model.AccessLevelDesc;
            entity.AccessLevelID = model.AccessLevelID;
            entity.AccountName = model.AccountName;
            entity.SubAccountNames = subAccountNames;

            return entity;
        }


        public static AccessLevelModel FromEntity(IAccessLevel entity)
        {
            if (entity == null)
            {
                return null;
            }

            AccessLevelModel model = new AccessLevelModel();
            model.AccessLevelName = entity.AccessLevelName;
            model.AccessLevelDesc = entity.AccessLevelDesc;
            model.AccessLevelID = entity.AccessLevelID;
            model.AccountName = entity.AccountName;
            model.SubAccountNames = ((string[])entity.SubAccountNames).ToList();

            return model;
        }


        internal static List<AccessLevelModel> FromSqlEntities(List<AccessLevelPlu> entities)
        {
            var models = new List<AccessLevelModel>();
            if (entities == null)
            {
                return models;
            }
            foreach (var item in entities)
            {
                var model = FromSqlEntity(item);
                models.Add(model);
            }
            return models;
        }

        private static AccessLevelModel FromSqlEntity(AccessLevelPlu entity)
        {
            var model = new AccessLevelModel();
            model.AccessLevelID = entity.RecordId;
            model.AccessLevelName = entity.Name;
            model.AccessLevelDesc = entity.Description;
            model.AccountName = "";
            model.SubAccountNames = new List<string>();

            return model;
        }
    }
}
