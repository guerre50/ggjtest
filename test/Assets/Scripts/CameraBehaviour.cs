using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public GameObject prota;
	private Vector3 targetPosition;
	public Bounds limits;
	private bool moveToTarget = false;
	private Vector3 initialCenter;

	void Start () {
		initialCenter = limits.center;
		targetPosition = transform.position;
	}

	void Update () {
		limits.center = transform.position;
		if(!limits.Contains(prota.transform.position)) {
			moveToTarget = true;
		}

		if (moveToTarget) {
			targetPosition.x = prota.transform.position.x;
			targetPosition.z = prota.transform.position.z;
			
			iTween.MoveUpdate(gameObject, targetPosition, 2f);
			if (Vector3.Distance(transform.position, targetPosition) < 0.5f) moveToTarget = false;
		}
	}

	void OnDrawGizmos() {
		//Gizmos.DrawCube (limits.center, limits.extents);
	}
}
