using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace HelperTools
{
    public interface IDalMySql
    {
        DataTable MySqlListData(string SQLstring);
        long ExecuteMySql(string SQLstring);
    }
}
