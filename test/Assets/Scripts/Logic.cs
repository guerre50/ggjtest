using UnityEngine;
using System.Collections;

public  class Logic : GameObjectSingleton<Logic> {
	public GameObject start;
	public float roundTime = 30.0f;
	public float time;
	public Prota prota;
	public enum GameState {PLAYING, WAITING};
	public GameState gameState = GameState.WAITING;

	// Use this for initialization
	void Start () {
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
		if (Input.GetKeyDown(KeyCode.Space)) {
			gameState = GameState.PLAYING;
		}
	}

	private void PlayingState() {
		time -= Time.deltaTime;
		if (time <= 0) {
			Restart();
		}
	}

	public void Restart() {
		time = roundTime;
		//prota.transform.position = start.transform.position;
		gameState = GameState.WAITING;

	}
}
