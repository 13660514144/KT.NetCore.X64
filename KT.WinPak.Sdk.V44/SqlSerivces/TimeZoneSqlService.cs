using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.ISqlDaos;
using KT.WinPak.SDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.SqlSerivces
{
    public class TimeZoneSqlService : ITimeZoneSqlService
    {
        private ITimeZoneSqlDao _timeZoneSqlDao;
        public TimeZoneSqlService(ITimeZoneSqlDao timeZoneSqlDao)
        {
            _timeZoneSqlDao = timeZoneSqlDao;
        }
        public List<TimeZoneModel> GetAll()
        {
            var entities = _timeZoneSqlDao.GetAll();

            var models = TimeZoneModel.FromSqlEntities(entities);

            return models;
        }
    }
}
