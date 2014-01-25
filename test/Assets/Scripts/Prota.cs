using UnityEngine;
using System.Collections;

public class Prota : MonoBehaviour {
	public float maxVelocity = 30.0f;
	public float accel = 0.5f;
	private float velocity = 0.0f;
	public int life = 4;
	public int traps = 5;
	public GameObject trap;

	private Logic _logic;

	void Start () {
		_logic = Logic.instance;
	}

	void Update () {
		if (_logic.gameState == Logic.GameState.PLAYING) {
			UpdateMovement();
			Actions();
		}
	}

	public void UpdateMovement() {
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

	public void Actions() {
		if (InputController.GetKeyDown(InputController.Key.A, _logic.GetCurrentPlayer())) {
			if (_logic.CheckAndPlaceTrap()) {
				GameObject newTrap = Instantiate(trap, transform.position, transform.rotation) as GameObject;
				newTrap.GetComponent<Trap>().Player = _logic.GetCurrentPlayer();
			}
		}

		if (InputController.GetKeyDown(InputController.Key.Y, _logic.GetCurrentPlayer())) {
			_logic.CheckAndBuyTime();
		}
	}

	public void DealDamage() {
		life -= 1;
	}

}
