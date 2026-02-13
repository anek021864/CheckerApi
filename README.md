http://localhost:15672/
user: guest

//Database first for oracle database
dotnet ef dbcontext scaffold
"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.225.223)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=jig)));User ID=JIG;Password=JIG;Persist Security Info=True;"
Oracle.EntityFrameworkCore --table T_JIGNET_CHECKER --table T_IOT_MASTER --output-dir Models --context-dir Data --context ProdCheckerDbContext --data-annotations --use-database-names --no-onconfiguring --force

//Database first for postgeSql database
dotnet ef dbcontext scaffold
"Host=192.168.225.169;Port=5432;Database=PDE-DBP-TEST;Username=jigcheckertest; Password=jigcheckertest"
Npgsql.EntityFrameworkCore.PostgreSQL
--table JIGDWHTEST.t_iot_master
--table JIGDWHTEST.t_jignet_checker
--output-dir Models/PostgreSql
--context-dir Data
--context ProdCheckerPostgreSqlDqlDbContext
--namespace JigNetApi.Data
--data-annotations
--use-database-names
--no-onconfiguring
--force

-- สร้างตารางใน Schema JIGDWHTEST (ชื่อ Schema ต้องมีอยู่จริง)
CREATE TABLE "JIGDWHTEST".t_iot_master (
id VARCHAR(24) NOT NULL,
product_sn VARCHAR(100),
computer_name VARCHAR(11),
location NUMERIC,
model VARCHAR(20),
productionline VARCHAR(20),
result NUMERIC,
failcode VARCHAR(5),
cheker_name VARCHAR(500),
productiontime TIMESTAMP,
shift NUMERIC,
checker_type BOOLEAN NOT NULL,
ref1_value VARCHAR(100),
ref2_value VARCHAR(100),
ref3_value VARCHAR(100),
program_version NUMERIC,
production_type NUMERIC,
step_version NUMERIC,
step_type NUMERIC,
failname VARCHAR(500),
data TEXT,
start_time VARCHAR(10),
finish_time VARCHAR(10),
testname TEXT,
testno TEXT,
unit VARCHAR(4000),
info VARCHAR(4000),
speclow TEXT,
spechi TEXT,
tacttime VARCHAR(50),
usbserial VARCHAR(50),
orderno VARCHAR(50),
destination VARCHAR(100),
romversion VARCHAR(20),
ref4_value VARCHAR(100),
ref5_value VARCHAR(100),
noticeconditionflg VARCHAR(100),
countclearflg VARCHAR(100),
datainfo VARCHAR(50),

    -- แก้จุดนี้: ห้ามมี Schema นำหน้าชื่อ PK
    CONSTRAINT t_iot_master_pk PRIMARY KEY (id)

);

-- สร้าง Index
CREATE INDEX t_iot_master_index1 ON "JIGDWHTEST".t_iot_master (productiontime DESC);
