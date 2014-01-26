using UnityEngine;
using System.Collections;

public class MyGUI : MonoBehaviour {
	public TextMesh timeText;
	//public TextMesh middleText;
	//public TextMesh lifeText;
	//public TextMesh trapsText;
	public TextMesh menuText;
	private Logic _logic;
	public GameObject circleTime;
	private Logic.GameState _state;
	Vector3 original;
	public GameObject Menubackground;

	// Use this for initialization
	void Start () {
		_logic = Logic.instance;
		original =  circleTime.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (_logic.gameState == Logic.GameState.PLAYING) {
			if (_logic.time < 1.0f) {

				//iTween.ScaleFrom(timeText.gameObject, new Vector3(0.01416988f, 0.01416988f, 0.01416988f), 0.5f);
			} 
			timeText.text = (int)_logic.time + "";
		} else {
			timeText.text = "";
			//iTween.PunchScale(timeText.gameObject, new Vector3(0.05f, 0.0f, 0.05f), 0.2f);
		}

	}

	public void SetState (Logic.GameState state) {
		_state = state;
		if (state == Logic.GameState.PLAYING) {
			menuText.gameObject.SetActive (false);
			timeText.gameObject.SetActive (true);
			iTween.PunchScale(timeText.gameObject, new Vector3(0.05f, 0.0f, 0.05f), 0.2f);



			iTween.ScaleTo(circleTime, iTween.Hash(
				"scale", original, 
				"time", 0.5f,
				"easingType", iTween.EaseType.easeOutExpo));

			_.Wait(0.5f).Done (()=>{iTween.ScaleTo(circleTime, iTween.Hash(
				"scale", original*0.1f, 
				"time", 0.5f,
					"easingType", iTween.EaseType.easeOutExpo));});

			_.Wait(1.0f).Done(()=>{ iTween.ScaleTo(circleTime, iTween.Hash(
					"scale", original, 
				"time", 0.5f,
					"easingType", iTween.EaseType.easeOutExpo, 
				"onComplete", "OnCompleteScale",
					"onCompleteTarget", gameObject));});

		}
		else if (state == Logic.GameState.WAITING) {

			menuText.text = "Player "+_logic.GetCurrentPlayer()+" press A";
			menuText.gameObject.SetActive (true);
			timeText.gameObject.SetActive (false);


		}
	}

	public void OnCompleteScale() {
		if (_state == Logic.GameState.PLAYING) {
			Vector3 original =  circleTime.transform.localScale;
			iTween.ScaleTo(circleTime, iTween.Hash(
				"scale", original*0.1f, 
				"time", 0.5f,
				"easingType", iTween.EaseType.easeOutExpo));
			
			_.Wait(0.5f).Done(()=>{ 
				if (_state == Logic.GameState.PLAYING) {
					iTween.ScaleTo(circleTime, iTween.Hash(
					"scale", original, 
					"time", 0.5f,
					"easingType", iTween.EaseType.easeOutExpo, 
					"onComplete", "OnCompleteScale",
						"onCompleteTarget", gameObject));}
				});

		}
	}
}
