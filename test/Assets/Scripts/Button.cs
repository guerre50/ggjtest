﻿using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Logic.GameMode buttonVisibleIn;
	public GameObject[] doors;
    public GameObject keyBox;
	public GameObject coloredRenderer;

	private bool buttonActive = false;
	private bool wasActive = false;

	private Logic _logic;
	private SoundManager _sound;

	void Start() {
		_logic = Logic.instance;
		_sound = SoundManager.instance;
        //graphics.renderer.material.color = keyBox.GetComponentInChildren<Renderer>().material.color;
	}

	void FixedUpdate () {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
			rend.enabled = IsVisible();// || buttonActive;
        }

		Renderer border = coloredRenderer.GetComponent<Renderer>();
        border.material.color = keyBox.GetComponent<Pushable>().currentColor;

        if (buttonActive)
        {
            if (!wasActive) {
				NotifyDoors(true);
				_sound.Play ("high_bleep", transform.position);
			}
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
        //Debug.Log("Collided");
        //Debug.Log(col);
        //Debug.Log(keyBox);
        if (col.gameObject == keyBox.gameObject)
        {
            //Debug.Log("Entered");
            buttonActive = true;
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
        Gizmos.DrawLine(transform.position, keyBox.transform.position);

	}
}
