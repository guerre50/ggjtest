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

    private Vector3 sidescrollGravity = new Vector3(0, 0, -1.0f);
    private Vector3 topviewGravity = new Vector3(0, -1.0f, 0);

	public float startingMoney = 5.0f;
	public int startingTraps = 5;
	public float moneyPerSecond = 0.7f;
	public float timeStealPerPress = 0.1f;
	public float moneyPerSteal = 0.5f;

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
            Debug.Log("Gravity changed to sidescroll");
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

    private void SidescrollMode()
    {
        Debug.Log("entered sidescrollmode");
        if (InputController.GetKeyDown(InputController.Key.A,0))
        {
            Debug.Log("G was pressed");
            gameMode = GameMode.TOPVIEW;
        }
    }

    private void TopviewMode()
    {
        if (InputController.GetKeyDown(InputController.Key.A,0))
        {
            gameMode = GameMode.SIDESCROLL;
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
		if (traps[currentPlayer] > 0 || (CheckAndBuyTrap())) {
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
			traps[currentPlayer] += 1;
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
