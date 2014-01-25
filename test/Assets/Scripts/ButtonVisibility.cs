using UnityEngine;
using System.Collections;

public class ButtonVisibility : MonoBehaviour {

	private Button button;

	void Start () {
		button = transform.parent.GetComponent<Button> ();
	}

	void Update () {
		renderer.enabled = !button.InOwnMode ();
	}
}
