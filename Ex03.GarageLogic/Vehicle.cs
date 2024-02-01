using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private List<Wheel> m_Wheels;
        private Energy m_EnergyType;

        public Vehicle(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheels, Energy i_EnergyType)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_Wheels = i_Wheels;
            m_EnergyType = i_EnergyType;
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public Energy EnergyType
        {
            get { return m_EnergyType; }
        }

        public static VehicleCreator.eVehicleType GetEVehicleType(Vehicle i_Vehicle)
        {
            VehicleCreator.eVehicleType vehicleType;

            if (i_Vehicle is Car && i_Vehicle.EnergyType is FuelTank)
            {
                vehicleType = VehicleCreator.eVehicleType.FuelBasedCar;
            }

            else if (i_Vehicle is Car && i_Vehicle.EnergyType is Battery)
            {
                vehicleType = VehicleCreator.eVehicleType.ElectricCar;
            }

            else if (i_Vehicle is Motorcycle && i_Vehicle.EnergyType is FuelTank)
            {
                vehicleType = VehicleCreator.eVehicleType.FuelBasedMotorcycle;
            }

            else if (i_Vehicle is Motorcycle && i_Vehicle.EnergyType is Battery)
            {
                vehicleType = VehicleCreator.eVehicleType.ElectricMotorcycle;
            }

            else if (i_Vehicle is Truck)
            {
                vehicleType = VehicleCreator.eVehicleType.Truck;
            }
            else
            {
                vehicleType = 0;
            }

            return vehicleType;
        }
    }
}
