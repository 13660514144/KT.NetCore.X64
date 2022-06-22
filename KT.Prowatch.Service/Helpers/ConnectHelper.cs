using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.Helpers
{
    public class ConnectHelper
    {
        private static ConnectHelper instance;
        public static ConnectHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectHelper();
                }
                return instance;
            }
        }
        private ConnectHelper()
        {

        }

        public string SetMsSqlConnectStr(string addr, string name, string user, string password)
        {
            return string.Format("Provider=SQLOLEDB.1; Server={0}; Initial Catalog={1}; uid={2}; pwd={3}", addr, name, user, password);
        }

    }
}
