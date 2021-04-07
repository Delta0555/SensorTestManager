using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensorData.DBLayer;
using SensorData.ServiceClasses;
using SensorDataService.Authentication;
using System.Collections.Generic;


namespace SensorDataService.Controllers {
    [ApiController]
   [Authorize]
    [Route("Sensor")]
    public class SensorController : ControllerBase {

        private readonly IDBLayer __pDB;

        public SensorController(IDBLayer pDBLayer) {
            __pDB = pDBLayer;
        }

        [HttpPut]
        public bool UpdateSensor(Sensor pSensor) {
            DBSensor pDBSensor = DBConversionHelper.ToDBSensor(pSensor);
            return __pDB.UpdateSensor(pDBSensor);
        }


        [HttpPost]
        public bool InsertNewSensor(Sensor pSensor) {
            DBSensor pDBSensor = DBConversionHelper.ToDBSensor(pSensor);
            return __pDB.InsertNewSensor(pDBSensor);
        } 

        [HttpGet]
        [Authorize(Roles = "Admin, ResultCreator")]       
        [Route("Full")]
        public IEnumerable<FullSensor> GetAllFullSensors() {
            List<DBFullSensor> pDBFullSensors =  __pDB.GetAllFullSensors();
            return DBConversionHelper.FromDBFullSensor(pDBFullSensors);
        }

        [HttpGet]
        public IEnumerable<Sensor> GetAllSensors() {
            List<DBSensor> pSensors = __pDB.GetAllSensors();
            return DBConversionHelper.FromDBSensor(pSensors);
        }
    }
}
