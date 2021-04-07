namespace SensorData.DBLayer {

    /// <summary>
    /// Holds all table names and column names for the underlying db schema.
    /// Makes sure that they are defined only once, avoiding typos and hard to find errors
    /// </summary>
    internal static class DBNames {

        public static class SensorTestParamView {
            public const string ViewName = "sensor_testparams";
        }

        public static class AllTestResults {
            public const string ViewName = "all_testresults";
        }

        public  static class SensorTable {
            public const string TableName = "sensor";
            
            public enum Columns {
                material_num,
                name,
                testparam
            }
        }

        public static class UserTable {
            public const string TableName = "serviceusers.users";

            public enum Columns {
                id,
                login,
                pass
            }
        }

        public static class ServiceRoleTable {
            public const string TableName = "serviceusers.roles";

            public enum Columns {
                roleid,
                name,
                description
            }
        }

        public static class UserToRolesTable {
            public const string TableName = "serviceusers.usertoroles";


            public enum Columns {
                userid,
                roleid

            }
        }

        public static class TestParamTable {
            public const string TableName = "testparameter";
           
            public enum Columns {
                id,
                name,
                pretest_ms,
                time_ms,
                highlimit_mv,
                lowlimit_mv
            }
        }

            public static class TestRunTable {
            public const string TableName = "testrun";

            public enum Columns {
                sensor_matnum,
                date,
                measured_mv,
                succeeded
            }
        }
    }
}
