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

	// Privates
	private int currentPlayer = 0;
	private MyGUI camGUI;
	public GameObject menuBackground;
	public GameObject menuBackgroundHole;

	private float menuAnimationTime = 1.0f;
	private float animatedTime = 0.0f;

	private bool itween;

	// Use this for initialization
	void Start () {
		prota = GameObject.FindGameObjectWithTag ("Player").GetComponent<Prota> ();
		initialPosition = prota.gameObject.transform.position;
		camGUI = GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<MyGUI> ();
		menuBackground = GameObject.FindGameObjectWithTag ("MenuBackground");
		menuBackgroundHole = GameObject.FindGameObjectWithTag ("MenuBackgroundHole");

		//Stop ();
		time = 0.0f;
		
		prota.enabled = false;
		menuBackground.renderer.material.color = prota.body.renderer.material.color;
		menuBackground.transform.position = prota.transform.position + Camera.main.transform.forward*-5;
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
		if (animatedTime > 0.0f && !itween) {
			itween = true;
			iTween.ScaleTo (menuBackground, iTween.Hash(
				"scale", new Vector3(-0.15370647f, -0.15370647f, -0.15370647f),
				"onComplete", "OnScaleComplete",
				"onCompleteTarget", gameObject,
				"time", menuAnimationTime/3));
			menuBackground.transform.position = prota.transform.position + Camera.main.transform.forward*-5;
		}
		if (animatedTime <= 0.0f) {
			camGUI.SetState(gameState);
			if (InputController.GetKeyDown (InputController.Key.A, GetCurrentPlayer())) {
				Restart ();
			}
		}
	} 

	private void PlayingState() {
		time -= Time.deltaTime;
		if (time > 0.0f && !itween) {
			itween = true;
			iTween.ScaleTo (menuBackground, iTween.Hash(
				"scale", new Vector3(-36, -36, -36),
				"onComplete", "OnScaleComplete",
				"easetype", iTween.EaseType.easeOutExpo,
				"onCompleteTarget", gameObject,
				"time", menuAnimationTime));
			iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f, 0.0f, 0.1f), 1.0f);
			if (gameMode == GameMode.TOPVIEW) {
				iTween.ScaleTo (prota.body, new Vector3(4f, 4f, 4f), 0.3f);
				_.Wait(0.3f).Done (()=> { iTween.ScaleTo (prota.body, new Vector3(1f, 1f, 1f), 0.3f); prota.particles.Emit();});

			}
		}
		if (time > 0.0f) {
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

	private void OnScaleComplete() {
	}

    private void SidescrollMode()
    {

    }

    private void TopviewMode()
    {
        
    }

	public void Restart() {
		time += roundTime;
		itween = false;

		if (gameMode == GameMode.SIDESCROLL) {
			gameMode = GameMode.TOPVIEW;
		} else {
			gameMode = GameMode.SIDESCROLL;
		}

		gameState = GameState.PLAYING;
		prota.enabled = true;
		prota.rigidbody.isKinematic = false;
		camGUI.SetState(gameState);
	}

	public void Stop() {
		currentPlayer = (currentPlayer + 1)%2;
		animatedTime = menuAnimationTime;
		
		prota.enabled = false;
		prota.rigidbody.isKinematic = true;
		menuBackground.renderer.material.color = prota.gameObject.renderer.material.color;
		menuBackground.transform.position = prota.transform.position;
		menuBackground.renderer.enabled = true;
		itween = false;
		gameState = GameState.WAITING;
	}
	
	public int GetCurrentPlayer() {  // Between 1 and 2 (internally working between 0 and 1)
		return currentPlayer + 1;
	}
}
