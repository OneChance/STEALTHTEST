using UnityEngine;
using System.Collections;

public class CCTVPlayerDectection : MonoBehaviour
{
		private GameObject player;
		private LastPlayerSighting lastPlayerSighting;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		}

		void OnTriggerStay (Collider other)
		{
				if (other == player) {
						Vector3 relPos = player.transform.position - transform.position;
						
						RaycastHit hit;
					
						if (Physics.Raycast (transform.position, relPos, out hit)) {
								if (hit.collider.gameObject == player) {
										lastPlayerSighting.position = player.transform.position;
								}
						}
				}
		}
}
