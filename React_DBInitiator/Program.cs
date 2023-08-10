
using Dapper;
using React_DBInitiator;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


DapperContext context = new DapperContext();

Console.WriteLine("");
Console.WriteLine("Initiating creation of Data database....");

Console.WriteLine("Creating Department Table....");

//Insert creation method here

Console.WriteLine("Database has been created successfully!!");
