#pragma strict

import XInputDotNetPure;
private var inverse : boolean;
private var time1 : float;
private var time2 : float; 

function Start () {
	if (PlayerPrefs.HasKey("inverse")) {
		inverse = (PlayerPrefs.GetInt("inverse") == -1 ? true : false);
	} else {
		PlayerPrefs.SetInt("inverse", 1);
		inverse = false;
	}
	
}

function Update () {
	if (Input.GetKeyDown(KeyCode.F12)) {
		inverse = !inverse;
		PlayerPrefs.SetInt("inverse", (inverse ? -1 : 1));
	} 
	if (time1 != Mathf.Infinity) {
		time1 -= Time.deltaTime;
		if (time1 <= 0.0f) {
			time1 = Mathf.Infinity;
			GamePad.SetVibration ( PlayerIndex.One,0.0f,0.0f);
			}
	}
	if (time2 != Mathf.Infinity) {
		time2 -= Time.deltaTime;
		if (time2 <= 0.0f) {
			time2 = Mathf.Infinity;
			GamePad.SetVibration ( PlayerIndex.Two,0.0f,0.0f);
		}
	}
}

function StopVibration(playerIndex : int, time : float) {
	yield WaitForSeconds(time);
 	Vibrate(playerIndex, 0.0f, 0.0f);
}


function Vibrate( playerIndex : int, val : float, time : float) {
	var player : PlayerIndex;
	var res : String = "";
	if (playerIndex == 1) {player = PlayerIndex.One;res += "one";}
	else if ( playerIndex == 2) {player = PlayerIndex.Two;res += "two";}
	else return;
	if (inverse) if (playerIndex == 1) player = PlayerIndex.Two;
	else player = PlayerIndex.One;
    GamePad.SetVibration ( player,val,val);
    if (time > 0.0f) {
    	if (player == PlayerIndex.One) time1 = Mathf.Infinity;
    	else time2 = Mathf.Infinity;
    	StartCoroutine(StopVibration(playerIndex, time));
    } else {
    	if (player == PlayerIndex.One) time1 = 0.2;
    	else time2 = 0.2;
    }
}