using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public ValueOutOfRangeException(Exception i_InnerException, float i_MinValue, float i_MaxValue) : base(String.Format("Value out of range. Range is: {0} - {1}", i_MinValue, i_MaxValue), i_InnerException)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }

        public float MaxValue
        {
            get { return m_MaxValue; }
        }
    }
}