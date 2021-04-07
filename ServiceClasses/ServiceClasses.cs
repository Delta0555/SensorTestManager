using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.ServiceClasses {
    [JsonObject]
    public class TestParameter {
        public TestParameter(long iD, string name, int preTestMS, int testTimeMS, double highLimitmV, double lowLimitmV) {
            ID = iD;
            Name = name;
            PreTestMS = preTestMS;
            TestTimeMS = testTimeMS;
            HighLimitmV = highLimitmV;
            LowLimitmV = lowLimitmV;
        }

        public TestParameter() { }

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("preTestMS")]
        public int PreTestMS { get; set; }

        [JsonProperty("testTimeMS")]
        public int TestTimeMS { get; set; }

        [JsonProperty("highLimitmV")]
        public double HighLimitmV { get; set; }

        [JsonProperty("lowLimitmV")]
        public double LowLimitmV { get; set; }
    }

    [JsonObject]
    public class FullTestRun {

        public FullTestRun() { }

        public FullTestRun(long nMatNum, string sSensorName, long nTestParamiD, string sTestParamName,
            int nPreTestMS, int nTestTimeMS, double fHighLimitmV, double fLowLimitmV,
            double? fMeasurement, bool? bSucceeded) {
            MaterialNumber = nMatNum;
            SensorName = sSensorName;
            TestParamID = nTestParamiD;
            TestName = sTestParamName;
            PreTestMS = nPreTestMS;
            TestTimeMS = nTestTimeMS;
            HighLimitmV = fHighLimitmV;
            LowLimitmV = fLowLimitmV;
            MeasuredValue = fMeasurement;
            Succeeded = bSucceeded;
        }

        [JsonProperty("materialNumber")]
        public long MaterialNumber { get; set; }

        [JsonProperty("sensorName")]
        public string SensorName { get; set; }
        [JsonProperty("testParamId")]
        public long TestParamID { get; set; }

        [JsonProperty("testName")]
        public string TestName { get; set; }

        [JsonProperty("preTestMS")]
        public int PreTestMS { get; set; }

        [JsonProperty("testTimeMS")]
        public int TestTimeMS { get; set; }

        [JsonProperty("highLimitmV")]
        public double HighLimitmV { get; set; }

        [JsonProperty("lowLimitmV")]
        public double LowLimitmV { get; set; }

        [JsonProperty("measuredValue")]
        public double? MeasuredValue { get; set; }
        [JsonProperty("succeeded")]
        public bool? Succeeded { get; set; }
    }

    [JsonObject]
    public abstract class SensorBase {
        public SensorBase() { }

        public SensorBase(long nMatNum, string sName) {
            MaterialNumber = nMatNum;
            Name = sName;
        }
        [JsonProperty("materialNumber")]
        public long MaterialNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject]
    public class Sensor : SensorBase {
        public Sensor() { }

        public Sensor(long nMatNum, string sName, long? nTestParamID) : base(nMatNum, sName) {
            TestParamID = nTestParamID;
        }

        [JsonProperty("testParamId")]
        public long? TestParamID { get; set; }
    }

    [JsonObject]
    public class FullSensor : SensorBase {

        public FullSensor() { }
        public FullSensor(long nMatNum, string sName, TestParameter pParam) : base(nMatNum, sName) {
            TestParam = pParam;
        }
        [JsonProperty("testParam")]
        public TestParameter TestParam { get; set; }
    }
}
