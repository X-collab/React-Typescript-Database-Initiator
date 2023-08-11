using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace React_DBInitiator.Functions
{
    public class TableCreation
    {
        public async Task CreateTable(string name, string fileName)
        {
            try
            {
                var scriptPath = string.Format("{0}/Scripts/{1}.sql", Directory.GetCurrentDirectory(), fileName);
                var script = File.ReadAllText(scriptPath);

                await Console.Out.WriteLineAsync("Checking if script exists......");
                if (File.Exists(scriptPath))
                {
                    await Console.Out.WriteLineAsync("Success!! Script has been found!!");

                    string connString = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString;

                    SqlConnection conn = new SqlConnection(connString);

                    Server server = new Server(new ServerConnection(conn));

                    await Console.Out.WriteLineAsync(string.Format("Creating {0} table...", name));

                    server.ConnectionContext.ExecuteNonQuery(script);

                    await Console.Out.WriteLineAsync(string.Format("Success!! {0} table has been created!", name));

                }
                else
                {
                    await Console.Out.WriteLineAsync("The script could not be found!!!");
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " +  ex.Message);
            }

        }
    }
}
