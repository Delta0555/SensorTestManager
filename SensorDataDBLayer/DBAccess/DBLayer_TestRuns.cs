using Npgsql;
using System;
using System.Collections.Generic;

namespace SensorData.DBLayer {
    public partial class DBLayer {

        public void InsertNewTestRun(DBTestRun pTestRun) {
         
            try {
                using (NpgsqlConnection pDBConnection = new NpgsqlConnection(__sConnectionString)) {
                    pDBConnection.Open();


                    string sInsert = string.Format("INSERT INTO {0} ({1}, {2}, {3},{4}) VALUES (@sSensorMatNum,@pDate,@fMeasuredmV,@bSucceeded)",
                        DBNames.TestRunTable.TableName, DBNames.TestRunTable.Columns.sensor_matnum, DBNames.TestRunTable.Columns.date, DBNames.TestRunTable.Columns.measured_mv,
                        DBNames.TestRunTable.Columns.succeeded);

                    // Insert some data
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sInsert, pDBConnection)) {

                        cmd.Parameters.AddWithValue("sSensorMatNum", pTestRun.SensorMaterialNumber);
                        cmd.Parameters.AddWithValue("pDate", pTestRun.EpochStart);
                        cmd.Parameters.AddWithValue("fMeasuredmV", pTestRun.MeasuredValuemV);
                        cmd.Parameters.AddWithValue("bSucceeded", pTestRun.Succeeded);                       
                       cmd.ExecuteNonQuery();   
                    }
                }

            }
            catch (Exception e) {
                //todo: Add logging
            }
        }

        public void UpdateTestRun(DBTestRun pUpdateRun) {
            throw new NotImplementedException();
        }

        public List<DBTestRun> GetTestRunsForSensor(long nSensorID) {
            string sSelect = string.Format("SELECT * FROM {0} WHERE {1} = @nSensorID",
                DBNames.TestRunTable.TableName, DBNames.TestRunTable.Columns.sensor_matnum);

            List<NpgsqlParameter> pSelectParams = new List<NpgsqlParameter> { new NpgsqlParameter("nSensorID", nSensorID) };

            return GetCollectionFromDB<DBTestRun>(__sConnectionString, sSelect, ReadConvertTestRunFromDB, pSelectParams);
        }


        public List<DBFullTestRun> GetAllFullTestRuns() {
            string sSelect = string.Format("SELECT * FROM {0}",
                DBNames.AllTestResults.ViewName);

            return GetCollectionFromDB<DBFullTestRun>(__sConnectionString, sSelect, ReadConvertFullTestRunFromDB);
        }

        public List<DBFullTestRun> GetTestRunFor(long nMaterialNumber) {
            throw new NotImplementedException();
        }


        private static DBTestRun ReadConvertTestRunFromDB(NpgsqlDataReader pReader) {
            //We're not using tryparse here for a reason. We don't want to have any broken records coming through the line to the test setup. Break early instead of breaking expensive
            long nEpochTime = Int64.Parse(pReader[0].ToString());
            long nSensorMatNum = Int64.Parse(pReader[1].ToString());
            double? fMeasuredValuemV = pReader[2] == DBNull.Value ? null : (double?)pReader[2];
            bool? bSucceeded = pReader[3] == DBNull.Value ? null : (bool?)pReader[3];

            return new DBTestRun(nEpochTime, nSensorMatNum, fMeasuredValuemV, bSucceeded);
        }

        private static DBFullTestRun ReadConvertFullTestRunFromDB(NpgsqlDataReader pReader) {
            long nMatNum = Int64.Parse(pReader[0].ToString());
            string sSensorName = pReader[1].ToString();
            long nTestParamID = Int64.Parse(pReader[2].ToString());
            string sTestParamName = pReader[3].ToString();
            int nPreTestMS = Int32.Parse(pReader[4].ToString());
            int nTime = Int32.Parse(pReader[5].ToString());
            double nHighLimitmV = Double.Parse(pReader[6].ToString());
            double nLowLimitmV = Double.Parse(pReader[7].ToString());
            DateTime pDate = DateTime.Parse(pReader[8].ToString());
            double? fMeasuredValuemV = pReader[9] == DBNull.Value ? null : (double?)pReader[9];
            bool? bSucceeded = pReader[10] == DBNull.Value ? null : (bool?)pReader[10];

            return new DBFullTestRun(nMatNum, sSensorName,nTestParamID,sTestParamName,nPreTestMS,nTime,nHighLimitmV,nLowLimitmV,pDate,fMeasuredValuemV,bSucceeded);
        }
    }
}
