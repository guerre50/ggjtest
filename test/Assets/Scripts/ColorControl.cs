using UnityEngine;
using System.Collections;

public class ColorControl : GameObjectSingleton<ColorControl> {

	private Music music;
	private float bpm = 60.0f;
	private float colorChange = 20.0f;
	public Color color;

	private float secondsBetweenBeats;
	private float lastBeat = 0.0f;
	private Color nextColor;

	private Vector3 colorVelocity;
	private Vector3 colorAcc;

	// Use this for initialization
	void Start () {
		color = Color.gray;
		nextColor = Color.gray;
		music = transform.gameObject.GetComponent<Music> ();
		secondsBetweenBeats = bpm / 60.0f;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = music.audio.time - lastBeat;
		if (deltaTime > secondsBetweenBeats) {
			ChangeColor();
			lastBeat = music.audio.time;
		}


		color = new Color(color.r +  colorVelocity.x * Time.deltaTime,
							color.g + colorVelocity.y * Time.deltaTime,
		                    color.b + colorVelocity.z * Time.deltaTime);
		colorVelocity += colorAcc * Time.deltaTime;
		colorAcc = Vector3.Lerp( colorAcc, new Vector3 (0.0f, 0.0f, 0.0f), Time.deltaTime);
		//Debug.Log (colorAcc);
	}

	void ChangeColor() {
		colorAcc = new Vector3 (Random.Range (-colorChange, colorChange), Random.Range (-colorChange, colorChange), Random.Range (-colorChange, colorChange));
	}
}
