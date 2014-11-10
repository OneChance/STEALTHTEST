using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour
{
		public float liftCloseTime = 2f;
		public float liftMoveTime = 2.2f;
		public float sceneEndTime = 6f;
		public float liftMoveSpeed = 3f;
		private GameObject player;
		private Animator anim;
		private HashIDs hash;
		private CameraMovement cameraMovement;
		private SceneFadeInOut sceneFadeInOut;
		private LiftDoorsTracking liftDoorsTracking;
		private bool playerInLift;
		private float timer;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				anim = player.GetComponent<Animator> ();
				hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
				cameraMovement = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraMovement> ();
				sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fade).GetComponent<SceneFadeInOut> ();
				liftDoorsTracking = GetComponent<LiftDoorsTracking> ();
				timer = 0f;
		}

		void Update ()
		{
				if (playerInLift) {
						LiftActivation ();
				}

				if (timer < liftCloseTime) {
						liftDoorsTracking.DoorFollow ();
				} else {
						liftDoorsTracking.CloseDoor ();
				}
		}

		void LiftActivation ()
		{
				timer += Time.deltaTime;
				if (timer >= liftMoveTime) {
						anim.SetFloat (hash.speedFloat, 0f);
						cameraMovement.enabled = false;
						player.transform.parent = transform;
						
						transform.Translate (Vector3.up * liftMoveSpeed * Time.deltaTime);

						if (!audio.isPlaying) {
								audio.Play ();
						}
					
						if (timer >= sceneEndTime) {
								sceneFadeInOut.EndScene ();
						}
				}
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject == player) {
						playerInLift = true;
				}
		}

		void OnTriggerExit (Collider other)
		{
				if (other.gameObject == player) {
						playerInLift = false;
						timer = 0f;
				}
		}

}
