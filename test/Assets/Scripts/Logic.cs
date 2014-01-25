using UnityEngine;
using System.Collections;

public  class Logic : GameObjectSingleton<Logic> {
	public GameObject start;
	public float roundTime = 10.0f;
	public float time;
	private Prota prota;

	private Vector3 initialPosition;

	public enum GameState {PLAYING, WAITING};
	public GameState gameState = GameState.WAITING;
    public enum GameMode {SIDESCROLL, TOPVIEW};
	public GameMode gameMode = GameMode.TOPVIEW;
	
    private Vector3 sidescrollGravity = new Vector3(0, 0, -20.0f);
    private Vector3 topviewGravity = new Vector3(0, -20.0f, 0);

    public Color KEYBOXBOTH = new Color(255,0,255);
    public Color KEYBOXTOP = new Color(255,255,0);
    public Color KEYBOXSIDE = new Color(0, 255, 255);
    public Color KEYBOXNEUTRAL = new Color(255,255,255);

	// Privates
	private int currentPlayer = 0;
	private MyGUI camGUI;
	public GameObject menuBackground;

	private float menuAnimationTime = 1.0f;
	private float animatedTime = 0.0f;

	// Use this for initialization
	void Start () {
		prota = GameObject.FindGameObjectWithTag ("Player").GetComponent<Prota> ();
		initialPosition = prota.gameObject.transform.position;
		camGUI = GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<MyGUI> ();
		menuBackground = GameObject.FindGameObjectWithTag ("MenuBackground");

		//Stop ();
		time = 0.0f;
		
		prota.enabled = false;
		menuBackground.renderer.material.color = prota.gameObject.renderer.material.color;
		menuBackground.transform.localScale = new Vector3 (100.0f, 2.0f, 100.0f);
		menuBackground.transform.position = prota.transform.position;
		menuBackground.renderer.enabled = true;

		gameState = GameState.WAITING;
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
            Physics.gravity = topviewGravity;
            TopviewMode();
        }
        else if (gameMode == Logic.GameMode.SIDESCROLL && Physics.gravity != sidescrollGravity)
        {
            // when side scrolling
            Physics.gravity = sidescrollGravity;
            SidescrollMode();
        }

		if (InputController.GetKeyDown (InputController.Key.Back))
			Application.LoadLevel (Application.loadedLevel);
	}

	private void WaitingState() {
		animatedTime -= Time.deltaTime;

		if (animatedTime > 0.0f) {
			if (menuBackground.transform.localScale.magnitude < 150.0f)
				menuBackground.transform.localScale = new Vector3(menuBackground.transform.localScale.x*(2.0f - menuAnimationTime/10.0f), 2.0f, menuBackground.transform.localScale.z*(2.0f - menuAnimationTime/10.0f));
			menuBackground.transform.position = prota.transform.position;
		}
		else {
			camGUI.SetState(gameState);
			if (InputController.GetKeyDown (InputController.Key.A, GetCurrentPlayer())) {
				Restart ();
			}
		}
	}

	private void PlayingState() {
		time -= Time.deltaTime;

		if (time > 0.0f) {
			menuBackground.transform.localScale = new Vector3 (menuBackground.transform.localScale.x * ( menuAnimationTime / 10.0f + 0.4f), 2.0f, menuBackground.transform.localScale.z * ( menuAnimationTime / 10.0f + 0.4f));
			menuBackground.transform.position = prota.transform.position;
		}

		if (InputController.GetKeyDown(InputController.Key.Start, GetCurrentPlayer())) //Change players
		{
			Stop();
		}

		// World
		if (time <= 0) {
			Stop();
		}
	}

    private void SidescrollMode()
    {

    }

    private void TopviewMode()
    {
        
    }

	public void Restart() {
		time += roundTime;
		//prota.transform.position = start.transform.position;
		if (gameMode == GameMode.SIDESCROLL) {
			gameMode = GameMode.TOPVIEW;
		} else {
			gameMode = GameMode.SIDESCROLL;
		}

		gameState = GameState.PLAYING;
		//prota.transform.position = initialPosition;
		prota.enabled = true;
		//menuBackground.renderer.enabled = false;
		camGUI.SetState(gameState);
	}

	public void Stop() {
		currentPlayer = (currentPlayer + 1)%2;
		animatedTime = menuAnimationTime;
		
		prota.enabled = false;
		menuBackground.renderer.material.color = prota.gameObject.renderer.material.color;
		menuBackground.transform.localScale = new Vector3 (0.01f, 1.0f, 0.01f);
		menuBackground.transform.position = prota.transform.position;
		menuBackground.renderer.enabled = true;

		gameState = GameState.WAITING;
	}
	
	public int GetCurrentPlayer() {  // Between 1 and 2 (internally working between 0 and 1)
		return currentPlayer + 1;
	}
}
