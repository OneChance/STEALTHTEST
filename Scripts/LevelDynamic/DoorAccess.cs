using UnityEngine;
using System.Collections;

public class DoorAccess : MonoBehaviour
{
		public bool needKey;
		public AudioClip swishClip;
		public AudioClip accessDeniedClip;
		private GameObject player;
		private Animator anim;
		private PlayerInventory playerInventory;
		private HashIDs hash;
		private int enterCount;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				anim = GetComponent<Animator> ();
				playerInventory = player.GetComponent<PlayerInventory> ();
				hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		}

		void Update ()
		{
				anim.SetBool (hash.openBool, enterCount > 0);
				if (anim.IsInTransition (0) && !audio.isPlaying) {
						audio.clip = swishClip;
						audio.Play ();
				}
		}

		void OnTriggerEnter (Collider other)
		{			
				if (other.gameObject == player) {
						if (needKey) {
								if (!playerInventory.hasKey) {
										audio.clip = accessDeniedClip;
										audio.Play ();
								} else {
										enterCount++;
								}
						} else {
								enterCount++;
						}
				} else if (other.gameObject.tag == Tags.enemy) {
						if (other is CapsuleCollider) {
								enterCount++;
						}
				}
		}

		void OnTriggerExit (Collider other)
		{
				if (other.gameObject == player || (other.gameObject.tag == Tags.enemy && other is CapsuleCollider)) {
						enterCount = Mathf.Max (0, enterCount - 1);
				}
		}
}
