using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	private ColorControl _color;

	// Use this for initialization
	void Start () {
		_color = ColorControl.instance;
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.color = _color.color;
	}
}
