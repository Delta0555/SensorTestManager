using System;
using System.Collections.Generic;

namespace SensorData.DBLayer {
    public class DBSensor {
        public long MaterialNumber { get; private set; }

        public string Name { get; set; }

        public long? TestParamID {
            get; set;
        }        

        public DBSensor(long nMaterialNumber, string sName, long? nTestParamID = null) {
            MaterialNumber = nMaterialNumber;
            Name = sName;
            TestParamID = nTestParamID;
        }
    }

    public class DBFullSensor {
        public DBSensor Sensor { get; set; }

        public DBTestParameter TestParam { get; set; }
    }

    public class DBUser {
        public int UserID { get; set; }

        public string Login { get; set; }

        //todo: Make this one encrypted!!!!
        public string Pass { get; set; }        

        public DBUser(int nID, string sLogin, string sPass) {
            UserID = nID;
            Login = sLogin;
            Pass = sPass;
        }
    }

    public class DBFullUser {
        public DBUser User { get; private set; }

        public List<DBServiceRole> Roles { get; private set; }

        public DBFullUser(DBUser pUser, List<DBServiceRole> pRoles) {
            User = pUser;
            Roles = pRoles;
        }
    }


    public class DBServiceRole {
        public int ID { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public DBServiceRole(int nID, string sRole, string sDescription) {
            ID = nID;
            RoleName = sRole;
            Description = sDescription;
        }
    }

    public class UserToRoleMappings {
        public int UserID { get; set; }

        public int RoleID { get; set; }
    }

    public class DBTestParameter {
        public long ID { get; private set; }

        public string Name { get; set; }

        public int PreTestMS { get; set; }

        public int TestTimeMS { get; set; }

        public double HighLimitmV { get; set; }

        public double LowLimitmV { get; set; }

        //public DBTestParameter(string sName, int nPreTestMS, int nTestTimeMS, int nHighLimitmV, int nLowLimitmV):
        //    this(-1, sName, nPreTestMS, nTestTimeMS, nHighLimitmV, nLowLimitmV) { }

        public DBTestParameter(long nID, string sName, int nPreTestMS, int nTestTimeMS, double fHighLimitmV, double fLowLimitmV) {
            ID = nID;
            Name = sName;
            PreTestMS = nPreTestMS;
            TestTimeMS = nTestTimeMS;
            HighLimitmV = fHighLimitmV;
            LowLimitmV = fLowLimitmV;
        }
    }

    public class DBTestRun {
        public long EpochStart { get; private set; }

        public DateTime Start { get; private set; }

        public long SensorMaterialNumber { get; set; }

        public double? MeasuredValuemV { get; set; }

        public bool? Succeeded { get; set; }

        public DBTestRun(long nEpochStart, long nSensorMatNumber, double? fMeasuredValuemV, bool? bSucceeded) {
            EpochStart = nEpochStart;
            Start = DateTimeOffset.FromUnixTimeSeconds(nEpochStart).DateTime;
            SensorMaterialNumber = nSensorMatNumber;
            MeasuredValuemV = MeasuredValuemV;
            Succeeded = bSucceeded;
        }
    }

    public class DBFullTestRun {
        public long MaterialNumber { get; set; }

        public string SensorName { get; set; }

        public long TestParamID { get; set; }

        public string TestName { get; set; }

        public int PreTestMS { get; set; }

        public int TestTimeMS { get; set; }

        public double HighLimitmV { get; set; }

        public double LowLimitmV { get; set; }

        public DateTime TestStart { get; private set; }

        public double? MeasuredValuemV { get; set; }

        public bool? Succeeded { get; set; }

        public DBFullTestRun(long nMateriaNumber, string sSensorName, long nTestParamID, string sTestName, int nPreTestMS, 
            int nTestTimeMS, double fHighLimitmV, double fLowLimitmV, DateTime pStart, double? fMeasuredValue, bool? bSucceeded) {
            MaterialNumber = nMateriaNumber;
            SensorName = sSensorName;
            TestParamID = nTestParamID;
            TestName = sTestName;
            PreTestMS = nPreTestMS;
            TestTimeMS = nTestTimeMS;
            HighLimitmV = fHighLimitmV;
            LowLimitmV = fLowLimitmV;
            TestStart = pStart;
            MeasuredValuemV = fMeasuredValue;
            Succeeded = bSucceeded;
        }
    }
}
