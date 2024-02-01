using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ex03.GarageLogic
{
    public class Garage
    {
        List<ClientCard> m_ClientCards;

        public Garage()
        {
            m_ClientCards = new List<ClientCard>();
        }

        public ClientCard FindCard(string i_LicenseNumber)
        {
            ClientCard o_Card = null;
            o_Card = m_ClientCards.Find(card => card.Vehicle.LicenseNumber.Equals(i_LicenseNumber));
            if (o_Card == null)
            {
                throw new KeyNotFoundException();
            }

            return o_Card;
        }

        public void AddClientCard(string i_owner, string i_PhoneNumber, VehicleData i_VehicleData)
        {
            ClientCard existingCard, newClientCard;
            Vehicle newVehicle;

            try
            {
                existingCard = FindCard(i_VehicleData.m_VehicleLicenseNumber);
            }

            catch (KeyNotFoundException)
            {
                existingCard = null;
            }

            if (existingCard == null)
            {
                newVehicle = VehicleCreator.CreateVehicle(i_VehicleData);
                newClientCard = new ClientCard(i_owner, i_PhoneNumber, ClientCard.eVehicleStatus.InRepair, newVehicle);
                m_ClientCards.Add(newClientCard);
            }

            else
            {
                existingCard.VehicleStatus = ClientCard.eVehicleStatus.InRepair;
                throw new VehicleAlreadyExistsException(new Exception(), existingCard.Vehicle.LicenseNumber);
            }
        }

        public List<string> GetLicenseNumbersByStatus(ClientCard.eVehicleStatus i_VehicleStatus)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (ClientCard card in m_ClientCards)
            {
                if (card.VehicleStatus == i_VehicleStatus)
                {
                    licenseNumbers.Add(card.Vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, ClientCard.eVehicleStatus i_NewStatus)
        {
            ClientCard clientCard = FindCard(i_LicenseNumber);
            clientCard.VehicleStatus = i_NewStatus;
        }

        public void InflateToMax(string i_LicenseNumber)
        {
            ClientCard clientCard = FindCard(i_LicenseNumber);
            foreach (Wheel wheel in clientCard.Vehicle.Wheels)
            {
                wheel.inflateToMax();
            }
        }

        public void Refuel(string i_LicenseNumber, FuelTank.eFuelType i_FuelType, float i_FuelToAdd)
        {
            FuelTank fuelTankToFill = null;
            ClientCard clientCard = FindCard(i_LicenseNumber);

            fuelTankToFill = clientCard.Vehicle.EnergyType as FuelTank;
            if (fuelTankToFill == null)
            {
                throw new ArgumentNullException();
            }

            fuelTankToFill.Refuel(i_FuelToAdd, i_FuelType);
        }

        public void Charge(string i_LicenseNumber, float i_MinutesToCharge)
        {
            Battery batteryToCharge = null;
            ClientCard clientCard = FindCard(i_LicenseNumber);

            batteryToCharge = clientCard.Vehicle.EnergyType as Battery;
            if (batteryToCharge == null)
            {
                throw new ArgumentNullException();
            }

            batteryToCharge.Recharge(i_MinutesToCharge / 60);
        }
    }
}
