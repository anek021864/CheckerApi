using JigNetApi;
using JigNetApi.Dtos;
using JigNetApi.Models; // หรือ namespace ของ entity

namespace JigNetApi.Dtos;

public static class TJigNetCheckerMapper
{
    public static void ApplyToEntity(TJigNetCheckerCrateDto dto, T_JIGNET_CHECKER e)
    {
        e.ID = Id24.NewHex24();
        e.PRODSERIALNO = dto.PRODSERIALNO;
        e.COMPUTERNAME = dto.COMPUTERNAME;
        e.MODEL = dto.MODEL;
        e.CELL = dto.CELL;
        e.RESULTPASSFAIL = dto.RESULTPASSFAIL;
        e.FAILNUMBER = dto.FAILNUMBER;
        e.JIGNAME = dto.JIGNAME;
        e.DATECHECK = dto.DATECHECK;
        e.TIMEFINISH = dto.TIMEFINISH;
        e.WIRESSNO = dto.WIRESSNO;
        e.PROGRAMVER = dto.PROGRAMVER;
        e.STEPLISTVER = dto.STEPLISTVER;
        e.ALLDATALOG = dto.ALLDATALOG;
        e.NGNAME = dto.NGNAME;
        e.POSITION = dto.POSITION;
        e.CHT = dto.CHT;
        e.TESTNAME = dto.TESTNAME;
        e.TESTNO = dto.TESTNO;
        e.SPECHI = dto.SPECHI;
        e.SPECLOW = dto.SPECLOW;
        e.DATA = dto.DATA;
    }
}
