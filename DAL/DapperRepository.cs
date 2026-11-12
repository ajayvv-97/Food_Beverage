
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;

namespace DAL
{
    public class DapperRepository<T> where T : class
    {
        #region GetAll
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll(string storedProcedure)
        {
            List<T> lstResult = null;

            using (IDbConnection connection = OpenConnection())
            {
                lstResult = connection.Query<T>(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
            }

            return lstResult;
        }
        #endregion GetAll

        #region FindByID
        /// <summary>
        /// FindByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindByID(string sp, DynamicParameters param)
        {
            T resultObj = null;

            using (IDbConnection connection = OpenConnection())
            {
                resultObj = connection.Query<T>(sp, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return resultObj;
        }
        #endregion FindByID

        #region Add
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(string sp, DynamicParameters param, bool transactionType = true)
        {
            bool status = false;

            using (IDbConnection connection = OpenConnection())
            {
                if (transactionType)
                {
                    using (var tran = connection.BeginTransaction())
                    {
                        try
                        {
                            status = connection.Execute(sp, param, tran, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                            tran.Commit();
                            status = true;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    status = connection.Execute(sp, param, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                }
            }

            return status;
        }
        #endregion Add

        #region Add
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public T AddandReturnID(string sp, DynamicParameters param, bool transactionType = true)
        {
            bool status = false;
            T resultObj = null;

            using (IDbConnection connection = OpenConnection())
            {
                if (transactionType)
                {
                    using (var tran = connection.BeginTransaction())
                    {
                        try
                        {
                            resultObj = connection.Query<T>(sp, param, tran, commandType: CommandType.StoredProcedure).FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    status = connection.Execute(sp, param, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                }
            }

            return resultObj;
        }
        #endregion Add

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(string sp, DynamicParameters param, bool trasactionType = true)
        {
            bool status = false;

            using (IDbConnection connection = OpenConnection())
            {
                if (trasactionType)
                {
                    using (var tran = connection.BeginTransaction())
                    {
                        try
                        {
                            status = connection.Execute(sp, param, tran, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    status = connection.Execute(sp, param, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                }
            }

            return status;
        }
        #endregion Update

        #region Remove
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(string sp, DynamicParameters param)
        {
            bool status = false;

            using (IDbConnection connection = OpenConnection())
            {
                //const string sql = "dbo.tmpDapperUserDelete";
                status = connection.Execute(sp, param, commandType: CommandType.StoredProcedure) > 0 ? true : false;
            }

            return status;
        }
        #endregion Remove

        #region SelectQuery
        /// <summary>
        /// SelectQuery
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> SelectQuery(string sp, DynamicParameters param)
        {
            List<T> lstResult = null;
           
            using (IDbConnection connection = OpenConnection())
            {
                lstResult = connection.Query<T>(sp, param, commandType: CommandType.StoredProcedure).ToList();
            }

            return lstResult;
        }
        #endregion SelectQuery

        #region SelectQuery_CardAccount Fetch
        /// <summary>
        /// SelectQuery
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T>SelectQuery_CardAcc(string sp, DynamicParameters param)
        {
            List<T> lstResult = null;
            string connectionString = "Data Source=192.168.0.40;Initial Catalog=mPOS_DBNew;Persist Security Info=True;User ID=sa;Password=,rhsm098";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    lstResult = connection.Query<T>(sp, param, commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                }
            }

            return lstResult;
        }
        #endregion SelectQuery

        #region SelectQueryWithOutClass
        /// <summary>
        /// SelectQueryWithOutClass
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataSet SelectQueryWithOutClass(string sp, List<SqlParameter> param)
        {
            DataSet lstResult = new DataSet();

            using (IDbConnection connection = OpenConnection())
            {
                IDbDataAdapter dbAdapter = new SqlDataAdapter();
                IDbCommand dbCommand = connection.CreateCommand();
                dbCommand.CommandText = sp;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbAdapter.SelectCommand = dbCommand;

                foreach (var data in param)
                {

                    dbAdapter.SelectCommand.Parameters.Add(data);
                }

                dbAdapter.Fill(lstResult);
            }

            return lstResult;
        }
        #endregion SelectQueryWithOutClass

        #region ExecuteSp
        /// <summary>
        /// ExecuteSp
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <param name="transactionType"></param>
        /// <returns></returns>
        public bool ExecuteSp(string sp, DynamicParameters param, bool transactionType = false)
        {
            bool status = false;

            using (IDbConnection connection = OpenConnection())
            {
                if (transactionType)
                {
                    using (var tran = connection.BeginTransaction())
                    {
                        status = connection.Execute(sp, param, tran, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                    }
                }
                else
                {
                    status = connection.Execute(sp, param, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                }
            }

            return status;
        }
        #endregion ExecuteSpC:\inetpub\wwwroot\AYJSoftWebServicepppppppp,k

                /// <summary>
        /// Executes a parameterized text command using the application's configured database.
        /// </summary>
        public int ExecuteText(string sql, object parameters)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL command cannot be empty.", "sql");

            using (IDbConnection connection = OpenConnection())
            {
                return connection.Execute(sql, parameters, commandType: CommandType.Text);
            }
        }

        #region OpenConnection
        /// <summary>
        /// OpenConnection
        /// </summary>
        /// <returns></returns
        public static IDbConnection OpenConnection()
        {
            //IDbConnection connection = new SqlConnection("Data Source=192.168.0.40;Initial Catalog=Food_Beverage_TEST;Persist Security Info=True;User ID=sa;Password=,rhsm098");//Food_Beverage_DEBUG
            //IDbConnection connection = new SqlConnection("Data Source=192.168.0.159;Initial Catalog=Food_Beverage_copy;Persist Security Info=True;User ID=sa;Password=,rhsm098");
            //IDbConnection connection = new SqlConnection("Data Source=OFFICE\\SQLEXPRESS;Initial Catalog=Food_Beverage_TEST;Integrated Security=True");
            // IDbConnection connection = new SqlConnection("Data Source=LAPTOP - HQE017L7\\SQLEXPRESS;Initial Catalog=Food_Beverage_TEST;Persist Security Info=True;User ID=sa;Password=,rhsm098");//Food_Beverage_DEBUG

            //connection.Open();
            //return connection;


            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBConfig", "dbsettings.txt");
            string connectionString = File.ReadAllText(configPath).Trim();

            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;

        }
        #endregion OpenConnection
    }
}
