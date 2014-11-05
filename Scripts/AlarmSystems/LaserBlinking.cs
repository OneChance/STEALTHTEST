using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour
{
		public float onTime;
		public float offTime;
		private float timer;

		void Update ()
		{
				timer += Time.deltaTime;
				if ((renderer.enabled && timer >= onTime) || (!renderer.enabled && timer >= offTime)) {
						switchBeam ();
				}
		}

		void switchBeam ()
		{
				timer = 0;
				renderer.enabled = !renderer.enabled;
				light.enabled = !light.enabled;
		}
}
