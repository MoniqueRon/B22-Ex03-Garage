using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInput
    {
        public static bool GetValidIntInRange(out int o_UserInput, int i_Min, int i_Max)
        {
            string inputString;

            inputString = Console.ReadLine();
            if (!int.TryParse(inputString, out o_UserInput))
            {
                throw new FormatException();
            }

            if (o_UserInput < i_Min || o_UserInput > i_Max)
            {
                throw new ValueOutOfRangeException(new Exception(), i_Min, i_Max);
            }

            return true;
        }

        private static bool getValidFloatInRange(out float o_UserInput, float i_Min, float i_Max)
        {
            string inputString;

            inputString = Console.ReadLine();
            if (!float.TryParse(inputString, out o_UserInput))
            {
                throw new FormatException();
            }

            if (o_UserInput < i_Min || o_UserInput > i_Max)
            {
                throw new ValueOutOfRangeException(new Exception(), i_Min, i_Max);
            }

            return true;
        }

        private static bool getValidString(out string o_UserInput)
        {
            string inputString;

            inputString = Console.ReadLine();
            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException();
            }

            o_UserInput = inputString;
            return true;
        }

        private static bool getValidName(out string o_Name)
        {
            string inputString;

            inputString = Console.ReadLine();
            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException();
            }

            if (!inputString.All(char.IsLetter))
            {
                throw new FormatException();
            }

            o_Name = inputString;
            return true;
        }

        private static bool getValidPhoneNumber(out string o_PhoneNumber)
        {
            string inputString;

            inputString = Console.ReadLine();
            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException();
            }
            else if (inputString.Length != 10)
            {
                throw new ValueOutOfRangeException(new Exception(), 10, 10);
            }

            foreach (char c in inputString)
            {
                if (c < '0' || c > '9')
                {
                    throw new FormatException();
                }
            }

            o_PhoneNumber = inputString;
            return true;
        }

        public static void GetPhoneNumberFromUser(out string o_PhoneNumber)
        {
            bool isValid = false;
            string userInput = string.Empty;

            Console.WriteLine("Please enter the owner's phone number");
            while (!isValid)
            {
                try
                {
                    isValid = getValidPhoneNumber(out userInput);
                }

                catch (ArgumentNullException)
                {
                    Console.WriteLine("Invalid input. Phone number can't be empty. Please enter the owner's phone number");
                }

                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Invalid input. Phone nuumber must be 10 digits");
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Phone nuumber must be 10 digits");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_PhoneNumber = userInput;
        }

        public static void GetOwnerNameFromUser(out string o_OwnerName)
        {
            bool isValid = false;
            string userInput = string.Empty;

            Console.WriteLine("Please enter the owner's name");
            while (!isValid)
            {
                try
                {
                    isValid = getValidName(out userInput);
                }

                catch (ArgumentNullException)
                {
                    Console.WriteLine("Invalid input. Owner name can't be empty. Please enter the owner's name");
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Owner name must contain only letters");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_OwnerName = userInput;
        }

        public static VehicleData GetVehicleDataFromUser()
        {
            VehicleData vehicleData = new VehicleData();

            getModelNameFromUser(out vehicleData.m_VehicleModelName);
            GetLicenseNumberFromUser(out vehicleData.m_VehicleLicenseNumber);
            getVehicleTypeFromUser(out vehicleData.m_VehicleType);
            getWheelsManufacturerNameFromUser(out vehicleData.m_WheelManufacturerName);
            getWheelsCurrentAirPressureFromUser(vehicleData.m_VehicleType, out vehicleData.m_WheelCurrentAirPressure);
            getDataByVehicleTypeFromUser(vehicleData);

            return vehicleData;
        }

        public static void getDataByVehicleTypeFromUser(VehicleData o_VehicleData)
        {
            VehicleCreator.eVehicleType vehicleType = o_VehicleData.m_VehicleType;

            switch (vehicleType)
            {
                case VehicleCreator.eVehicleType.FuelBasedCar:
                case VehicleCreator.eVehicleType.ElectricCar:
                    getCarColorFromUser(out o_VehicleData.m_CarColor);
                    getCarNumberOfDoorsFromUser(out o_VehicleData.m_CarNumberOfDoors);

                    if (o_VehicleData.m_VehicleType == VehicleCreator.eVehicleType.FuelBasedCar)
                    {
                        getCurrentAmountOfFuelFromUser(out o_VehicleData.m_CurrentAmountOfFuel, Car.sr_MaxAmountOfFuel);
                    }

                    else if (o_VehicleData.m_VehicleType == VehicleCreator.eVehicleType.ElectricCar)
                    {
                        getRemainingBatteryTimeFromUser(out o_VehicleData.m_RemainingBatteryTime, Car.sr_MaxBatteryTime);
                    }

                    break;

                case VehicleCreator.eVehicleType.FuelBasedMotorcycle:
                case VehicleCreator.eVehicleType.ElectricMotorcycle:
                    getMotorcycleLicenseTypeFromUser(out o_VehicleData.m_MotorcycleLicenseType);
                    getMotorcycleEngineVolumeFromUser(out o_VehicleData.m_MotorcycleEngineVolume);

                    if (o_VehicleData.m_VehicleType == VehicleCreator.eVehicleType.FuelBasedMotorcycle)
                    {
                        getCurrentAmountOfFuelFromUser(out o_VehicleData.m_CurrentAmountOfFuel, Motorcycle.sr_MaxAmountOfFuel);
                    }

                    else if (o_VehicleData.m_VehicleType == VehicleCreator.eVehicleType.ElectricMotorcycle)
                    {
                        getRemainingBatteryTimeFromUser(out o_VehicleData.m_RemainingBatteryTime, Motorcycle.sr_MaxBatteryTime);
                    }

                    break;

                case VehicleCreator.eVehicleType.Truck:
                    getCurrentAmountOfFuelFromUser(out o_VehicleData.m_CurrentAmountOfFuel, Truck.sr_MaxAmountOfFuel);
                    getIsTruckCargoCooledFromUser(out o_VehicleData.m_TruckCargoCooled);
                    getTruckTankVolumeFromUser(out o_VehicleData.m_TruckTankVolume);
                    break;

                default:
                    break;
            }
        }

        private static void getModelNameFromUser(out string o_VehicleModelName)
        {
            bool isValid = false;
            string userInput = string.Empty;

            Console.WriteLine("Please enter the vehicle's model name");
            while (!isValid)
            {
                try
                {
                    isValid = getValidString(out userInput);
                }

                catch (ArgumentNullException)
                {
                    Console.WriteLine("Invalid input, model name can't be empty. Please enter the vehicle's model name");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_VehicleModelName = userInput;
        }

        public static void GetLicenseNumberFromUser(out string o_LicenseNumber)
        {
            bool isValid = false;
            string userInput = string.Empty;

            Console.WriteLine("Please enter a vehicle's license number");
            while (!isValid)
            {
                try
                {
                    isValid = getValidString(out userInput);
                }

                catch (ArgumentNullException)
                {
                    Console.WriteLine("Invalid input, license number can't be empty. Please enter a vehicle's license number");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_LicenseNumber = userInput;
        }

        private static void getVehicleTypeFromUser(out VehicleCreator.eVehicleType o_VehicleType)
        {
            bool isValid = false;
            int userInput = 0;
            string vehicleTypeOptions = String.Format(@"
Please choose the vehicle's type:
    
1) Fuel Based Car
2) Electric Car
3) Fuel Based Motorcycle
4) Electric Motorcycle
5) Truck

Please choose the vehicle type by entering the type number");

            Console.WriteLine(vehicleTypeOptions);
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 1, 5);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_VehicleType = (VehicleCreator.eVehicleType)userInput;
        }

        private static void getWheelsManufacturerNameFromUser(out string o_WheelManufacturerName)
        {
            bool isValid = false;
            string userInput = string.Empty;

            Console.WriteLine("Please enter the wheels' manufacturer name");
            while (!isValid)
            {
                try
                {
                    isValid = getValidString(out userInput);
                }

                catch (ArgumentNullException)
                {
                    Console.WriteLine("Invalid input, manufacturer name can't be empty. Please enter the wheels' manufacturer name");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_WheelManufacturerName = userInput;
        }

        private static void getWheelsCurrentAirPressureFromUser(VehicleCreator.eVehicleType i_VehicleType, out float o_WheelCurrentAirPressure)
        {
            bool isValid = false;
            float userInput = 0;
            float maxAirPressure = 0;

            switch (i_VehicleType)
            {
                case VehicleCreator.eVehicleType.FuelBasedCar:
                case VehicleCreator.eVehicleType.ElectricCar:
                    maxAirPressure = Car.sr_MaxAirPressure;
                    break;

                case VehicleCreator.eVehicleType.FuelBasedMotorcycle:
                case VehicleCreator.eVehicleType.ElectricMotorcycle:
                    maxAirPressure = Motorcycle.sr_MaxAirPressure;
                    break;

                case VehicleCreator.eVehicleType.Truck:
                    maxAirPressure = Truck.sr_MaxAirPressure;
                    break;

                default:
                    break;
            }

            Console.WriteLine("Please enter the wheels' current air pressure");
            while (!isValid)
            {
                try
                {
                    isValid = getValidFloatInRange(out userInput, 0, maxAirPressure);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number");
                }

                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_WheelCurrentAirPressure = userInput;
        }

        private static void getCurrentAmountOfFuelFromUser(out float o_CurrentAmountOfFuel, float i_MaxAmountOfFuel)
        {
            bool isValid = false;
            float userInput = 0;

            Console.WriteLine("Please enter the vehicle's current amount of fuel");
            while (!isValid)
            {
                try
                {
                    isValid = getValidFloatInRange(out userInput, 0, i_MaxAmountOfFuel);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number");
                }

                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_CurrentAmountOfFuel = userInput;
        }

        private static void getRemainingBatteryTimeFromUser(out float o_RemainingBatteryTime, float i_MaxBatteryTime)
        {
            bool isValid = false;
            float userInput = 0;

            Console.WriteLine("Please enter the vehicle's remaining battery time in hours");
            while (!isValid)
            {
                try
                {
                    isValid = getValidFloatInRange(out userInput, 0, i_MaxBatteryTime);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number");
                }

                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_RemainingBatteryTime = userInput;
        }

        private static void getCarColorFromUser(out Car.eCarColor o_CarColor)
        {
            bool isValid = false;
            int userInput = 0;
            string colorOptions = String.Format(@"
Please choose the car's color:

1) Red
2) White
3) Green
4) Blue

Please choose the car's color by entering the color number");

            Console.WriteLine(colorOptions);
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 1, 4);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_CarColor = (Car.eCarColor)userInput;
        }

        private static void getCarNumberOfDoorsFromUser(out Car.eNumberOfDoors o_CarNumberOfDoors)
        {
            bool isValid = false;
            int userInput = 0;
            string numberOfDoorsOptions = String.Format(@"
Please enter the car's number of doors:

1) Two (2)
2) Three (3)
3) Four (4)
4) Five (5)");

            Console.WriteLine(numberOfDoorsOptions);
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 2, 5);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_CarNumberOfDoors = (Car.eNumberOfDoors)userInput;
        }

        private static void getMotorcycleLicenseTypeFromUser(out Motorcycle.eLicenseType o_MotorcycleLicenseType)
        {
            bool isValid = false;
            int userInput = 0;
            string licenseTypeOptions = String.Format(@"
Please choose the motorcycle's license type:

1) A
2) A1
3) B1
4) BB

