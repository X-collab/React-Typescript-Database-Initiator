using React_DBInitiator;
using React_DBInitiator.Functions;
 
TableCreation table = new TableCreation();

await table.CreateTable("race", "RaceTableScript");

//Before running the console application, make sure to create the initial database where the tables will be stored

