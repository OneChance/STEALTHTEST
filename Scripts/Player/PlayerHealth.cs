using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
		public float health = 100f;
		public float resetAfterDeathTime = 5f;
		public AudioClip deathClip;
		private Animator anim;
		private PlayerMovement playerMovement;
		private HashIDs hash;
		private SceneFadeInOut sceneFader;
		private LastPlayerSighting lastPlayerSighting;
		private float resetTimer = 0f;
		private bool playerDead = false;

		void Awake ()
		{
				anim = GetComponent<Animator> ();
				playerMovement = GetComponent<PlayerMovement> ();
				hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
				sceneFader = GameObject.FindGameObjectWithTag (Tags.fade).GetComponent<SceneFadeInOut> ();
				lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		}

		void Update ()
		{
				if (health <= 0) {
						if (!playerDead) {
								PlayerDying ();
								PlayerDead ();
						} else {
								LevelReset ();
						}	
				}
		}

		void PlayerDying ()
		{
				playerDead = true;
				anim.SetBool (hash.deadBool, true);
				AudioSource.PlayClipAtPoint (deathClip, transform.position);
		}

		void PlayerDead ()
		{
				if (anim.GetCurrentAnimatorStateInfo (0).nameHash == hash.dyingState) {
						anim.SetBool (hash.deadBool, false);
				}
			
				anim.SetFloat (hash.speedFloat, 0);
				playerMovement.enabled = false;
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;		
				audio.Stop ();
		}

		void LevelReset ()
		{
				resetTimer += Time.deltaTime;
				if (resetTimer >= resetAfterDeathTime) {
						sceneFader.EndScene ();
				}
		}

		public void TakeDamage (float damage)
		{
				health -= damage;
		}
}
