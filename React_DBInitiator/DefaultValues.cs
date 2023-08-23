using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React_DBInitiator
{
    public class DefaultValues
    {
        public List<ColumnDefinition> departmentColumns = new() 
        {
            new ColumnDefinition { Name = "ID", DataType = "INT", Properties = "NOT NULL PRIMARY KEY IDENTITY (1,1)"},
            new ColumnDefinition { Name = "Name", DataType = "VARCHAR(50)", Properties = "NOT NULL"}
        };

        public List<ColumnDefinition> raceColumns = new()
        {
            new ColumnDefinition { Name = "ID", DataType = "INT", Properties = "NOT NULL PRIMARY KEY IDENTITY (1,1)"},
            new ColumnDefinition { Name = "Description", DataType = "VARCHAR(50)", Properties = "NOT NULL"},
            new ColumnDefinition { Name = "IsEnabled", DataType = "BIT", Properties = "NOT NULL DEFAULT 0"}
        };

        public List<ColumnDefinition> employeeColumns = new()
        {
            new ColumnDefinition { Name = "EmployeeID", DataType = "VARCHAR(255)", Properties = "NOT NULL"},
            new ColumnDefinition { Name = "Name", DataType = "VARCHAR(50)", Properties = "NOT NULL DEFAULT 'N/A'"},
            new ColumnDefinition { Name = "Surname", DataType = "VARCHAR(50)", Properties = "NOT NULL DEFAULT 'N/A'"},
            new ColumnDefinition { Name = "Email", DataType = "VARCHAR(255)", Properties = "NULL DEFAULT 'N/A'"},
            new ColumnDefinition { Name = "Cell", DataType = "VARCHAR(20)", Properties = "NULL DEFAULT 'N/A'"},
            new ColumnDefinition { Name = "ID", DataType = "VARCHAR(25)", Properties = "NOT NULL DEFAULT 'N/A'"},
            new ColumnDefinition { Name = "Gender", DataType = "VARCHAR(255)", Properties = "NOT NULL DEFAULT 'N/A'"},
            new ColumnDefinition { Name = "RaceID", DataType = "INT", Properties = "NOT NULL FOREIGN KEY REFERENCES Race(ID)"},
            new ColumnDefinition { Name = "IsSubscribed", DataType = "BIT", Properties = "NOT NULL DEFAULT 0"}
        };

        public List<ParameterDefinition> raceResultParams = new()
        {
            new ParameterDefinition { Name = "PageSize", DataType = "INT" },
            new ParameterDefinition { Name = "PageNumber", DataType = "INT" },
            new ParameterDefinition { Name = "Description", DataType = "VARCHAR(255)" },
            new ParameterDefinition { Name = "IsEnabled", DataType = "BIT" }
        };

        public List<ParameterDefinition> departemtResultParams = new()
        {
            new ParameterDefinition { Name = "PageSize", DataType = "INT" },
            new ParameterDefinition { Name = "PageNumber", DataType = "INT" },
            new ParameterDefinition { Name = "Name", DataType = "VARCHAR(255)" }
        };

        public List<ParameterDefinition> employeeResultParams = new()
        {
            new ParameterDefinition { Name = "PageSize", DataType = "INT" },
            new ParameterDefinition { Name = "PageNumber", DataType = "INT" },
            new ParameterDefinition { Name = "Name", DataType = "VARCHAR(50)" },
            new ParameterDefinition { Name = "Surname", DataType = "VARCHAR(50)" },
            new ParameterDefinition { Name = "Email", DataType = "VARCHAR(255)" },
            new ParameterDefinition { Name = "Cell", DataType = "VARCHAR(20)" },
            new ParameterDefinition { Name = "ID", DataType = "VARCHAR(25)" },
            new ParameterDefinition { Name = "Gender", DataType = "VARCHAR(255)" },
            new ParameterDefinition { Name = "RaceID", DataType = "INT" },
            new ParameterDefinition { Name = "IsSubscribed", DataType = "BIT" },
        };

        public List<ParameterDefinition> employeeParams = new()
        {
            new ParameterDefinition { Name = "EmployeeID", DataType = "VARCHAR(255)"},
            new ParameterDefinition { Name = "Name", DataType = "VARCHAR(50)" },
            new ParameterDefinition { Name = "Surname", DataType = "VARCHAR(50)" },
            new ParameterDefinition { Name = "Email", DataType = "VARCHAR(255)" },
            new ParameterDefinition { Name = "Cell", DataType = "VARCHAR(20)" },
            new ParameterDefinition { Name = "ID", DataType = "VARCHAR(25)" },
            new ParameterDefinition { Name = "Gender", DataType = "VARCHAR(255)" },
            new ParameterDefinition { Name = "RaceID", DataType = "INT" },
            new ParameterDefinition { Name = "Subscribed", DataType = "BIT" },
        };

    }
}
