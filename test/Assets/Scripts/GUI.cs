using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {
	public TextMesh timeText;
	public TextMesh middleText;
	public TextMesh lifeText;
	public TextMesh trapsText;
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
	}
	
	private void PlayingState() {
		middleText.gameObject.SetActive(false);
		timeText.text = ((int)_logic.time)+"";
		lifeText.text = _logic.prota.life +"";
		trapsText.text = _logic.prota.traps +"";
	}
}
