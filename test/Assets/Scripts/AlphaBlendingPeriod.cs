using UnityEngine;
using System.Collections;

public class AlphaBlendingPeriod : MonoBehaviour {

	public float period = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Color c = renderer.material.color;
		renderer.material.color = new Color (c.r, c.g, c.b, (Mathf.Sin (Time.time / period) + 1.0f)/2.2f + 0.2f);
	}
}
