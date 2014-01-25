using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	private bool _tween = false;
	private Vector3 initialScale;
	public GameObject body;

	void Start() {
		  initialScale = transform.localScale;
	}

	void OnCollisionEnter(Collision col) {
		float scaleX = Mathf.Lerp( 1.0f, 1.3f, Mathf.Abs(col.relativeVelocity.x)/2.0f);
		float scaleZ = Mathf.Lerp( 0.8f, 1.0f, 2.0f/Mathf.Abs(col.relativeVelocity.z));

		transform.localScale = initialScale; 
		if (!_tween) {
			_tween = true;
			iTween.ScaleFrom(body, iTween.Hash(
				"scale", new Vector3(scaleX, 0.8f, scaleZ), 
				"time", 0.5f,
				"onUpdate", "OnScale",
				"onUpdateTarget", gameObject,
				"onComplete", "OnScaleComplete",
				"onCompleteTarget", gameObject));
		}
	}

	public void OnScale() {
		Vector3 pos = transform.localPosition;
		pos.z = transform.localScale.z/2;
	}
	
	public void OnScaleComplete() {
		_tween = false;
		transform.localScale = initialScale;
	}
}
