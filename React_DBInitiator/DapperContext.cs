using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React_DBInitiator
{
    public class DapperContext
    {
        public IDbConnection CreateConnection()
            => new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString);
    }
}
