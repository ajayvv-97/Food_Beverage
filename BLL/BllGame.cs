using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllGame
    {
        public bool AddData(string procedureName, DmnGame objTestClass)
        {
            DAL.DapperRepository<DmnGame> objDapperRepository = new DAL.DapperRepository<DmnGame>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Id",objTestClass.Id);
            parms.Add("Game", objTestClass.mGame);
            parms.Add("MacId", objTestClass.Macid);
            parms.Add("RFID ", objTestClass.Rfid);
            parms.Add("CardRate", objTestClass.CardRate);
            parms.Add("TokenRate", objTestClass.TokenRate);
            parms.Add("CoinRate", objTestClass.TokenRate);
            parms.Add("DiscRate", objTestClass.DiscRate);
            parms.Add("gDesc", objTestClass.gDesc);
            parms.Add("CreatedBy", objTestClass.Createdby);
            parms.Add("CreatedDate", objTestClass.CreatedDate);
            parms.Add("BranchId", objTestClass.Branchid);
            parms.Add("Status", objTestClass.status);
            parms.Add("LocationId", objTestClass.Location);
            parms.Add("image", objTestClass.Icon);
            parms.Add("Status", objTestClass.status);
            parms.Add("VipRate", objTestClass.viprate);
            parms.Add("TimeDuration", objTestClass.TimeDuration);
            parms.Add("CatergoryId", objTestClass.CategoryId);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;



        }
        public bool AddCategory(string procedureName,DmnGameCategory objtestClass)
        {
            DAL.DapperRepository<DmnGameCategory> objdapperRepository = new DAL.DapperRepository<DmnGameCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Gcategory", objtestClass.Gcategory);
            parms.Add("gDesc", objtestClass.gDesc);
            parms.Add("createdby", objtestClass.Createdby);
            var lstData = objdapperRepository.Add(procedureName, parms);
            return lstData;
        }

        }
    }
