using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        public static void Run(Garage i_Garage)
        {
            bool isValid = false;
            int userInput = 0;

            printWelcome();

            while(userInput != 8)
            {
                printMenu();
                while (!isValid)
                {
                    try
                    {
                        isValid = UserInput.GetValidIntInRange(out userInput, 1, 8);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
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

                switch (userInput)
                {
                    case 1:
                        addVehicleToGarage(i_Garage);
                        break;
                    case 2:
                        displayLicenseNumbersByStatus(i_Garage);
                        break;
                    case 3:
                        changeVehicleStatus(i_Garage);
                        break;
                    case 4:
                        inflateVehicleToMax(i_Garage);
                        break;
                    case 5:
                        refuelVehicle(i_Garage);
                        break;
                    case 6:
                        rechargeVehicle(i_Garage);
                        break;
                    case 7:
                        displayVehicleData(i_Garage);
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        userInput = 0;
                        break;
                }

                Console.WriteLine("Enter any key to continue...");
                Console.ReadKey();
                isValid = false;
                userInput = 0;
            }
        }

        private static void printWelcome()
        {
            string welcome = String.Format("Welcome to Monique & Inbar's Garage!");
            Console.WriteLine(welcome);
        }
        private static void printMenu()
        {
            string menu = String.Format(@"
Our Menu:

1) Add a new vehicle to the garage
2) Display license numbers by status
3) Change a vehicle's status
4) Inflate a vehicle's tires to maximum
5) Refuel a vehicle
6) Recharge a vehicle
7) Display a vehicle's data
8) Exit

