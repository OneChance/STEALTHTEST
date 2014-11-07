using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour
{
		public AudioClip keyGet;
		private GameObject player;
		private PlayerInventory playerInventory;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				playerInventory = player.GetComponent<PlayerInventory> ();
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject == player) {
						AudioSource.PlayClipAtPoint (keyGet, transform.position, 0.5f);
						playerInventory.hasKey = true;
						Destroy (gameObject);
				}			
		}
}
