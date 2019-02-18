﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTKUtilites.DataAccessLayer;
using System.Configuration;
using System.Data;
using GTKUtilites.SessionUtils;
using GTKUtilites.HelpMethods;

namespace GlobalPartsLibrary.DataAccessLayer
{
   public class CompositionsDAL
    {
        IDBOracleAdapter da = new IDBOracleAdapter();
        #region Open
        internal DataSet Open_GPCMPSTN()
        {
            IDBManager dbManager = CommonConnection.Connectionstring();
            try
            {
                dbManager.Open();

                DataSet ds = new DataSet();
                dbManager.CreateParameters(4);

                if (ConfigurationManager.AppSettings["DataB"] == "SQL")
                {
                    if (SessionObjects.obj.GlobalPropertiesObject.UnId != null && SessionObjects.obj.GlobalPropertiesObject.UnId != "")
                        dbManager.AddParameters(0, "@returnUniqueKey", new Guid(SessionObjects.obj.GlobalPropertiesObject.UnId), ParameterDirection.InputOutput, 100);
                    else
                        dbManager.AddParameters(0, "@returnUniqueKey", DBNull.Value, ParameterDirection.InputOutput, 100);

                    if (SessionObjects.obj.GlobalPropertiesObject.FteCode != null && SessionObjects.obj.GlobalPropertiesObject.FteCode != "")
                        dbManager.AddParameters(1, "@FTECODE", SessionObjects.obj.GlobalPropertiesObject.FteCode, ParameterDirection.Input, 10);

                    else
                        dbManager.AddParameters(1, "@FTECODE", DBNull.Value, ParameterDirection.Input, 10);

                    dbManager.AddParameters(2, "@UserCode", SessionObjects.obj.GlobalPropertiesObject.UserCode, ParameterDirection.Input, 100);
                    dbManager.AddParameters(3, "@ModCode", SessionObjects.obj.GlobalPropertiesObject.ModCode, ParameterDirection.Input, 100);
                    // dbManager.AddParameters(3, "@ListCode", ListCode, ParameterDirection.Input);

                    // string spCall = Helper.Ins.GetSPCall((dbManager.Parameters, "Open_Grid");
                    string spCall = Helper.Ins.GetSPCall(dbManager.Parameters, "Open_GPCMPSTN");
                    ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "Open_GPCMPSTN");

                    if (dbManager.Parameters[0] != null)
                        SessionObjects.obj.GlobalPropertiesObject.UnId = dbManager.Parameters[0].Value.ToString();

                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        public DataSet Fetch_GPCMPSTN(string CType, string CmpCd)
        {
            IDBManager dbManager = CommonConnection.Connectionstring();
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(2);
                DataSet ds = new DataSet();
                if (ConfigurationManager.AppSettings["DataB"] == "SQL")
                {
                    dbManager.AddParameters(0, "@CType", CType, ParameterDirection.Input, 100);
                    dbManager.AddParameters(1, "@CmpCd", CmpCd, ParameterDirection.Input, 100);
                }
                if (ConfigurationManager.AppSettings["DataB"] == "SQL")
                {
                    string spCall = Helper.Ins.GetSPCall(dbManager.Parameters, "Fetch_GPCMPSTN");
                    ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "Fetch_GPCMPSTN");

                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }

        }

        internal int Save_GPCMPSTN(string xmlData)
        {
            IDBManager dbManager = CommonConnection.Connectionstring();
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(2);
                if (ConfigurationManager.AppSettings["DataB"] == "SQL")
                {
                    dbManager.AddParameters(0, "@UserCode", SessionObjects.obj.GlobalPropertiesObject.UserCode, ParameterDirection.Input, 100);
                    dbManager.AddParameters(1, "@SaveXML", xmlData, ParameterDirection.Input);
                    //dbManager.AddParameters(2, "@Mode", Mode, ParameterDirection.Input, 2);
                    //dbManager.AddParameters(3, "@Active", Active, ParameterDirection.Input, 3);
                }
                int res = 0;
                if (ConfigurationManager.AppSettings["DataB"] == "SQL")
                {
                    string spCall = Helper.Ins.GetSPCall(dbManager.Parameters, "Save_GPCMPSTN");
                    res = Convert.ToInt32(dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Save_GPCMPSTN"));
                }


                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
    }
}
