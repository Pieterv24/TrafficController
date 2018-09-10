using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Models
{
    [Serializable]
    public class Trigger
    {
        public string type;
        public bool triggered;
        public string id;
    }
}
