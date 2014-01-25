﻿using UnityEngine;
using System.Collections;

public  class Logic : GameObjectSingleton<Logic> {
	public GameObject start;
	public float roundTime = 30.0f;
	public float time;
	public Prota prota;
	public enum GameState {PLAYING, WAITING};
	public GameState gameState = GameState.WAITING;
	public float startingMoney = 5.0f;
	public int startingTraps = 5;
	public float moneyPerSecond = 0.7f;

	// Prices
	public float trapPrice = 1.0f;
	public float timePrice = 5.0f;
	public float timeBuyableAmount = 5.0f;

	// Privates
	private int currentPlayer = 0;
	private Vector2 money;
	private Vector2 traps;

	// Use this for initialization
	void Start () {
		money = new Vector2 (startingMoney, startingMoney);
		traps = new Vector2 (startingTraps, startingTraps);

		currentPlayer = 1;
		Restart();
	}
	
	// Update is called once per frame
	void Update () {
		switch(gameState) {
		case GameState.PLAYING:
			PlayingState();
			break;
		case GameState.WAITING:
			WaitingState();
			break;
		}
	}

	private void WaitingState() {
		if (InputController.GetKeyDown(InputController.Key.A)) {
			gameState = GameState.PLAYING;
		}
	}

	private void PlayingState() {
		time -= Time.deltaTime;
		money [currentPlayer] += Time.deltaTime * moneyPerSecond;

		if (time <= 0) {
			Restart();
		}
	}

	public void Restart() {
		time = roundTime;
		//prota.transform.position = start.transform.position;
		gameState = GameState.WAITING;

		currentPlayer = (currentPlayer + 1) % 2;
	}

	public int GetCurrentPlayerMoney() {
		return (int)money[currentPlayer];
	}

	public int GetCurrentPlayerTraps() {
		return (int)traps[currentPlayer];
	}

	public bool CheckAndPlaceTrap() {
		if (traps[currentPlayer] > 0) {
			--traps[currentPlayer];

			return true;
		}

		return false;
	}

	public int GetCurrentPlayer() {  // Between 1 and 2 (internally working between 0 and 1)
		return currentPlayer + 1;
	}

	public bool CheckAndBuyTrap() {
		if (money[currentPlayer] >= trapPrice) {
			money[currentPlayer] -= trapPrice;
			return true;
		}
		return false;
	}

	public bool CheckAndBuyTime() {
		if (money[currentPlayer] >= timePrice) {
			money[currentPlayer] -= timePrice;
			time += timeBuyableAmount;
			return true;
		}
		return false;
	}
}
