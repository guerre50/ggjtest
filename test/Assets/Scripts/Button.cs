using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Logic.GameMode buttonVisibleIn;
	public GameObject[] doors;
    public GameObject keyBox;

	private bool buttonActive = false;
	private bool wasActive = false;
	public GameObject graphics;

	private Logic _logic;

	void Start() {
		_logic = Logic.instance;
        graphics.renderer.material.color = keyBox.GetComponentInChildren<Renderer>().material.color;
	}

	void FixedUpdate () {
		graphics.renderer.enabled = IsVisible();

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
        {
            door.SendMessage("SetState", open);
        }
	}

	void OnTriggerStay (Collider col) {
        // check if button is pressable in this mode
		//if(InOwnMode()) {
            if (col.gameObject.tag == "Rock") {
			    buttonActive = true;
		    }
		//}
        if (col.gameObject == keyBox.gameObject)
        {
            // draw key to center of button
            // turn color of
        }
	}

    public bool IsVisible()
    {
        return buttonVisibleIn == _logic.gameMode;
    }

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
        foreach (GameObject door in doors)
        {
            Gizmos.DrawLine(transform.position, door.transform.position);
        }
	}
}
