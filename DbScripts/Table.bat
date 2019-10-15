set /p server= Enter SQL Server Name:
set /p db_name= Enter DB Name:
del "ResultLog.log"

ECHO add column and update date
sqlcmd -S %server% -d %db_name% -i "Users.sql" >> "ResultLog.log"
sqlcmd -S %server% -d %db_name% -i "Calculations.sql" >> "ResultLog.log"