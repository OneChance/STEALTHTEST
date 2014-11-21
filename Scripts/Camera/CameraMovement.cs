using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
		public float smooth = 1.5f;
		private Transform player;
		private Vector3 relPos;
		private float relDistance;
		private Vector3 newPos;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player).transform;
				relPos = transform.position - player.position;
				relDistance = relPos.magnitude - 0.5f; 
		}

		void FixedUpdate ()
		{
				Vector3 standardPos = player.position + relPos;
				Vector3 abovePos = player.position + Vector3.up * relDistance;
				Vector3[] checkPoints = new Vector3[5];
				
				checkPoints [0] = standardPos;
				checkPoints [4] = abovePos;
				checkPoints [1] = Vector3.Lerp (standardPos, abovePos, 0.25f);
				checkPoints [2] = Vector3.Lerp (standardPos, abovePos, 0.5f);
				checkPoints [3] = Vector3.Lerp (standardPos, abovePos, 0.75f);
				
				foreach (Vector3 pos in checkPoints) {
						if (CheckPos (pos)) {	
								newPos = pos;
								break;
						}
				}

				transform.position = Vector3.Lerp (transform.position, newPos, smooth * Time.deltaTime);
				smoothLookAt ();
		}

		bool CheckPos (Vector3 checkPoint)
		{
				RaycastHit hit;
				
				if (Physics.Raycast (checkPoint, player.position - checkPoint, out hit, relDistance)) {
						if (hit.transform.gameObject.tag == Tags.sceneObject) {
								return false;
						} 						
				}
				return true;
		}

		void smoothLookAt ()
		{
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (player.position - newPos, Vector3.up), smooth * Time.deltaTime);
		}
}
