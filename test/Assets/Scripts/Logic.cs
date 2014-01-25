using UnityEngine;
using System.Collections;

public  class Logic : GameObjectSingleton<Logic> {
	public GameObject start;
	public float roundTime = 30.0f;
	public float time;
	public Prota prota;
	public enum GameState {PLAYING, WAITING};
	public GameState gameState = GameState.WAITING;
	public float startingMoney = 5.0f;
	public float moneyPerSecond = 0.7f;
	public float timeStealPerPress = 0.1f;
	public float moneyPerSteal = 0.5f;

	// Prices
	public float trapPrice = 5.0f;
	public float timePrice = 5.0f;
	public float timeBuyableAmount = 5.0f;

	// Privates
	private int currentPlayer = 0;
	private Vector2 money;

	// Use this for initialization
	void Start () {
		money = new Vector2 (startingMoney, startingMoney);

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

		// Playing player
		money [currentPlayer] += Time.deltaTime * moneyPerSecond;

		// Waiting player
		if (InputController.GetKeyDown (InputController.Key.A)) {
			time -= timeStealPerPress;
			money[currentPlayer] += moneyPerSteal;
		}

		// World
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
