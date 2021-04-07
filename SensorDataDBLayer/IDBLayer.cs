using System.Collections.Generic;

namespace SensorData.DBLayer {
    public interface IDBLayer {
        List<DBTestParameter> GetAllTestParameters();

        List<DBFullSensor> GetAllFullSensors();
        List<DBSensor> GetAllSensors();

        List<DBFullTestRun> GetAllFullTestRuns();

        List<DBFullTestRun> GetTestRunFor(long nMaterialNumber);

        void InsertNewTestRun(DBTestRun pTestRun);

        void UpdateTestRun(DBTestRun pUpdateRun);
        DBTestParameter GetTestParameter(long nParamID);
        long InsertNewTestParameter(DBTestParameter pNewParam);
        bool UpdateTestParameter(DBTestParameter pDBParam);

        bool UpdateSensor(DBSensor pSensor);
        bool InsertNewSensor(DBSensor dBSensor);
    }
}
