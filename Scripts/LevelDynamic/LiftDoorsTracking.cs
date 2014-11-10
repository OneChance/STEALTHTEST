using UnityEngine;
using System.Collections;

public class LiftDoorsTracking : MonoBehaviour
{
		public float doorSpeed;
		private Transform leftOuterDoor;
		private Transform rightOuterDoor;
		private Transform leftInnerDoor;
		private Transform rightInnerDoor;
		private float leftInnerColsedX;
		private float rightInnerClosedX;

		void Awake ()
		{
				leftOuterDoor = GameObject.Find ("door_exit_outer_left_001").transform;
				rightOuterDoor = GameObject.Find ("door_exit_outer_right_001").transform;
				leftInnerDoor = GameObject.Find ("door_exit_inner_left_001").transform;
				rightInnerDoor = GameObject.Find ("door_exit_inner_right_001").transform;

				leftInnerColsedX = leftInnerDoor.position.x;
				rightInnerClosedX = rightInnerDoor.position.x;
		}

		void moveDoors (float leftOutX, float rightOutX)
		{
				float newX = Mathf.Lerp (leftInnerDoor.position.x, leftOutX, doorSpeed * Time.deltaTime);
				leftInnerDoor.position = new Vector3 (newX, leftInnerDoor.position.y, leftInnerDoor.position.z);

				newX = Mathf.Lerp (rightInnerDoor.position.x, rightOutX, doorSpeed * Time.deltaTime);
				rightInnerDoor.position = new Vector3 (newX, rightInnerDoor.position.y, rightInnerDoor.position.z);
		}

		public void DoorFollow ()
		{	
				moveDoors (leftOuterDoor.position.x, rightOuterDoor.position.x);
		}

		public void CloseDoor ()
		{
				moveDoors (leftInnerColsedX, rightInnerClosedX);
		}
}
