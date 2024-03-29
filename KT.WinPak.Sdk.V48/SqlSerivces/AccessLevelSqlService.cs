﻿using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.ISqlDaos;
using KT.WinPak.SDK.V48.Models;
using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.V48.SqlSerivces
{
    public class AccessLevelSqlService : IAccessLevelSqlService
    {
        private IAccessLevelSqlDao _accessLevelSqlDao;
        public AccessLevelSqlService(IAccessLevelSqlDao accessLevelSqlDao)
        {
            _accessLevelSqlDao = accessLevelSqlDao;
        }
        public List<AccessLevelModel> GetAll()
        {
            var entities = _accessLevelSqlDao.GetAll();

            var models = AccessLevelModel.FromSqlEntities(entities);

            return models;
        }
        public AccessLevelModel Add(AccessLevelModel model)
        {
            throw new NotImplementedException();
        }

        public bool Configure(ConfigureAccessLevelModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string bstrAcclName)
        {
            throw new NotImplementedException();
        }

        public bool Edit(string bstrAccl, AccessLevelModel model)
        {
            throw new NotImplementedException();
        }

        public AccessLevelModel GetByName(string bstrAcclName)
        {
            throw new NotImplementedException();
        }
    }
}
