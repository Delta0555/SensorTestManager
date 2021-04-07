using Microsoft.AspNetCore.Mvc;
using SensorData.DBLayer;
using SensorData.ServiceClasses;
using System.Collections.Generic;

namespace SensorDataService.Controllers {
    [ApiController]
    [Route("TestRun")]
   
    public class TestRunController : Controller {
        // GET: TestRunController
        private readonly IDBLayer __pDB;

        public TestRunController(IDBLayer pDBLayer) {
            __pDB = pDBLayer;
        }

        [HttpGet]
        [Route("Full")]
        public List<FullTestRun> GetAllFullTestRuns() {
            List<DBFullTestRun> pRuns = __pDB.GetAllFullTestRuns();
            return DBConversionHelper.FromDBFullTestRun(pRuns);
        }
    }
}
