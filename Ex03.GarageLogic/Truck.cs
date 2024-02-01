using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {

        public static readonly int sr_NumberOfWheels = 16;
        public static readonly int sr_MaxAirPressure = 24;
        public static readonly float sr_MaxAmountOfFuel = 120;
        public static readonly FuelTank.eFuelType sr_FuelType = FuelTank.eFuelType.Soler;
        private bool m_CargoIsCooled;
        private float m_CargoTankVolume;

        public Truck(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheels, Energy i_EnergyType, bool i_IsCargoCooled, float i_TankVolume) : base(i_ModelName, i_LicenseNumber, i_Wheels, i_EnergyType)
        {
            m_CargoIsCooled = i_IsCargoCooled;
            m_CargoTankVolume = i_TankVolume;
        }

        public bool IsCargoCooled
        {
            get { return m_CargoIsCooled; }
            set { m_CargoIsCooled = value; }
        }

        public float TankVolume
        {
            get { return m_CargoTankVolume; }
        }
    }
}
