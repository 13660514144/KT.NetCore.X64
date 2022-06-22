using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.TestTool.TestApp.Datas.Base;
using KT.TestTool.TestApp.Entities;
using KT.TestTool.TestApp.IDaos;
using System.Collections.Generic;
using System.Linq;

namespace KT.TestTool.TestApp.Daos
{
    public class SystemConfigDataDao : ISystemConfigDataDao
    {
        private TestAppContext _context;
        public SystemConfigDataDao(TestAppContext context)
        {
            _context = context;
        }

        public SystemConfigEntity Add(SystemConfigEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }
            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = _context.SystemConfigs.Add(entity);
            var rows = _context.SaveChanges();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public bool Delete(string id)
        {
            //查询出要删除的数据
            var entity = _context.SystemConfigs.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw CustomException.Run("系统中找不到当前系统配置数据：id:{0} ", id);
            }

            //执行新增操作
            var result = _context.SystemConfigs.Remove(entity);
            var rows = _context.SaveChanges();
            return rows > 0;
        }

        public SystemConfigEntity Edit(SystemConfigEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = _context.SystemConfigs.Update(entity);
            var rows = _context.SaveChanges();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public List<SystemConfigEntity> GetAll()
        {
            var results = _context.SystemConfigs.ToList();
            return results;
        }

        public SystemConfigEntity GetById(string id)
        {
            var result = _context.SystemConfigs.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public SystemConfigEntity GetByKey(string key)
        {
            var result = _context.SystemConfigs.FirstOrDefault(x => x.Key == key);
            return result;
        }
    }
}
