using UnityEngine;
using System.Collections;

public class Lletres : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (0.0f, 1.0f) < 0.95f) {
			iTween.PunchPosition(gameObject, new Vector3(Random.Range (-4.0f, 4.0f), 0, Random.Range (-4.0f, 4.0f)), 2.0f);
		}
	}
}
