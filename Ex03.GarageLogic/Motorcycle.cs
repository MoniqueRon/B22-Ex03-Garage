using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A = 1,
            A1,
            B1,
            BB
        }

        public static readonly int sr_NumberOfWheels = 2;
        public static readonly int sr_MaxAirPressure = 31;
        public static readonly float sr_MaxBatteryTime = 2.5f;
        public static readonly float sr_MaxAmountOfFuel = 6.2f;
        public static readonly FuelTank.eFuelType sr_FuelType = FuelTank.eFuelType.Octan98;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheels, Energy i_EnergyType, eLicenseType i_LicenseType, int i_EngineVolume) : base(i_ModelName, i_LicenseNumber, i_Wheels, i_EnergyType)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
        }
    }
}
