using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	pubilc float patrolSpeed = 2f;
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
	private wayPointIndex;

	void Awake(){
		enemySight = GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
		chaseTimer = 0f;
		patrolTimer = 0f;
	}

	void Update(){
		if(enemySight.playerInSight && playerHealth.health>0f){
			Shooting();
		}else if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health>0f){
			Chasing();
		}else{
			Patrolling();
		}
	}

	void Shooting(){
		nav.Stop();
	}

	void Chasing(){
		Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
		if(sightingDeltaPos.magnitude>2){
			nav.destination = enemySight.personalLastSighting;
		}

		nav.speed = chaseSpeed;

		if(nav.remainingDistance < nav.stoppingDistance){

		}
	}

	void Patrolling(){

	}
}
