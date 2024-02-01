using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        public enum eVehicleType
        {
            FuelBasedCar = 1,
            ElectricCar,
            FuelBasedMotorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static Vehicle CreateVehicle(VehicleData i_VehicleData)
        {
            Vehicle o_NewVehicle = null;
            List<Wheel> o_Wheels;
            Energy o_Energy;

            switch (i_VehicleData.m_VehicleType)
            {
                case eVehicleType.FuelBasedCar:
                case eVehicleType.ElectricCar:
                    if (i_VehicleData.m_VehicleType == eVehicleType.FuelBasedCar)
                    {
                        o_Energy = new FuelTank(Car.sr_FuelType, i_VehicleData.m_CurrentAmountOfFuel, Car.sr_MaxAmountOfFuel);
                        ((FuelTank)o_Energy).UpdateRemainingEnergyPercentage();
                    }

                    else
                    {
                        o_Energy = new Battery(i_VehicleData.m_RemainingBatteryTime, Car.sr_MaxBatteryTime);
                        ((Battery)o_Energy).UpdateRemainingEnergyPercentage();
                    }

                    o_Wheels = CreateWheels(i_VehicleData, Car.sr_NumberOfWheels, Car.sr_MaxAirPressure);
                    o_NewVehicle = CreateCar(i_VehicleData, o_Wheels, o_Energy);
                    break;

                case eVehicleType.FuelBasedMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    if (i_VehicleData.m_VehicleType == eVehicleType.FuelBasedMotorcycle)
                    {
                        o_Energy = new FuelTank(Motorcycle.sr_FuelType, i_VehicleData.m_CurrentAmountOfFuel, Motorcycle.sr_MaxAmountOfFuel);
                        ((FuelTank)o_Energy).UpdateRemainingEnergyPercentage();
                    }

                    else
                    {
                        o_Energy = new Battery(i_VehicleData.m_RemainingBatteryTime, Motorcycle.sr_MaxBatteryTime);
                        ((Battery)o_Energy).UpdateRemainingEnergyPercentage();
                    }
                    o_Wheels = CreateWheels(i_VehicleData, Motorcycle.sr_NumberOfWheels, Motorcycle.sr_MaxAirPressure);
                    o_NewVehicle = CreateMotorcycle(i_VehicleData, o_Wheels, o_Energy);
                    break;

                case eVehicleType.Truck:
                    o_Wheels = CreateWheels(i_VehicleData, Truck.sr_NumberOfWheels, Truck.sr_MaxAirPressure);
                    o_Energy = new FuelTank(Truck.sr_FuelType, i_VehicleData.m_CurrentAmountOfFuel, Truck.sr_MaxAmountOfFuel);
                    ((FuelTank)o_Energy).UpdateRemainingEnergyPercentage();
                    o_NewVehicle = CreateTruck(i_VehicleData, o_Wheels, o_Energy);
                    break;

                default:
                    break;
            }

            return o_NewVehicle;
        }

        public static Vehicle CreateCar(VehicleData i_VehicleData, List<Wheel> i_Wheels, Energy i_EnergyType)
        {
            Vehicle o_NewCar = new Car(i_VehicleData.m_VehicleModelName, i_VehicleData.m_VehicleLicenseNumber, i_Wheels, i_EnergyType, i_VehicleData.m_CarColor, i_VehicleData.m_CarNumberOfDoors);
            
            return o_NewCar;
        }

        public static Vehicle CreateMotorcycle(VehicleData i_VehicleData, List<Wheel> i_Wheels, Energy i_EnergyType)
        {
            Vehicle o_NewMotorcycle = new Motorcycle(i_VehicleData.m_VehicleModelName, i_VehicleData.m_VehicleLicenseNumber, i_Wheels, i_EnergyType, i_VehicleData.m_MotorcycleLicenseType, i_VehicleData.m_MotorcycleEngineVolume);

            return o_NewMotorcycle;
        }

        public static Vehicle CreateTruck(VehicleData i_VehicleData, List<Wheel> i_Wheels, Energy i_EnergyType)
        {
            Vehicle o_NewTruck = new Truck(i_VehicleData.m_VehicleModelName, i_VehicleData.m_VehicleLicenseNumber, i_Wheels, i_EnergyType, i_VehicleData.m_TruckCargoCooled, i_VehicleData.m_TruckTankVolume);

            return o_NewTruck;
        }

        public static List<Wheel> CreateWheels(VehicleData i_VehicleData, int i_NumberOfWheels, float i_MaxAirPressure)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel newWheel = new Wheel(i_VehicleData.m_WheelManufacturerName, i_VehicleData.m_WheelCurrentAirPressure, i_MaxAirPressure);
                wheels.Add(newWheel);
            }

            return wheels;
        }
    }
}
