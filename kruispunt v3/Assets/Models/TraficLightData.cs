using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Models
{
    [Serializable]
    public class TraficLightData
    {
        public string type;
        public LightInfo[] trafficLights;
    }
}
