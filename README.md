http://localhost:15672/

dotnet ef dbcontext scaffold
"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.225.223)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=jig)));User ID=JIG;Password=JIG;Persist Security Info=True;"
Oracle.EntityFrameworkCore --table T_JIGNET_CHECKER --table T_IOT_MASTER --output-dir Models --context-dir Data --context ProdCheckerDbContext --data-annotations --use-database-names --no-onconfiguring --force
