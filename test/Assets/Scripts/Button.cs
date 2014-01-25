using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public bool floored = true;
	public GameObject[] doors;

	private bool buttonActive = false;
	private bool wasActive = false;

	private Logic _logic;

	void Start() {
		_logic = Logic.instance;
	}

	void FixedUpdate () {
		if (buttonActive) {
			if (!wasActive) NotifyDoors(true);
		}
		else if (wasActive) NotifyDoors(false);

		wasActive = buttonActive;
		buttonActive = false;
	}

	void NotifyDoors (bool open) {
		foreach (GameObject door in doors)
			door.SendMessage("SetState", open);
	}

	void OnTriggerStay (Collider col) {
		if (InOwnMode()) {
			buttonActive = true;
		}
	}

	public bool InOwnMode() {
		return (floored && (_logic.gameMode == Logic.GameMode.SIDESCROLL)) || (!floored && (_logic.gameMode == Logic.GameMode.TOPVIEW));
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		foreach (GameObject door in doors)
			Gizmos.DrawLine (transform.position, door.transform.position);
	}
}
