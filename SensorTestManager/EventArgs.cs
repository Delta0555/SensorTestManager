using System;

namespace SensorData.TestManager {
    public class TestParameterArgs : EventArgs {
        public TestParameterViewModel TestParams { get; set; }
    }

    public class SensorArgs : EventArgs {
        public FullSensorViewModel Sensor { get; set; }
    }

    public class OpModeChangedArgs : EventArgs {
        public OpMode Mode { get; set; }
    }
}
