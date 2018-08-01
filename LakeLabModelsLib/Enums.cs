using System;
using System.Collections.Generic;
using System.Text;

namespace LakeLabLib
{
    public class Enums
    {
        public enum SensorTypes
        {
            Dissolved_Oxygen,
            Temperature
        };

        public enum Depth
        {
            Oberfläche,
            Mitte,
            Grund,
            NotDefined
        }
    }
}
