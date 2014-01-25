using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	private GameObject cam;

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void OnLevelWasLoaded(int level) {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	void Start() {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		audio.loop = true;
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = cam.transform.position;
	}
}
