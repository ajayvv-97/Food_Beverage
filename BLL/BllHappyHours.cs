using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllHappyHours
    {
        public bool AddHappyHoures(string procedureName, DmnHappyHours objtestclass)
        {
            DAL.DapperRepository<DmnHappyHours> objDapperRepository = new DAL.DapperRepository<DmnHappyHours>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@gmid", objtestclass.gmid);
            parms.Add("@htitle", objtestclass.htitle);
            parms.Add("@Dayid", objtestclass.Dayid);
            parms.Add("@fTime", objtestclass.ftime);
            parms.Add("@tTime", objtestclass.ttime);
            parms.Add("@gDur", objtestclass.gDur);
            parms.Add("@CardRate", objtestclass.cardrate);
            parms.Add("@CardDiscRate", objtestclass.CardDiscRate);
            parms.Add("@CardDiscper", objtestclass.CardDiscPer);
            parms.Add("@VIPRate", objtestclass.VIPRate);
            parms.Add("@VIPDiscRate", objtestclass.VIPDiscRate);
            parms.Add("@VIPDiscper", objtestclass.VIPDiscPer);
            parms.Add("@CreatedBy", objtestclass.CreatedBy);
            parms.Add("@flage", objtestclass.flage);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
           

        }

    }
}
