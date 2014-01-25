﻿using UnityEngine;
using System.Collections;

public class MyGUI : MonoBehaviour {
	//public TextMesh timeText;
	//public TextMesh middleText;
	//public TextMesh lifeText;
	//public TextMesh trapsText;
	public TextMesh menuText;
	private Logic _logic;

	// Use this for initialization
	void Start () {
		_logic = Logic.instance;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetState (Logic.GameState state) {
		if (state == Logic.GameState.PLAYING)
			menuText.gameObject.SetActive (false);
		else if (state == Logic.GameState.WAITING) {
			menuText.text = "Player "+_logic.GetCurrentPlayer()+" press A";
			menuText.gameObject.SetActive (true);
		}
	}
}
