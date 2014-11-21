using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
		public float patrolSpeed = 2f;
		public float chaseSpeed = 5f;
		public float chaseWaitTime = 5f;
		public float patrolWaitTime = 1f;
		public Transform[] patrolWayPoints;
		private EnemySight enemySight;
		private NavMeshAgent nav;
		private Transform player;
		private PlayerHealth playerHealth;
		private LastPlayerSighting lastPlayerSighting;
		private float chaseTimer;
		private float patrolTimer;
		private int wayPointIndex;

		void Awake ()
		{
				enemySight = GetComponent<EnemySight> ();
				nav = GetComponent<NavMeshAgent> ();
				player = GameObject.FindGameObjectWithTag (Tags.player).transform;
				playerHealth = player.GetComponent<PlayerHealth> ();
				lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
				chaseTimer = 0f;
				patrolTimer = 0f;
		}

		void Update ()
		{
				if (enemySight.playerInSight && playerHealth.health > 0f) {
						Shooting ();
				} else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f) {
						Chasing ();
				} else {
						Patrolling ();
				}
		}

		void Shooting ()
		{
				nav.Stop ();
		}

		void Chasing ()
		{
				Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
				if (sightingDeltaPos.magnitude > 2) {
						nav.destination = enemySight.personalLastSighting;
				}

				nav.speed = chaseSpeed;

				if (nav.remainingDistance < nav.stoppingDistance) {
						chaseTimer += Time.deltaTime;
						if (chaseTimer >= chaseWaitTime) {
								lastPlayerSighting.position = lastPlayerSighting.resetPosition;
								enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
								chaseTimer = 0f;
						}
				} else {
						//玩家再次触发别处的警报，此时追踪等待为到达最大等待时间，并未清0，所以在往新地点移动的过程中，清0计时器
						chaseTimer = 0f;
				}
		}

		void Patrolling ()
		{
				nav.speed = patrolSpeed;
				if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance) {
						patrolTimer += Time.deltaTime;

						if (patrolTimer >= patrolWaitTime) {
								if (wayPointIndex == patrolWayPoints.Length - 1) {
										wayPointIndex = 0;
								} else {
										wayPointIndex++;
								}
								patrolTimer = 0f;
						}
				} else {
						patrolTimer = 0f;
				}

				nav.destination = patrolWayPoints [wayPointIndex].position;
		}
}
