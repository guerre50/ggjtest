using UnityEngine;
using System.Collections;

public class MyGUI : MonoBehaviour {
	public TextMesh timeText;
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
		if (_logic.gameState == Logic.GameState.PLAYING) {
			if (_logic.time < 3.0f) {
				iTween.PunchScale(timeText.gameObject, new Vector3(0.03f, 0.0f, 0.03f), 0.5f);
			}
			timeText.text = _logic.time.ToString ("F2");
		} else {
			iTween.PunchScale(timeText.gameObject, new Vector3(0.05f, 0.0f, 0.05f), 0.2f);
		}

	}

	public void SetState (Logic.GameState state) {
		if (state == Logic.GameState.PLAYING) {
			menuText.gameObject.SetActive (false);
			timeText.gameObject.SetActive (true);
		}
		else if (state == Logic.GameState.WAITING) {

			menuText.text = "Player "+_logic.GetCurrentPlayer()+" press A";
			menuText.gameObject.SetActive (true);
			timeText.gameObject.SetActive (false);


		}
	}
}
