using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelTank : Energy
    {
        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        private eFuelType m_FuelType;
        private float m_CurrentAmountOfFuel;
        private float m_MaxAmountOfFuel;


        public FuelTank(eFuelType i_FuelType, float i_CurrentAmountOfFuel, float i_MaxAmountOfFuel)
        {
            m_FuelType = i_FuelType;
            m_CurrentAmountOfFuel = i_CurrentAmountOfFuel;
            m_MaxAmountOfFuel = i_MaxAmountOfFuel;
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        public float CurrentAmountOfFuel 
        { 
            get { return m_CurrentAmountOfFuel; } 
            set { m_CurrentAmountOfFuel = value; } 
        }

        public float MaxAmountOfFuel
        {
            get { return m_MaxAmountOfFuel; }
        }

        public void Refuel(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (m_FuelType == i_FuelType)
            {
                if (m_CurrentAmountOfFuel + i_FuelToAdd > m_MaxAmountOfFuel)
                {
                    throw new ValueOutOfRangeException(new Exception(), 0, m_MaxAmountOfFuel);
                }

                m_CurrentAmountOfFuel += i_FuelToAdd;
                UpdateRemainingEnergyPercentage();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void UpdateRemainingEnergyPercentage()
        {
            m_RemainingEnergyPercentage = (float)(m_CurrentAmountOfFuel * 100.0f / m_MaxAmountOfFuel);
        }
    }
}
