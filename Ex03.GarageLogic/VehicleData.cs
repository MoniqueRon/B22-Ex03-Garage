using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleData
    {
        public string m_VehicleModelName;
        public string m_VehicleLicenseNumber;
        public VehicleCreator.eVehicleType m_VehicleType;

        public string m_WheelManufacturerName;
        public float m_WheelCurrentAirPressure;

        public float m_RemainingBatteryTime;
        public float m_CurrentAmountOfFuel;

        public Car.eCarColor m_CarColor;
        public Car.eNumberOfDoors m_CarNumberOfDoors;

        public Motorcycle.eLicenseType m_MotorcycleLicenseType;
        public int m_MotorcycleEngineVolume;

        public bool m_TruckCargoCooled;
        public float m_TruckTankVolume;
    }
}
