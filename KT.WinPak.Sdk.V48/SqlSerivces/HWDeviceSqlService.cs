using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.ISqlDaos;
using KT.WinPak.SDK.V48.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.SqlSerivces
{
    public class HWDeviceSqlService : IHWDeviceSqlService
    {
        private IHWDeviceSqlDao _hwDeviceSqlDao;
        public HWDeviceSqlService(IHWDeviceSqlDao hwDeviceSqlDao)
        {
            _hwDeviceSqlDao = hwDeviceSqlDao;
        }

        public List<HWDeviceModel> GetAll()
        {
            throw new NotImplementedException();
        }
         

        public List<HWDeviceModel> GetAllReaders()
        {
            var entities = _hwDeviceSqlDao.GetReaders();

            var models = HWDeviceModel.FromSqlEntities(entities);

            return models;
        }
    }
}
