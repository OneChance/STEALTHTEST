using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour
{
		public GameObject laser;
		public Material unlockMat;
		private GameObject player;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
		}

		void OnTriggerStay (Collider other)
		{
				if (other.gameObject == player) {
						if (Input.GetButton ("Switch")) {
								laserDeactivation ();
						}
				}
		}

		void laserDeactivation ()
		{
				laser.SetActive (false);
				GameObject.Find ("prop_switchUnit_screen").renderer.material = unlockMat;
				audio.Play ();
		}
}