Please choose the motorcycle's license type by entering the license type number");

            Console.WriteLine(licenseTypeOptions);
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 1, 4);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_MotorcycleLicenseType = (Motorcycle.eLicenseType)userInput;
        }

        private static void getMotorcycleEngineVolumeFromUser(out int o_MotorcycleEngineVolume)
        {
            bool isValid = false;
            int userInput = 0;

            Console.WriteLine("Please enter the motorcycle's engine volume");
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 0, int.MaxValue);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }

                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Invalid input. Please enter a positive number");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_MotorcycleEngineVolume = userInput;
        }

        private static void getIsTruckCargoCooledFromUser(out bool o_TruckCargoCooled)
        {
            bool isValid = false;
            int userInput = 0;
            bool cargoIsCooled;

            string cargoIsCoolesOptions = String.Format(@"
Does the truck conttain a cooled cargo?:

1) Yes
2) No");

            Console.WriteLine(cargoIsCoolesOptions);
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 1, 2);
                }

                catch (FormatException)
                {
                    Console.WriteLine(@"Invalid input. Please enter 1 for 'Yes' or 2 for 'No'");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter {0} or {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            cargoIsCooled = (userInput == 1);
            o_TruckCargoCooled = cargoIsCooled;
        }

        private static void getTruckTankVolumeFromUser(out float o_TruckTankVolume)
        {
            bool isValid = false;
            float userInput = 0;

            Console.WriteLine("Please enter the truck's tank volume");
            while (!isValid)
            {
                try
                {
                    isValid = getValidFloatInRange(out userInput, 0, float.MaxValue);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number");
                }

                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Invalid input. Please enter a positive floating point number");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_TruckTankVolume = userInput;
        }

        public static void GetVehicleStatusFromUser(out ClientCard.eVehicleStatus o_VehicleStatus)
        {
            bool isValid = false;
            int userInput = 0;
            string statusOptions = String.Format(@"
Please choose vehicle status:

1) In Repair
2) Repaired
3) Payed

Please choose the desired vehicle status by entering the status number");

            Console.WriteLine(statusOptions);
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 1, 3);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_VehicleStatus = (ClientCard.eVehicleStatus)userInput;
        }
       

        public static void GetFuelTypeFromUser(out FuelTank.eFuelType o_FuelType)
        {
            bool isValid = false;
            int userInput = 0;
            string fuelTypeOptions = String.Format(@"
Please choose a type of fuel:

1) Octan95
2) Octan96
3) Octan98
4) Soler

