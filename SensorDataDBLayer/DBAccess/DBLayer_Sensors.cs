using Npgsql;
using System;
using System.Collections.Generic;

namespace SensorData.DBLayer {
    public partial class DBLayer {
        public bool InsertNewSensor(DBSensor pSensor) {
            bool bSuccess = true;
            try {
                using (NpgsqlConnection pDBConnection = new NpgsqlConnection(__sConnectionString)) {
                    pDBConnection.Open();

                    string sInsert = string.Format("INSERT INTO {0} ({1}, {2}, {3}) VALUES (@nMatNum, @sName,@nTestParamID)",
                        DBNames.SensorTable.TableName, DBNames.SensorTable.Columns.material_num, DBNames.SensorTable.Columns.name, DBNames.SensorTable.Columns.testparam);

                    // Insert some data
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sInsert, pDBConnection)) {

                        cmd.Parameters.AddWithValue("nMatNum", pSensor.MaterialNumber);
                        cmd.Parameters.AddWithValue("sName", pSensor.Name);
                        cmd.Parameters.AddWithValue("nTestParamID", pSensor.TestParamID ?? (object)DBNull.Value);
                        cmd.ExecuteNonQueryAsync();                        
                    }
                }
            }
            catch (Exception e) {
                bSuccess = false;
                //todo: Add logging
            }

            return bSuccess;
        }

        public bool UpdateSensor(DBSensor pSensor) {
            bool bSuccess = true;
            try {
                string sUpdate = string.Format("UPDATE {0} SET {1}=@sName, {2}=@nTestParamID WHERE {3}=@nMatNum",
                DBNames.SensorTable.TableName, DBNames.SensorTable.Columns.name, DBNames.SensorTable.Columns.testparam, DBNames.SensorTable.Columns.material_num);           

                using (NpgsqlConnection pDBConnection = new NpgsqlConnection(__sConnectionString)) {
                    pDBConnection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sUpdate, pDBConnection)) {                        
                        cmd.Parameters.AddWithValue("sName", pSensor.Name);
                        cmd.Parameters.AddWithValue("nTestParamID", pSensor.TestParamID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("nMatNum", pSensor.MaterialNumber);                        
                        cmd.ExecuteNonQueryAsync();
                        pDBConnection.Close();
                    }
                }
            }
            catch (Exception e) {
                bSuccess = false;
                //todo: Add logging
            }

            return bSuccess;
        }
       

        public List<DBFullSensor> GetAllFullSensors() {            
            string sSelect = string.Format("SELECT * FROM {0}", DBNames.SensorTestParamView.ViewName);            
            return GetCollectionFromDB<DBFullSensor>(__sConnectionString, sSelect, ReadConvertFullSensorFromDB);
        }

        public List<DBSensor> GetAllSensors() {
            string sSelect = string.Format("SELECT * FROM {0}", DBNames.SensorTable.TableName);
            return GetCollectionFromDB<DBSensor>(__sConnectionString, sSelect, ReadConvertSensorFromDB);
        }

        private static DBSensor ReadConvertSensorFromDB(NpgsqlDataReader pReader) {
            //We're not using tryparse here for a reason. We don't want to have any broken records coming through the line to the test setup. Break early instead of breaking expensive
            long nMatNum = Int64.Parse(pReader[0].ToString());
            string sSensorName = pReader[1].ToString();
            long? nTestParamID = pReader[2] == DBNull.Value ? null : (long?)pReader[2];

            return new DBSensor(nMatNum, sSensorName, nTestParamID);            
        }
        

        /// <summary>
        /// Used for reading data from generic Get method for collections
        /// </summary>
        /// <param name="pReader">DB Reader returned from the select query</param>
        /// <returns></returns>
        private static DBFullSensor ReadConvertFullSensorFromDB(NpgsqlDataReader pReader) {                        
            //We're not using tryparse here for a reason. We don't want to have any broken records coming through the line to the test setup. Break early instead of breaking expensive
            long nMatNum = Int64.Parse(pReader[0].ToString());
            string sSensorName = pReader[1].ToString();
            long? nTestParamID = pReader[2] == DBNull.Value ? null : (long?)pReader[2];

            DBSensor pSensor = new DBSensor(nMatNum, sSensorName, nTestParamID);

            DBTestParameter pTestParam = null;

            if (nTestParamID != null) {
                string sTestParamName = pReader[3].ToString();
                int nPreTestMS = Int32.Parse(pReader[4].ToString());
                int nTime = Int32.Parse(pReader[5].ToString());
                double nHighLimitmV = Double.Parse(pReader[6].ToString());
                double nLowLimitmV = Double.Parse(pReader[7].ToString());

                pTestParam = new DBTestParameter(nTestParamID.Value, sTestParamName, nPreTestMS, nTime, nHighLimitmV, nLowLimitmV);
            }

            return new DBFullSensor { Sensor = pSensor, TestParam = pTestParam };
        }
    }
}              