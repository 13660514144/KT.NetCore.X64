﻿using KT.WinPak.SDK.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.ISqlDaos
{
    public interface IAccessLevelSqlDao
    {
        List<AccessLevelPlus> GetAll();
    }
}