using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Logic.GameMode buttonPressableIn;
	public GameObject[] doors;

	private bool buttonActive = false;
	private bool wasActive = false;

	private Logic _logic;

	void Start() {
		_logic = Logic.instance;
	}

	void FixedUpdate () {
        if (buttonActive)
        {
            if (!wasActive) NotifyDoors(true);
        }
        else
        {
            if (wasActive) NotifyDoors(false);
        }

		wasActive = buttonActive;
		buttonActive = false;
	}

	void NotifyDoors (bool open) {
		foreach (GameObject door in doors)
			door.SendMessage("SetState", open);
	}

	void OnTriggerStay (Collider col) {
        // check if button is pressable in this mode
		//if(InOwnMode()) {
            if (col.gameObject.tag == "Rock") {
			    buttonActive = true;
		    }
		//}
	}

    public bool InOwnMode()
    {
        return buttonPressableIn == _logic.gameMode;
    }

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		foreach (GameObject door in doors)
			Gizmos.DrawLine (transform.position, door.transform.position);
	}
}
