﻿using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {
	public TextMesh timeText;
	public TextMesh middleText;
	public TextMesh moneyText;
	private Logic _logic;

	// Use this for initialization
	void Start () {
		_logic = Logic.instance;
	}
	
	// Update is called once per frame
	void Update () {
		switch(_logic.gameState) {
		case Logic.GameState.PLAYING:
			PlayingState();
			break;
		case Logic.GameState.WAITING:
			WaitingState();
			break;
		}


	}

	private void WaitingState() {
		middleText.gameObject.SetActive(true);
		moneyText.gameObject.SetActive (false);
	}
	
	private void PlayingState() {
		middleText.gameObject.SetActive(false);
		moneyText.gameObject.SetActive (true);
		timeText.text = ((int)_logic.time) + "";
		moneyText.text = _logic.GetCurrentPlayerMoney() + " $";
	}
}
