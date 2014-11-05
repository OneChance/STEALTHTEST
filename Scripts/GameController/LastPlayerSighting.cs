using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour
{
		public Vector3 position = new Vector3 (1000f, 1000f, 1000f);
		public Vector3 resetPosition = new Vector3 (1000f, 1000f, 1000f);
		public float lightHighIntensity = 0.25f;
		public float lightLowIntensity = 0f;
		public float lightFadeSpeed = 7f;
		public float musicFadeSpeed = 1f;
		private AlarmLight alarm;
		private Light mainLight;
		private AudioSource panicAudio;
		private AudioSource[] sirens;
		
		void Awake ()
		{
				alarm = GameObject.FindGameObjectWithTag (Tags.alarm).GetComponent<AlarmLight> ();
				mainLight = GameObject.FindGameObjectWithTag (Tags.mainLight).GetComponent<Light> ();
				panicAudio = transform.Find ("secondaryMusic").audio;
				GameObject[] gos = GameObject.FindGameObjectsWithTag (Tags.siren);
				sirens = new AudioSource[gos.Length];
				for (int i=0; i<gos.Length; i++) {
						sirens [i] = gos [i].audio;
				}
		}

		void Update ()
		{
				SwitchAlarm ();
				SwitchMusic ();
		}

		void SwitchAlarm ()
		{
				alarm.alarmOn = (position != resetPosition);
				
				float newIntensity;

				if (alarm.alarmOn) {
						newIntensity = lightLowIntensity;
				} else {
						newIntensity = lightHighIntensity;
				}

				mainLight.intensity = Mathf.Lerp (mainLight.intensity, newIntensity, lightFadeSpeed * Time.deltaTime);

				foreach (AudioSource aus in sirens) {
						if (alarm.alarmOn && !aus.isPlaying) {
								aus.Play ();
						} else if (!alarm.alarmOn) {
								aus.Stop ();
						}
				}
		}

		void SwitchMusic ()
		{
				if (position != resetPosition) {
						audio.volume = Mathf.Lerp (audio.volume, 0, musicFadeSpeed * Time.deltaTime);
						panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
				} else {
						audio.volume = Mathf.Lerp (audio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
						panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
				}
		}
}
