
using Dapper;
using React_DBInitiator;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


DapperContext context = new DapperContext();

Console.WriteLine("");
Console.WriteLine("Initiating creation of Data database....");

Console.WriteLine("Creating Department Table....");

try
{
    var query = "SELECT * FROM Department";

    using (var connection = context.CreateConnection())
    {
        var creation = await connection.QueryAsync(query);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

//Insert creation method here

Console.WriteLine("Database has been created successfully!!");
