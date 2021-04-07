using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SensorData.DBLayer {
    public partial class DBLayer {
        public long InsertNewTestParameter(DBTestParameter pNewTestParameter) {
            long nNewID = long.MinValue;
            try {
                using (NpgsqlConnection pDBConnection = new NpgsqlConnection(__sConnectionString)) {
                    pDBConnection.Open();
                    

                    string sInsert = string.Format("INSERT INTO {0} ({1}, {2}, {3},{4},{5}) VALUES (@sName,@nPreTestMS,@nTimeMS,@nHighLimitMS,@nLowLimitMS) RETURNING {6}",
                        DBNames.TestParamTable.TableName, DBNames.TestParamTable.Columns.name, DBNames.TestParamTable.Columns.pretest_ms, DBNames.TestParamTable.Columns.time_ms,
                        DBNames.TestParamTable.Columns.highlimit_mv, DBNames.TestParamTable.Columns.lowlimit_mv, DBNames.TestParamTable.Columns.id);

                    // Insert some data
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sInsert, pDBConnection)) {

                        cmd.Parameters.AddWithValue("sName", pNewTestParameter.Name);
                        cmd.Parameters.AddWithValue("nPreTestMS", pNewTestParameter.PreTestMS);
                        cmd.Parameters.AddWithValue("nTimeMS", pNewTestParameter.TestTimeMS);
                        cmd.Parameters.AddWithValue("nHighLimitMS", pNewTestParameter.HighLimitmV);
                        cmd.Parameters.AddWithValue("nLowLimitMS", pNewTestParameter.LowLimitmV);
                        object pID = cmd.ExecuteScalar();

                        Int64.TryParse(pID.ToString(), out nNewID);
                    }
                }

            }
            catch (Exception e) {
                //todo: Add logging
            }

            return nNewID;
        }

        public bool UpdateTestParameter(DBTestParameter pDBParam) {
            bool bSucces = true;
            try {
                string sUpdate = string.Format("UPDATE {0} SET {1}=@sName, {2} = @nPreTestMS, {3} = @nTimeMS, {4} = @nHighLimitmV, {5} = @nLowLimitmV WHERE {6} = @nID",
              DBNames.TestParamTable.TableName, DBNames.TestParamTable.Columns.name, DBNames.TestParamTable.Columns.pretest_ms, DBNames.TestParamTable.Columns.time_ms, DBNames.TestParamTable.Columns.highlimit_mv,
              DBNames.TestParamTable.Columns.lowlimit_mv, DBNames.TestParamTable.Columns.id);

                using (NpgsqlConnection pDBConnection = new NpgsqlConnection(__sConnectionString)) {
                    pDBConnection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sUpdate, pDBConnection)) {                       
                        cmd.Parameters.AddWithValue("sName", pDBParam.Name);
                        cmd.Parameters.AddWithValue("nPreTestMS", pDBParam.PreTestMS);
                        cmd.Parameters.AddWithValue("nTimeMS", pDBParam.TestTimeMS);
                        cmd.Parameters.AddWithValue("nHighLimitmV", pDBParam.HighLimitmV);
                        cmd.Parameters.AddWithValue("nLowLimitmV", pDBParam.LowLimitmV);
                        cmd.Parameters.AddWithValue("nID", pDBParam.ID);
                        cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch(Exception e) {
                bSucces = false;
                //todo: Add logging
            }

            return bSucces;     
        }

        public DBTestParameter GetTestParameter(long nParamID) {
            string sWhere = string.Format("{0} = {1}", DBNames.TestParamTable.Columns.id, nParamID);
            List<DBTestParameter> pParams = GetAllTestParametersWhere(sWhere);

            return pParams.FirstOrDefault();
        }

        public List<DBTestParameter> GetAllTestParameters() {
            return GetAllTestParametersWhere(string.Empty);
        }

        private List<DBTestParameter> GetAllTestParametersWhere(string sWhere) {            
            string sSelect = string.Format("SELECT * FROM {0}", DBNames.TestParamTable.TableName);
            return GetCollectionFromDB<DBTestParameter>(__sConnectionString, sSelect, ReadConvertTestParamterFromDB);            
        }

        /// <summary>
        /// Used for reading data from generic Get method for collections
        /// </summary>
        /// <param name="pReader">DB Reader returned from the select query</param>
        /// <returns></returns>
        private static DBTestParameter ReadConvertTestParamterFromDB(NpgsqlDataReader pReader) {
            long nID = Int64.Parse(pReader[0].ToString());
            string sName = pReader[1].ToString();
            int nPreTestMS = Int32.Parse(pReader[2].ToString());
            int nTime = Int32.Parse(pReader[3].ToString());
            double nHighLimitmV = Double.Parse(pReader[4].ToString());
            double nLowLimitmV = Double.Parse(pReader[5].ToString());

            return new DBTestParameter(nID, sName, nPreTestMS, nTime, nHighLimitmV, nLowLimitmV);
        }
    }
}
