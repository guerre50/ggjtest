using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	private ColorControl _color;
	private Color initialColor;
	private float apportAmount;

	// Use this for initialization
	void Start () {
		_color = ColorControl.instance;
		initialColor = renderer.material.color;
		apportAmount = Random.Range (0.3f, 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.color = Color.Lerp (initialColor, _color.color, apportAmount);
	}
}
