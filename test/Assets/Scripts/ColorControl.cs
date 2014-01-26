using UnityEngine;
using System.Collections;

public class ColorControl : GameObjectSingleton<ColorControl> {

	private Music music;
	private float bpm = 30.0f;
	public Color color;
	public Color[] colors;

	private float secondsBetweenBeats;
	private float lastBeat = 0.0f;
	private Color nextColor;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		color = Color.gray;
		originalColor = color;
		nextColor = Color.gray;
		music = transform.gameObject.GetComponent<Music> ();
		secondsBetweenBeats = 60.0f / bpm;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = music.audio.time - lastBeat;
		if (deltaTime > secondsBetweenBeats) {
			ChangeColor();
			lastBeat = music.audio.time;
		}
		else color = Color.Lerp (originalColor, nextColor, deltaTime * 2.0f / secondsBetweenBeats);
	}

	void ChangeColor() {
		int i = Random.Range (0, colors.Length);
		nextColor = colors [i];
		originalColor = color;
	}
}
