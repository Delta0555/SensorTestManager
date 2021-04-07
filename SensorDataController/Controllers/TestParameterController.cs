using Microsoft.AspNetCore.Mvc;
using SensorData.DBLayer;
using SensorData.ServiceClasses;
using System.Collections.Generic;

namespace SensorDataService.Controllers {
    [ApiController]
    [Route("TestParameter")]
    public class TestParameterController : ControllerBase {

        private readonly IDBLayer __pDB;

        public TestParameterController(IDBLayer pDBLayer) {
            __pDB = pDBLayer;
        }

        [HttpGet]
        public IEnumerable<TestParameter> Get() {
            List<DBTestParameter> pDBTestParams = __pDB.GetAllTestParameters();

            return DBConversionHelper.FromDBParam(pDBTestParams);
        }

        [HttpGet("{id}")]
        public TestParameter GetTestParameter(long nParamID) {
           DBTestParameter pParam = __pDB.GetTestParameter(nParamID);

            return DBConversionHelper.FromDBParam(pParam);
        }

        [HttpPut]
        public bool  UpdateTestParameter(TestParameter pUpdateParam) {
            DBTestParameter pDBParam = DBConversionHelper.ToDBParam(pUpdateParam);
            return __pDB.UpdateTestParameter(pDBParam);
        }

        [HttpPost]
        public long AddNewTestParameter(TestParameter pNewParam) {
            DBTestParameter pDBParam = DBConversionHelper.ToDBParam(pNewParam);
            long nNewID = __pDB.InsertNewTestParameter(pDBParam);

            return nNewID;
        }
    }
}
