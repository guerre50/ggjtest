using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(3, 3, 3);
		Destroy (gameObject, 0.2f);
		iTween.ShakePosition(Camera.main.gameObject, new Vector3(2, 0, 2), 0.2f);
		_.Wait(0.2f).Done (() => {InputController.Vibrate(0.8f, 0.1f, 0.1f, 1); });
		InputController.Vibrate(0.2f, 0.5f, 0.2f, 1);
	}
}
