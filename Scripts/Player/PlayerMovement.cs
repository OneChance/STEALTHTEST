using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
		public AudioClip shoutingClip;
		public float turnSmoothing = 15f;
		public float speedDampTime = 0.1f;
		private Animator anim;
		private HashIDs hash;
		private float shoutInterval = 1.9f;
		private float shoutTimer;

		void Awake ()
		{
				anim = GetComponent<Animator> ();
				hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
				anim.SetLayerWeight (1, 1f);
		}

		void FixedUpdate ()
		{
				float h = Input.GetAxis ("Horizontal");
				float v = Input.GetAxis ("Vertical");
				bool sneak = Input.GetButton ("Sneak");
				MovementManagement (h, v, sneak);			
		}

		void Update ()
		{
				bool shout = Input.GetButtonDown ("Attract");
				anim.SetBool (hash.shoutingBool, shout);
				AudioManagement (shout);
		}

		void MovementManagement (float h, float v, bool sneak)
		{
				anim.SetBool (hash.sneakingBool, sneak);
				if (h != 0 || v != 0) {
						RotateManagement (h, v);
						anim.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
				} else {
						anim.SetFloat (hash.speedFloat, 0f);
				}
		}

		void RotateManagement (float h, float v)
		{
				Vector3 direction = new Vector3 (h, 0, v);
				rigidbody.MoveRotation (Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (direction, Vector3.up), turnSmoothing * Time.deltaTime));
				//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (direction, Vector3.up), turnSmoothing * Time.deltaTime);
		}

		void AudioManagement (bool shout)
		{
				if (anim.GetCurrentAnimatorStateInfo (0).nameHash == hash.locomotionState) {
						if (!audio.isPlaying) {
								audio.Play ();
						}			
				} else {
						audio.Stop ();
				}
				
				if (shout && shoutTimer == 0) {
						shoutTimer = shoutInterval;
						AudioSource.PlayClipAtPoint (shoutingClip, transform.position, 0.3f);
				}
				
				shoutTimer -= Time.deltaTime;
				if (shoutTimer < 0) {
						shoutTimer = 0;
				}
		}
}
