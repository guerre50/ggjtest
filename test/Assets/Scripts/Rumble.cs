using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Rumble : GameObjectSingleton<Rumble> {
	private float[] rumbling = new float[4];
	
	private IEnumerator StopVibration(PlayerIndex playerIndex, float time) {
		float currentTime = Time.time;
		rumbling[(int)playerIndex] = currentTime;
		yield return new WaitForSeconds(time);
	 	if (rumbling[(int)playerIndex] == currentTime) GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
	}
	
	public void Vibrate(float big = 0.5f, float small = 0.5f, float time = 0.0f, int player = 1) {
		PlayerIndex  playerIndex = ToPlayerIndex(player);
		
	    GamePad.SetVibration (playerIndex, big, small);
	    if (time > 0.0f && (big > 0.0f || small > 0.0f)) StartCoroutine(StopVibration(playerIndex, time));
	}
	
	public void PadVibration (int playerIndex, float big, float small ) {
	    GamePad.SetVibration (ToPlayerIndex(playerIndex), big, small );
	}
	
	void OnApplicationQuit() {
		GamePad.SetVibration (PlayerIndex.One, 0,0);
		GamePad.SetVibration (PlayerIndex.Two, 0,0);
		GamePad.SetVibration (PlayerIndex.Three, 0,0);
		GamePad.SetVibration (PlayerIndex.Four, 0,0);
	}
	
	private PlayerIndex ToPlayerIndex(int playerIndex) {
		return PlayerIndex.One+(playerIndex-1);	
	}
}
