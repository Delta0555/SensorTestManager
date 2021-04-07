using SensorData.DBLayer;
using SensorData.ServiceClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SensorDataService {
    internal static class DBConversionHelper {
        internal static List<TestParameter> FromDBParam(List<DBTestParameter> pFrom) {
            List<TestParameter> pTo = null;

            try {
                pTo = pFrom.Select(p => FromDBParam(p)).ToList();
            }
            catch (Exception e) {
                //todo: Add logging
            }

            return pTo ?? new List<TestParameter>(0);
        }

        internal static List<FullSensor> FromDBFullSensor(List<DBFullSensor> pFrom) {
            List<FullSensor> pTo = null;

            try {
                pTo = pFrom.Select(p => FromDBFullSensor(p)).ToList();
            }
            catch (Exception e) {
                // todo: Add logging
            }

            return pTo ?? new List<FullSensor>(0);
        }

        internal static List<FullTestRun> FromDBFullTestRun(List<DBFullTestRun> pFrom) {
            List<FullTestRun> pTo = null;

            try {
                pTo = pFrom.Select(p => FromDBFullTestRun(p)).ToList();
            }
            catch (Exception e) {

            }

            return pTo ?? new List<FullTestRun>(0);
        }

        internal static List<Sensor> FromDBSensor(List<DBSensor> pFrom) {
            List<Sensor> pTo = null;

            try {
                pTo = pFrom.Select(p => FromDBSensor(p)).ToList();
            }
            catch (Exception e) {
                // todo: Add logging
            }

            return pTo ?? new List<Sensor>(0);
        }

        internal static FullTestRun FromDBFullTestRun(DBFullTestRun pFrom) {
            return new FullTestRun(pFrom.MaterialNumber, pFrom.SensorName, pFrom.TestParamID,
                pFrom.TestName, pFrom.PreTestMS, pFrom.TestTimeMS, pFrom.HighLimitmV,
                pFrom.LowLimitmV, pFrom.MeasuredValuemV, pFrom.Succeeded);
        }

        internal static FullSensor FromDBFullSensor(DBFullSensor pFrom) {
            TestParameter pTestParam = pFrom.TestParam == null ? null : FromDBParam(pFrom.TestParam);

            return new FullSensor(pFrom.Sensor.MaterialNumber, pFrom.Sensor.Name, pTestParam);
        }

        internal static TestParameter FromDBParam(DBTestParameter pFrom) {
            return new TestParameter(pFrom.ID, pFrom.Name, pFrom.PreTestMS, pFrom.TestTimeMS, pFrom.HighLimitmV, pFrom.LowLimitmV);
        }

        internal static DBTestParameter ToDBParam(TestParameter pFrom) {
            return new DBTestParameter(pFrom.ID, pFrom.Name, pFrom.PreTestMS, pFrom.TestTimeMS, pFrom.HighLimitmV, pFrom.LowLimitmV);
        }

        internal static Sensor FromDBSensor(DBSensor pFrom) {
            return new Sensor(pFrom.MaterialNumber, pFrom.Name, pFrom.TestParamID);
        }

        internal static DBSensor ToDBSensor(Sensor pFrom) {
            return new DBSensor(pFrom.MaterialNumber, pFrom.Name, pFrom.TestParamID);
        }        
    }
}