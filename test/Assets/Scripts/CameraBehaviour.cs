using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public GameObject prota;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		targetPosition.x = prota.transform.position.x;
		targetPosition.z = prota.transform.position.z;

		iTween.MoveUpdate(gameObject, targetPosition, 3f);
	}
}