Please choose the desired action by entering the action number");

            Console.WriteLine(menu);
        }

        private static void addVehicleToGarage(Garage i_Garage)
        {
            string ownerName;
            string ownerPhoneNumber;
            VehicleData vehicleData;

            UserInput.GetOwnerNameFromUser(out ownerName);
            UserInput.GetPhoneNumberFromUser(out ownerPhoneNumber);
            try
            {
                vehicleData = UserInput.GetVehicleDataFromUser();
                i_Garage.AddClientCard(ownerName, ownerPhoneNumber, vehicleData);
                Console.WriteLine("Vehicle added successfully");
            }

            catch (VehicleAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void displayLicenseNumbersByStatus(Garage i_Garage)
        {
            ClientCard.eVehicleStatus vehicleStatus;
            List<string> licenseNumbers;
            StringBuilder output = new StringBuilder();

            UserInput.GetVehicleStatusFromUser(out vehicleStatus);
            licenseNumbers = i_Garage.GetLicenseNumbersByStatus(vehicleStatus);
            foreach (string licenseNumber in licenseNumbers)
            {
                Console.WriteLine(licenseNumber);
            }
            if (licenseNumbers.Count == 0)
            {
                Console.WriteLine("No vehicles with status {0}", vehicleStatus.ToString());
            }
            else
            {
                output.AppendLine(String.Format("Vehicles with status {0} in the garage:", vehicleStatus));
                foreach (string licenseNumber in licenseNumbers)
                {
                    output.AppendLine(licenseNumber);
                }
            }

            Console.WriteLine(output);
        }

        private static void changeVehicleStatus(Garage i_Garage)
        {
            ClientCard.eVehicleStatus newVehicleStatus;
            string licenseNumber;

            UserInput.GetVehicleStatusFromUser(out newVehicleStatus);
            UserInput.GetLicenseNumberFromUser(out licenseNumber);
            try
            {
                i_Garage.ChangeVehicleStatus(licenseNumber, newVehicleStatus);
                Console.WriteLine("Vehicle status changed successfuly to {0}", newVehicleStatus);
            }

            catch (KeyNotFoundException)
            {
                Console.WriteLine("No vehicles with license number {0} exist in the garage", licenseNumber);
            }
        }

        private static void inflateVehicleToMax(Garage i_Garage)
        {
            string licenseNumber;

            UserInput.GetLicenseNumberFromUser(out licenseNumber);
            try
            {
                i_Garage.InflateToMax(licenseNumber);
                Console.WriteLine("Vehicle wheels inflated to maximum successfuly");
            }

            catch (KeyNotFoundException)
            {
                Console.WriteLine("No vehicles with license number {0} exist in the garage", licenseNumber);
            }
        }

        private static void refuelVehicle(Garage i_Garage)
        {
            string licenseNumber;
            FuelTank.eFuelType fuelType;
            float fuelToAdd;

            UserInput.GetLicenseNumberFromUser(out licenseNumber);
            UserInput.GetFuelTypeFromUser(out fuelType);
            UserInput.GetFuelToAddFromUser(out fuelToAdd);
            try
            {
                i_Garage.Refuel(licenseNumber, fuelType, fuelToAdd);
                Console.WriteLine("Vehicle fueled successfuly");
            }

            catch (KeyNotFoundException)
            {
                Console.WriteLine("No vehicles with license number {0} exist in the garage", licenseNumber);
            }

            catch (ArgumentNullException)
            {
                Console.WriteLine("This is not a fuel based vehicle");
            }

            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("Invalid input. Cannot fuel more than the maximum capacity of {0}", ex.MaxValue);
            }

            catch (ArgumentException)
            {
                Console.WriteLine("Invalid input. Cannot fuel this vehicle with fuel type {0}", fuelType);
            }
        }

        private static void rechargeVehicle(Garage i_Garage)
        {
            string licenseNumber;
            float batteryTimeToAdd;

            UserInput.GetLicenseNumberFromUser(out licenseNumber);
            UserInput.GetBatteryTimeToAddFromUser(out batteryTimeToAdd);
            try
            {
                i_Garage.Charge(licenseNumber, batteryTimeToAdd);
                Console.WriteLine("Vehicle charged successfuly");
            }

            catch (KeyNotFoundException)
            {
                Console.WriteLine("No vehicles with license number {0} exist in the garage", licenseNumber);
            }

            catch (ArgumentNullException)
            {
                Console.WriteLine("This is not an electric vehicle");
            }

            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("Invalid input. Cannot charge more than the maximum capacity of {0}", ex.MaxValue);
            }
        }

        private static void displayVehicleData(Garage i_Garage)
        {
            string licenseNumber = String.Empty;
            ClientCard cardToDisplay;
            Vehicle vehicleToDisplay;
            VehicleCreator.eVehicleType vehicleType = 0;
            StringBuilder output = new StringBuilder();

            UserInput.GetLicenseNumberFromUser(out licenseNumber);
            try
            {
                cardToDisplay = i_Garage.FindCard(licenseNumber);
                vehicleToDisplay = cardToDisplay.Vehicle;
                vehicleType = Vehicle.GetEVehicleType(vehicleToDisplay);

                output.Append(String.Format(@"License Number: {0}
Model Name: {1}
Owner Name: {2}
Vehicle Status: {3}
Energy Percentage: {4} %", vehicleToDisplay.LicenseNumber, vehicleToDisplay.ModelName, cardToDisplay.OwnerName, cardToDisplay.VehicleStatus, vehicleToDisplay.EnergyType.RemainingEnergyPercentage));

                switch (vehicleType)
                {
                    case VehicleCreator.eVehicleType.FuelBasedCar:
                    case VehicleCreator.eVehicleType.ElectricCar:
                        if (vehicleToDisplay.EnergyType is FuelTank)
                        {
                            output.Append(String.Format(@"
Fuel Type: {0}
Fuel Tank Capacity: {1} Liters
Current Amount of Fuel: {2} Liters", ((FuelTank)vehicleToDisplay.EnergyType).FuelType, ((FuelTank)vehicleToDisplay.EnergyType).MaxAmountOfFuel, ((FuelTank)vehicleToDisplay.EnergyType).CurrentAmountOfFuel));
                        }
                        else if (vehicleToDisplay.EnergyType is Battery)
                        {
                            output.Append(String.Format(@"
Battery Capacity: {0} Hours
Remaining BatteryTime: {1} Hours", ((Battery)vehicleToDisplay.EnergyType).MaxBatteryTime, ((Battery)vehicleToDisplay.EnergyType).RemainingBatteryTime));
                        }

                        output.Append(String.Format(@"
Car Color: {0}
Number of Doors: {1}", ((Car)vehicleToDisplay).CarColor.ToString(), ((Car)vehicleToDisplay).NumberOfDoors));
                        break;
                    case VehicleCreator.eVehicleType.FuelBasedMotorcycle:
                    case VehicleCreator.eVehicleType.ElectricMotorcycle:
                        if (vehicleToDisplay.EnergyType is FuelTank)
                        {
                            output.Append(String.Format(@"
Fuel Type: {0}
Fuel Tank Capacity: {1} Liters
Current Amount of Fuel: {2} Liters", ((FuelTank)vehicleToDisplay.EnergyType).FuelType, ((FuelTank)vehicleToDisplay.EnergyType).MaxAmountOfFuel, ((FuelTank)vehicleToDisplay.EnergyType).CurrentAmountOfFuel));
                        }
                        else if (vehicleToDisplay.EnergyType is Battery)
                        {
                            output.Append(String.Format(@"
Battery Capacity: {0} Hours
Remaining BatteryTime: {1} Hours", ((Battery)vehicleToDisplay.EnergyType).MaxBatteryTime, ((Battery)vehicleToDisplay.EnergyType).RemainingBatteryTime));
                        }

                        output.Append(String.Format(@"
License Type: {0}
Engine Volume: {1}", ((Motorcycle)vehicleToDisplay).LicenseType.ToString(), ((Motorcycle)vehicleToDisplay).EngineVolume));
                        break;
                    case VehicleCreator.eVehicleType.Truck:
                        string isCooled = ((Truck)vehicleToDisplay).IsCargoCooled ? "Yes" : "No";
                        output.Append(String.Format(@"
Fuel Type: {0}
Fuel Tank Capacity: {1} Liters
Current Amount of Fuel: {2} Liters
Cargo is Cooled? {3}
Cargo Tank Volume: {4}"

, ((FuelTank)vehicleToDisplay.EnergyType).FuelType, ((FuelTank)vehicleToDisplay.EnergyType).MaxAmountOfFuel, ((FuelTank)vehicleToDisplay.EnergyType).CurrentAmountOfFuel, isCooled, ((Truck)vehicleToDisplay).TankVolume));
                        break;

                    default:
                        break;
                }

                output.AppendLine(@"

Wheels Information:");
                for (int i = 0; i < vehicleToDisplay.Wheels.Count; i++)
                {
                    output.Append(String.Format(@"
Wheel {0}:
Manufacturer: {1}
Max Air Pressure: {2}
Current Air Pressure: {3}
", (i + 1), vehicleToDisplay.Wheels[i].ManufacturerName, vehicleToDisplay.Wheels[i].MaxAirPressure, vehicleToDisplay.Wheels[i].CurrentAirPressure));
                }
            }

            catch (KeyNotFoundException)
            {
                Console.WriteLine("No vehicles with license number {0} exist in the garage", licenseNumber);
            }

            Console.WriteLine(output);
        }
    }
}
