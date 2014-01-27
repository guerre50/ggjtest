using UnityEngine;
using System.Collections;

public class Lletres : MonoBehaviour {

	public bool beginText = false;

	private TextMesh t;

	// Use this for initialization
	void Start () {
		t = gameObject.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (beginText) {
			t.color = new Color(t.color.r, t.color.g, t.color.b, (Mathf.Sin(Time.time/2.0f) + 1.0f)/2.0f);;
		}
		else {
			if (Random.Range (0.0f, 1.0f) < 0.95f) {
				iTween.PunchPosition (gameObject, new Vector3 (Random.Range (-4.0f, 4.0f), 0, Random.Range (-4.0f, 4.0f)), 2.0f);
			}
		}
		if (Input.anyKeyDown) {
			OnMouseDown();
		}
	}

	void OnMouseDown() {
		Application.LoadLevel (1);
	}
}
