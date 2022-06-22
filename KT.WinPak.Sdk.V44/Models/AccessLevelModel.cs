using KT.WinPak.SDK.Entities;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Models
{
    /// <summary>
    /// 访问级别
    /// </summary>
    public class AccessLevelModel
    {
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
        #endregion


        public static AccessLevelClass ToEntity(AccessLevelClass entity, AccessLevelModel model)
        {
            entity.AccessLevelName = model.AccessLevelName;
            entity.AccessLevelDesc = model.AccessLevelDesc;
            entity.AccessLevelID = model.AccessLevelID;
            entity.AccountName = model.AccountName;

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

            return model;
        }


        internal static List<AccessLevelModel> FromSqlEntities(List<AccessLevelPlus> entities)
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

        private static AccessLevelModel FromSqlEntity(AccessLevelPlus entity)
        {
            var model = new AccessLevelModel();
            model.AccessLevelID = entity.RecordId;
            model.AccessLevelName = entity.Name;
            model.AccessLevelDesc = entity.Description;
            model.AccountName = "";

            return model;
        }
    }
}
