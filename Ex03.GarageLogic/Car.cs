using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eCarColor
        {
            Red = 1,
            White,
            Green,
            Blue
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        public static readonly int sr_NumberOfWheels = 4;
        public static readonly int sr_MaxAirPressure = 29;
        public static readonly float sr_MaxBatteryTime = 3.3f;
        public static readonly float sr_MaxAmountOfFuel = 38f;
        public static readonly FuelTank.eFuelType sr_FuelType = FuelTank.eFuelType.Octan95;
        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        public Car(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheels, Energy i_EnergyType, eCarColor i_CarColor, eNumberOfDoors i_NumberOfDoors) : base(i_ModelName, i_LicenseNumber, i_Wheels, i_EnergyType)
        {
            m_CarColor = i_CarColor;
            m_NumberOfDoors = i_NumberOfDoors;
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }
    }
}
