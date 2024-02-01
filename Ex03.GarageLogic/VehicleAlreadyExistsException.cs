using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    public class VehicleAlreadyExistsException : Exception
    {
        private string m_LicenseNumber;

        public VehicleAlreadyExistsException(Exception i_InnerException, string i_LicenseNumber) : base(String.Format("Vehicle with license number {0} already exists in the garage. Vehicle status changed to 'In repair'", i_LicenseNumber), i_InnerException)
        {
            m_LicenseNumber = i_LicenseNumber;
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
        }
    }
}