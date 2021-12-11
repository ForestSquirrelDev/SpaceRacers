using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	[System.Serializable]
	public class PID2 {
		public float pFactor, iFactor, dFactor;

		private float integral;
		private float lastError;


		public PID2(float pFactor, float iFactor, float dFactor) {
			this.pFactor = pFactor;
			this.iFactor = iFactor;
			this.dFactor = dFactor;
		}


		public float Update(float setpoint, float actual, float timeFrame) {
			float present = setpoint - actual;
			integral += present * timeFrame;
			float deriv = (present - lastError) / timeFrame;
			lastError = present;
			return present * pFactor + integral * iFactor + deriv * dFactor;
		}
	}
}
