using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Battery : Energy
    {
        private float m_RemainingBatteryTime;
        private float m_MaxBatteryTime;

        public Battery(float i_RemainingBatteryTime, float i_MaxBatteryTime)
        {
            m_RemainingBatteryTime = i_RemainingBatteryTime;
            m_MaxBatteryTime = i_MaxBatteryTime;
        }

        public float RemainingBatteryTime
        {
            get { return m_RemainingBatteryTime; }
            set { m_RemainingBatteryTime = value; }
        }

        public float MaxBatteryTime
        {
            get { return m_MaxBatteryTime; }
        }

        public void Recharge(float i_HoursToCharge)
        {
            if (m_RemainingBatteryTime + i_HoursToCharge > m_MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(new Exception(), 0, m_MaxBatteryTime);
            }

            m_RemainingBatteryTime += i_HoursToCharge;
            UpdateRemainingEnergyPercentage();
        }

        public void UpdateRemainingEnergyPercentage()
        {
            m_RemainingEnergyPercentage = (float)(m_RemainingBatteryTime * 100.0f / m_MaxBatteryTime);
        }
    }
}
