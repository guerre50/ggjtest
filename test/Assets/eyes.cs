using UnityEngine;
using System.Collections;

public class eyes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0.0f, 1.0f) > 0.9f) {
			Blink();
		}
		//transform.parent.rigidbody.velocity;

	}

	void Blink() {
		//iTween.ScaleFrom (eyes, Vector3.zero, 0.1f);
	}
}
