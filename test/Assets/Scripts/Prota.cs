using UnityEngine;
using System.Collections;

public class Prota : MonoBehaviour {
	public float maxVelocity = 30.0f;
	public float accel = 0.5f;
	private float velocity = 0.0f;

	private Logic _logic;

	void Start () {
		_logic = Logic.instance;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mov = InputController.GetMovement(_logic.GetCurrentPlayer());
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
