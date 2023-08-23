using React_DBInitiator;

DefaultValues defaultValues = new DefaultValues();

//Starts off the service (Insert your server name in the "YourServerName")
DatabaseService service = new DatabaseService("YourServerName", "Data");

service.DatabaseInitiator();
service.TableInitiator("Race", defaultValues.raceColumns);
service.TableInitiator("Department", defaultValues.departmentColumns);
service.TableInitiator("Employee", defaultValues.employeeColumns);

service.StoredProcedureInitiator("PaginateRaceResults", @"SELECT @TotalResults = COUNT(*) FROM Race 
	WHERE (Description = @Description OR @Description IS NULL) AND (IsEnabled = @IsEnabled OR @IsEnabled IS NULL) 
	SELECT * FROM Race WHERE (Description = @Description OR @Description IS NULL) AND (IsEnabled = @IsEnabled OR @IsEnabled IS NULL)
	ORDER BY ID
	OFFSET @PageSize * (@PageNumber - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY;
	PRINT 'Total Results: ' + CAST(@TotalResults AS VARCHAR(10));", defaultValues.raceResultParams);

service.StoredProcedureInitiator("PaginateDepartmentResults", @"SELECT @TotalResults = COUNT(*) FROM Department 
	WHERE (Name = @Name OR @Name IS NULL)
	SELECT *
	FROM Department
	WHERE (Name = @Name OR @Name IS NULL)
	ORDER BY ID
	OFFSET @PageSize * (@PageNumber - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY;
	PRINT 'Total Results: ' + CAST(@TotalResults AS VARCHAR(10));", defaultValues.departemtResultParams);

service.StoredProcedureInitiator("PaginateEmployeeResults", @"SELECT * FROM Employee
	WHERE (Name = @Name OR @Name IS NULL) AND (Surname = @Surname OR @Surname IS NULL) AND 
	(Email = @Email OR @Email IS NULL) AND (Cell = @Cell OR @Cell IS NULL)
	AND (ID = @ID OR @ID IS NULL) AND (Gender = @Gender OR @Gender IS NULL) AND 
	(RaceID = @RaceID OR @RaceID IS NULL) AND (IsSubscribed = @IsSubscribed OR @IsSubscribed IS NULL)
	ORDER BY EmployeeID
	OFFSET @PageSize * (@PageNumber - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY;
	PRINT 'Total Results: ' + CAST(@TotalResults AS VARCHAR(10));", defaultValues.employeeResultParams);

service.StoredProcedureInitiator("UpdateEmployee", @"UPDATE Employee
SET Name = @Name, Surname = @Surname, Email = @Email, Cell = @Cell, ID = @ID, Gender = @Gender, RaceID = @RaceID, IsSubscribed = @Subscribed
WHERE EmployeeID = @EmployeeID", defaultValues.employeeParams);

service.StoredProcedureInitiator("CreateEmployee", @"INSERT INTO Employee 
VALUES(@EmployeeID, @Name, @Surname, @Email, @Cell, @ID, @Gender, @RaceID, @Subscribed)", defaultValues.employeeParams);
