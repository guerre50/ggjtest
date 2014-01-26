using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public GameObject prota;
	private Vector3 targetPosition;
	public Bounds limits;
	private bool moveToTarget = false;
	private Vector3 initialCenter;
	public Vector2 limitX;
	public Vector2 limitZ;

	void Start () {
		initialCenter = limits.center;
		targetPosition = transform.position;
	}

	void Update () {
		limits.center = transform.position;
		if(!limits.Contains(prota.transform.position)) {
			moveToTarget = true;
		}

		float vertical = Camera.main.orthographicSize;
		float horizontal = vertical * Screen.width / Screen.height;
		
		if (moveToTarget) {
			Vector3 candidatePosition = prota.transform.position;
			// out of the limits
			if (candidatePosition.x - horizontal < limitX.x) { 
				targetPosition.x = limitX.x + horizontal;
			} else if (candidatePosition.x + horizontal > limitX.y) {
				targetPosition.x = limitX.y - horizontal;
			} else {
				targetPosition.x = prota.transform.position.x;
			}

			if (candidatePosition.z - vertical < limitZ.x) {
				targetPosition.z = limitZ.x + vertical;
			} else if ( candidatePosition.y + vertical> limitZ.y) { 
				targetPosition.z = limitZ.y - vertical;
			} else {
				targetPosition.z = prota.transform.position.z;
			}

			iTween.MoveUpdate(gameObject, targetPosition, 2f);
			if (Vector3.Distance(transform.position, targetPosition) < 0.5f) moveToTarget = false;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(limitX.x, 0, limitZ.y), new Vector3(limitX.x, 0, limitZ.x));
		Gizmos.DrawLine(new Vector3(limitX.y, 0, limitZ.y), new Vector3(limitX.y, 0, limitZ.x));
		Gizmos.DrawLine(new Vector3(limitX.x, 0, limitZ.x), new Vector3(limitX.y, 0, limitZ.x));
		Gizmos.DrawLine(new Vector3(limitX.y, 0, limitZ.x), new Vector3(limitX.x, 0, limitZ.x));

		Gizmos.color = Color.white;
	}
}
