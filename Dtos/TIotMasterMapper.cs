using JigNetApi;
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
}
