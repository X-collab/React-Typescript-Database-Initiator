using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace React_DBInitiator
{
    public class DatabaseService
    {
        private readonly string _serverName;
        private readonly string _databaseName;
        private readonly string _databaseConnectionString;
        private readonly string _serverConnectionString;

        public DatabaseService(string serverName, string databaseName)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            _databaseConnectionString = $"Server={_serverName};Database={_databaseName};Trusted_Connection=true;";
            _serverConnectionString = $"Server={_serverName};Integrated Security=True;";
        }

        public void DatabaseInitiator()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_serverConnectionString))
                {
                    connection.Open();

                    //Check if database exists
                    string checkDatabaseExistsQuery = $"IF DB_ID('{_databaseName}') IS NOT NULL SELECT 1 ELSE SELECT 0";

                    using (SqlCommand command = new SqlCommand(checkDatabaseExistsQuery, connection))
                    {
                        int databaseExists = (int)command.ExecuteScalar();

                        if (databaseExists == 0)
                        {
                            Console.WriteLine($"Database '{_databaseName} does not exist.");
                            CreateDatabase();
                        }
                        else
                        {
                            Console.WriteLine($"Database '{_databaseName}' already exists.");
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                ErrorHandling($"Initiating database creation '{_databaseName}'", ex);
            }

        }

        public void TableInitiator(string tableName, List<ColumnDefinition> columns)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
                {
                    connection.Open();

                    //Check if table exists
                    string checkDatabaseExistsQuery = $"IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}') SELECT 1 ELSE SELECT 0";

                    using (SqlCommand command = new SqlCommand(checkDatabaseExistsQuery, connection))
                    {
                        int tableExists = (int)command.ExecuteScalar();

                        if (tableExists == 0)
                        {
                            Console.WriteLine($"Table '{tableName}' does not exist.");
                            CreateTable(tableName, columns);
                        }
                        else
                        {
                            Console.WriteLine($"Table '{tableName}' already exists.");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorHandling($"Initiating table creation '{tableName}'", ex);
            }

        }

        public void StoredProcedureInitiator(string procedureName, string procedureAction, List<ParameterDefinition> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
                {
                    connection.Open();

                    //Check if stored procedure exists
                    string checkStoredProcedureQuery = $"IF OBJECT_ID('{procedureName}', 'P') IS NOT NULL SELECT 1 ELSE SELECT 0";

                    using (SqlCommand command = new SqlCommand(checkStoredProcedureQuery, connection))
                    {
                        int procedureExists = (int)command.ExecuteScalar();

                        if (procedureExists == 0)
                        {
                            Console.WriteLine($"Procedure '{procedureName}' does not exist.");
                            CreateStoredProcedure(procedureName, procedureAction, parameters);
                        }
                        else
                        {
                            Console.WriteLine($"Procedure '{procedureName}' already exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling($"Initiating stored procedure '{procedureName}'", ex);
            }
        }

        public void CreateDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_serverConnectionString))
                {
                    connection.Open();

                    // Create a new database
                    string createDatabaseQuery = $"CREATE DATABASE {_databaseName}";

                    using (SqlCommand cmd = new SqlCommand(createDatabaseQuery, connection))
                    {
                        Console.WriteLine($"Creating database '{_databaseName}'....");
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Database '{_databaseName}' created successfully.");
                    }

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                ErrorHandling($"Creating database '{_databaseName}'", ex);
            }

        }

        public void CreateTable(string tableName, List<ColumnDefinition> columns)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
                {
                    connection.Open();

                    //Create a new table
                    string createTableQuery = $"CREATE TABLE {tableName} (";
                    foreach (var column in columns)
                    {
                        //Add each column to the query
                        createTableQuery += $"{column.Name} {column.DataType} {column.Properties}, ";
                    }

                    //Remove spacing and commas at the end of the query
                    createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

                    using (SqlCommand cmd = new SqlCommand(createTableQuery, connection))
                    {
                        Console.WriteLine($"Creating Table '{tableName}'....");
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Table '{tableName}' created successfully.");
                    }

                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                ErrorHandling($"Creating Table '{tableName}'", ex);
            }
        }

        public void CreateStoredProcedure(string procedureName, string procedureAction, List<ParameterDefinition> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
                {
                    connection.Open();

                    StringBuilder builder = new StringBuilder();

                    //Create a new stored procedure
                    string createStoredProcedureQuery = "";

                    if (parameters.IsNullOrEmpty())
                    {
                        createStoredProcedureQuery = $"CREATE PROCEDURE {procedureName} AS BEGIN {procedureAction} END";
                    }
                    else
                    {
                        foreach (var parameter in parameters)
                        {
                            //Add each parameter to the string
                            builder.Append($"@{parameter.Name} {parameter.DataType}, ");
                        }

                        createStoredProcedureQuery = $"CREATE PROCEDURE {procedureName} {builder.ToString().TrimEnd(',', ' ')} AS BEGIN {procedureAction} END";
           
                    }

                    using (SqlCommand cmd = new SqlCommand(createStoredProcedureQuery, connection))
                    {
                        Console.WriteLine($"Creating Procedure '{procedureName}'....");
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Procedure '{procedureName}' created successfully.");
                    }

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                ErrorHandling($"Creating Stored Procedure '{procedureName}'", ex);
            }
        }

        public void ErrorHandling(string actionName,Exception ex)
        {
            
            Console.WriteLine($"Error while '{actionName}': {ex.Message}!");
        }


    }
}
