using UnityEngine;
using System.Collections;

public  class Logic : GameObjectSingleton<Logic> {
	public GameObject start;
	public float roundTime = 30.0f;
	public float time;
	public Prota prota;

	public enum GameState {PLAYING, WAITING};
	public GameState gameState = GameState.WAITING;
    public enum GameMode {SIDESCROLL, TOPVIEW};
    public GameMode gameMode = GameMode.SIDESCROLL;

    private Vector3 sidescrollGravity = new Vector3(0, 0, -20.0f);
    private Vector3 topviewGravity = new Vector3(0, -20.0f, 0);

	// Privates
	private int currentPlayer = 0;

	// Use this for initialization
	void Start () {
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


		if (InputController.GetKeyDown(InputController.Key.X, 0))
		{
			if (gameMode == GameMode.SIDESCROLL) {
				gameMode = GameMode.TOPVIEW;
			} else {
				gameMode = GameMode.SIDESCROLL;
			}
		}

        // check in which mode
        if (gameMode == Logic.GameMode.TOPVIEW && Physics.gravity != topviewGravity)
        {
            // when top down
            Debug.Log("Gravity changed to topview");
            Physics.gravity = topviewGravity;
            TopviewMode();
        }
        else if (gameMode == Logic.GameMode.SIDESCROLL && Physics.gravity != sidescrollGravity)
        {
            // when side scrolling
            Physics.gravity = sidescrollGravity;
            SidescrollMode();
        }
            
	}

	private void WaitingState() {
		if (InputController.GetKeyDown(InputController.Key.A)) {
			gameState = GameState.PLAYING;
		}
	}

	private void PlayingState() {
		time -= Time.deltaTime;

		// World
		if (time <= 0) {
			Restart();
		}
	}

    private void SidescrollMode()
    {

    }

    private void TopviewMode()
    {
        
    }

	public void Restart() {
		time = roundTime;
		//prota.transform.position = start.transform.position;
		gameState = GameState.WAITING;

		currentPlayer = (currentPlayer + 1) % 2;
	}

	public int GetCurrentPlayer() {  // Between 1 and 2 (internally working between 0 and 1)
		return currentPlayer + 1;
	}
}