Please choose the desired type of fuel by entering the fuel number");

            Console.WriteLine(fuelTypeOptions);
            while (!isValid)
            {
                try
                {
                    isValid = GetValidIntInRange(out userInput, 1, 4);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a number between {0} and {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_FuelType = (FuelTank.eFuelType)userInput;
        }

        public static void GetFuelToAddFromUser(out float o_FuelToAdd)
        {
            bool isValid = false;
            float userInput = 0;

            Console.WriteLine("Please enter how nuch fuel you would like to add");
            while (!isValid)
            {
                try
                {
                    isValid = getValidFloatInRange(out userInput, 0, float.MaxValue);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number");
                }

                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Invalid input. Please enter a positive floating point number");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_FuelToAdd = userInput;
        }

        public static void GetBatteryTimeToAddFromUser(out float o_BatteryTimeToAdd)
        {
            bool isValid = false;
            float userInput = 0;

            Console.WriteLine("Please enter how nuch fuel you would like to add");
            while (!isValid)
            {
                try
                {
                    isValid = getValidFloatInRange(out userInput, 0, float.MaxValue);
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a floating point number");
                }

                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Invalid input. Please enter a positive floating point number");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unknown error: {0}", ex.Message);
                }
            }

            o_BatteryTimeToAdd = userInput;
        }
    }
}
