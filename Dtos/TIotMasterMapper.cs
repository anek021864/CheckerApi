using JigNetApi;
using JigNetApi.Data;
using JigNetApi.Dtos;
using JigNetApi.Models; // หรือ namespace ของ entity

public static class TIotMasterMapper
{
    public static TIotMasterResponseDto ToDto(T_IOT_MASTER e) =>
        new()
        {
            Id = e.ID,
            ProductSn = e.PRODUCT_SN,
            ComputerName = e.COMPUTER_NAME,
            Location = e.LOCATION,
            Model = e.MODEL,
            ProductionLine = e.PRODUCTIONLINE,
            Result = e.RESULT,
            FailCode = e.FAILCODE,
            CheckerName = e.CHEKER_NAME,
            ProductionTime = e.PRODUCTIONTIME,
            Shift = e.SHIFT,
            CheckerType = e.CHECKER_TYPE,
            Ref1Value = e.REF1_VALUE,
            Ref2Value = e.REF2_VALUE,
            Ref3Value = e.REF3_VALUE,
            ProgramVersion = e.PROGRAM_VERSION,
            ProductionType = e.PRODUCTION_TYPE,
            StepVersion = e.STEP_VERSION,
            StepType = e.STEP_TYPE,
            FailName = e.FAILNAME,
            Data = e.DATA,
            StartTime = e.START_TIME,
            FinishTime = e.FINISH_TIME,
            TestName = e.TESTNAME,
            TestNo = e.TESTNO,
            Unit = e.UNIT,
            Info = e.INFO,
            SpecLow = e.SPECLOW,
            SpecHi = e.SPECHI,
            TactTime = e.TACTTIME,
            UsbSerial = e.USBSERIAL,
            OrderNo = e.ORDERNO,
            Destination = e.DESTINATION,
            RomVersion = e.ROMVERSION,
            Ref4Value = e.REF4_VALUE,
            Ref5Value = e.REF5_VALUE,
            NoticeConditionFlg = e.NOTICECONDITIONFLG,
            CountClearFlg = e.COUNTCLEARFLG,
            DataInfo = e.DATAINFO,
        };

    public static TIotMasterResponseDto ToDtoPostgre(t_iot_master e) =>
        new()
        {
            Id = e.id,
            ProductSn = e.product_sn,
            ComputerName = e.computer_name,
            Location = e.location,
            Model = e.model,
            ProductionLine = e.productionline,
            Result = e.result,
            FailCode = e.failcode,
            CheckerName = e.cheker_name,
            ProductionTime = e.productiontime,
            Shift = e.shift,
            CheckerType = e.checker_type,
            Ref1Value = e.ref1_value,
            Ref2Value = e.ref2_value,
            Ref3Value = e.ref3_value,
            ProgramVersion = e.program_version,
            ProductionType = e.production_type,
            StepVersion = e.step_version,
            StepType = e.step_type,
            FailName = e.failname,
            Data = e.data,
            StartTime = e.start_time,
            FinishTime = e.finish_time,
            TestName = e.testname,
            TestNo = e.testno,
            Unit = e.unit,
            Info = e.info,
            SpecLow = e.speclow,
            SpecHi = e.spechi,
            TactTime = e.tacttime,
            UsbSerial = e.usbserial,
            OrderNo = e.orderno,
            Destination = e.destination,
            RomVersion = e.romversion,
            Ref4Value = e.ref4_value,
            Ref5Value = e.ref5_value,
            NoticeConditionFlg = e.noticeconditionflg,
            CountClearFlg = e.countclearflg,
            DataInfo = e.datainfo,
        };

    public static void ApplyToEntity(TIotMasterCreateDto dto, T_IOT_MASTER e)
    {
        e.ID = Id24.NewHex24();
        e.PRODUCT_SN = dto.ProductSn;
        e.COMPUTER_NAME = dto.ComputerName;
        e.LOCATION = dto.Location;
        e.MODEL = dto.Model;
        e.PRODUCTIONLINE = dto.ProductionLine;
        e.RESULT = dto.Result;
        e.FAILCODE = dto.FailCode;
        e.CHEKER_NAME = dto.CheckerName;
        e.PRODUCTIONTIME = dto.ProductionTime;
        e.SHIFT = dto.Shift;
        e.CHECKER_TYPE = dto.CheckerType;
        e.REF1_VALUE = dto.Ref1Value;
        e.REF2_VALUE = dto.Ref2Value;
        e.REF3_VALUE = dto.Ref3Value;
        e.PROGRAM_VERSION = dto.ProgramVersion;
        e.PRODUCTION_TYPE = dto.ProductionType;
        e.STEP_VERSION = dto.StepVersion;
        e.STEP_TYPE = dto.StepType;
        e.FAILNAME = dto.FailName;
        e.DATA = dto.Data;
        e.START_TIME = dto.StartTime;
        e.FINISH_TIME = dto.FinishTime;
        e.TESTNAME = dto.TestName;
        e.TESTNO = dto.TestNo;
        e.UNIT = dto.Unit;
        e.INFO = dto.Info;
        e.SPECLOW = dto.SpecLow;
        e.SPECHI = dto.SpecHi;
        e.TACTTIME = dto.TactTime;
        e.USBSERIAL = dto.UsbSerial;
        e.ORDERNO = dto.OrderNo;
        e.DESTINATION = dto.Destination;
        e.ROMVERSION = dto.RomVersion;
        e.REF4_VALUE = dto.Ref4Value;
        e.REF5_VALUE = dto.Ref5Value;
        e.NOTICECONDITIONFLG = dto.NoticeConditionFlg;
        e.COUNTCLEARFLG = dto.CountClearFlg;
        e.DATAINFO = dto.DataInfo;
    }

    public static void ApplyToPostgreSqlEntity(TIotMasterCreateDto dto, t_iot_master e)
    {
        e.id = Id24.NewHex24();
        e.product_sn = dto.ProductSn;
        e.computer_name = dto.ComputerName;
        e.location = dto.Location;
        e.model = dto.Model;
        e.productionline = dto.ProductionLine;
        e.result = dto.Result;
        e.failcode = dto.FailCode;
        e.cheker_name = dto.CheckerName;
        e.productiontime = dto.ProductionTime;
        e.shift = dto.Shift;
        e.checker_type = dto.CheckerType;
        e.ref1_value = dto.Ref1Value;
        e.ref2_value = dto.Ref2Value;
        e.ref3_value = dto.Ref3Value;
        e.program_version = dto.ProgramVersion;
        e.production_type = dto.ProductionType;
        e.step_version = dto.StepVersion;
        e.step_type = dto.StepType;
        e.failname = dto.FailName;
        e.data = dto.Data;
        e.start_time = dto.StartTime;
        e.finish_time = dto.FinishTime;
        e.testname = dto.TestName;
        e.testno = dto.TestNo;
        e.unit = dto.Unit;
        e.info = dto.Info;
        e.speclow = dto.SpecLow;
        e.spechi = dto.SpecHi;
        e.tacttime = dto.TactTime;
        e.usbserial = dto.UsbSerial;
        e.orderno = dto.OrderNo;
        e.destination = dto.Destination;
        e.romversion = dto.RomVersion;
        e.ref4_value = dto.Ref4Value;
        e.ref5_value = dto.Ref5Value;
        e.noticeconditionflg = dto.NoticeConditionFlg;
        e.countclearflg = dto.CountClearFlg;
        e.datainfo = dto.DataInfo;
    }
}
