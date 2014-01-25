using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public float rotationVelocity = 10.0f;
	private bool carrying = false;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, Time.deltaTime*rotationVelocity, 0, Space.World);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.transform != transform.parent) {
			transform.parent = col.gameObject.transform;
		}
	}

}
