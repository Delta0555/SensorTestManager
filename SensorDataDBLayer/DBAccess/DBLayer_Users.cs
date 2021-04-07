using Npgsql;
using System;
using System.Collections.Generic;

namespace SensorData.DBLayer {
    public partial class DBLayer {
        public List<DBUser> GetAllUsers() {
            string sSelect = string.Format("SELECT * FROM {0}",
                DBNames.UserTable.TableName);

            return GetCollectionFromDB<DBUser>(__sConnectionString, sSelect, ReadConvertUserFromDB);
        }

        public List<DBServiceRole> GetRolesForUser(int nUserID) {
            //string sSelect = string.Format("select * from serviceusers.roles JOIN serviceusers.usertoroles on serviceusers.roles.roleid = serviceusers.usertoroles.roleid WHERE serviceusers.usertoroles.userid = 1 ORDER BY serviceusers.roles.roleid");
            string sSelect = string.Format("SELECT * FROM {0} JOIN {1} ON {0}.{2} = {1}.{3} WHERE {1}.{4} = @nUserID ORDER BY {0}.{2}",
                DBNames.ServiceRoleTable.TableName,
                DBNames.UserToRolesTable.TableName,
                DBNames.ServiceRoleTable.Columns.roleid,
                DBNames.UserToRolesTable.Columns.roleid,
                DBNames.UserToRolesTable.Columns.userid);

            List<NpgsqlParameter> pSelectParams = new List<NpgsqlParameter> { new NpgsqlParameter("nUserID", nUserID) };

            return GetCollectionFromDB<DBServiceRole>(__sConnectionString, sSelect, ReadConvertServiceRoleFromDB, pSelectParams);
        }

        private static DBUser ReadConvertUserFromDB(NpgsqlDataReader pReader) {
            int nID = Int32.Parse(pReader[0].ToString());
            string sLogin = pReader[1].ToString();
            string sPass = pReader[2].ToString();

            return new DBUser(nID, sLogin, sPass);
        }

        private static DBServiceRole ReadConvertServiceRoleFromDB(NpgsqlDataReader pReader) {
            string sName = pReader[0].ToString();
            int nID = Int32.Parse(pReader[1].ToString());          
            string sDescription = pReader[2].ToString();

            return new DBServiceRole(nID, sName, sDescription);
        }
    }
}
