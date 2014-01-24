using UnityEngine;
using System.Collections;

public class Prota : MonoBehaviour {
	public float maxVelocity = 30.0f;
	public float accel = 0.5f;
	private float velocity = 0.0f;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mov = InputController.GetMovement(1);
		if (mov.magnitude != 0) {
			velocity += accel;
		} else {
			velocity -= accel;
		}
		velocity = Mathf.Clamp(velocity, 0, maxVelocity);

		mov *= (velocity*Time.deltaTime);
		transform.Translate(mov.x, 0, mov.y);

	}
}
