using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.DBLayer {
    public partial class DBLayer : IDBLayer {
        private readonly string __sConnectionString;

        public DBLayer(string sDBName, int nPort, string sUser, string sPW) : this("localhost", sDBName, nPort, sUser, sPW) {

        }

        public DBLayer(string sHost, string sDBName, int nPort, string sUser, string sPW) {
            __sConnectionString = string.Format("Host={0};Port={1};Username={2};Password={3};Database={4}",
                sHost, nPort, sUser,sPW,sDBName);
        }


        private static  List<T> GetCollectionFromDB<T>(string sConnection, string sSelect, Func<NpgsqlDataReader, T> pReaderConverter, List<NpgsqlParameter> pSelectParams = null) where T : class {
            List<T> pValues = null;
            using (NpgsqlConnection pDBConnection = new NpgsqlConnection(sConnection)) {
                pDBConnection.Open();
               
                using (NpgsqlCommand cmd = new NpgsqlCommand(sSelect, pDBConnection)) {

                    if(pSelectParams != null ) {                    
                        cmd.Parameters.AddRange(pSelectParams.ToArray());                    
                    }

                    // Execute the query and obtain a result set
                    NpgsqlDataReader pReader = cmd.ExecuteReader();
                    pValues = new List<T>(128);

                    // Output rows
                    while (pReader.Read()) {
                        try {
                            //We're not using tryparse here for a reason. We don't want to have any broken records coming through the line to the test setup. Break early instead of breaking expensive                            
                            pValues.Add(pReaderConverter(pReader));                            
                        }
                        catch (Exception e) {
                            //todo: Add logging
                        }
                    }
                }
            }

            return pValues ?? new List<T>(0);
        }      
    }
}
