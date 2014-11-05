using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour
{

		public float fadeSpeed = 2f;
		public float highIntensity = 2f;
		public float lowIntensity = 0.5f;
		public float changeMargin = 0.2f;
		public bool alarmOn;
		public float targetIntensity;
		private Light mylight;

		void Awake ()
		{
				mylight = GetComponent<Light> ();
				mylight.intensity = 0f;
				targetIntensity = highIntensity;
		}

		void Update ()
		{
				if (alarmOn) {
						mylight.intensity = Mathf.Lerp (mylight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
						checkTargetIntensity ();
				} else {
						mylight.intensity = Mathf.Lerp (mylight.intensity, 0f, fadeSpeed * Time.deltaTime);
				}
		}

		void checkTargetIntensity ()
		{
				if (Mathf.Abs (targetIntensity - mylight.intensity) < changeMargin) {
						if (targetIntensity == highIntensity) {
								targetIntensity = lowIntensity;
						} else {
								targetIntensity = highIntensity;
						}
				}
		}
}
