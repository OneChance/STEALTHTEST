using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
		public float maxDamage = 120f;
		public float minDamage = 45f;
		public AudioClip shotClip;
		public float flashIntensity = 3f;
		public float fadeSpeed = 10f;
		private Animator anim;
		private HashIDs hash;
		private LineRenderer laserShotLine;
		private Light laserShotlight;
		private SphereCollider col;
		private Transform player;
		private PlayerHealth playerHealth;
		private bool shooting;
		private float scaledDamage;

		void Awake ()
		{
				anim = GetComponent<Animator> ();
				hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
				laserShotLine = GetComponentInChildren<LineRenderer> ();
				laserShotlight = laserShotLine.gameObject.light;
				col = GetComponent<SphereCollider> ();
				player = GameObject.FindGameObjectWithTag (Tags.player).transform;
				playerHealth = player.gameObject.GetComponent<PlayerHealth> ();

				laserShotLine.enabled = false;
				laserShotlight.intensity = 0f;

				scaledDamage = maxDamage - minDamage;
		}

		void Update ()
		{
				float shot = anim.GetFloat (hash.shotFloat);
				if (shot > 0.5f && !shooting) {
						Shoot ();
				}
				if (shot < 0.5f) {
						shooting = false;
						laserShotLine.enabled = false;
				}

				laserShotlight.intensity = Mathf.Lerp (laserShotlight.intensity, 0f, fadeSpeed * Time.deltaTime);
		}
		
		void OnAnimatorIK (int layerIndex)
		{
				float aimWeight = anim.GetFloat (hash.aimWeightFloat);
				anim.SetIKPosition (AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
				anim.SetIKPositionWeight (AvatarIKGoal.RightHand, aimWeight);
		}

		void Shoot ()
		{
				shooting = true;

				float damageRatio = (col.radius - Vector3.Distance (player.position, transform.position)) / col.radius;
				float damage = minDamage + scaledDamage * damageRatio;
				playerHealth.TakeDamage (damage);
				ShotEffects ();
				
		}

		void ShotEffects ()
		{
				laserShotLine.SetPosition (0, laserShotlight.transform.position);
				laserShotLine.SetPosition (1, player.position + Vector3.up * 1.5f);
				laserShotLine.enabled = true;
				laserShotlight.intensity = flashIntensity;
				AudioSource.PlayClipAtPoint (shotClip, laserShotlight.transform.position);
		}
}
