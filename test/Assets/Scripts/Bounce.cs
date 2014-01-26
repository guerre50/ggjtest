using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	private bool _tween = false;
	private Vector3 initialScale;
	public GameObject body;
	public float vel;
	private SoundManager _sound;

	public float autoBouncePeriod = 0.0f;
	private float time = 0.0f;

	void Start() {
		  initialScale = transform.localScale;
		_sound = SoundManager.instance;
	}

	void Update() {
		//rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, Vector3.zero, 0.5f*vel);

		if (autoBouncePeriod > 0.01f) {
			time -= Time.deltaTime;

			if (time < 0.0f) {
				Blob ();
				time = autoBouncePeriod;
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		Blob (col);

		if (transform.root.gameObject.tag == "Rock")
			_sound.Play ("blob", transform.position);
		else
			_sound.Play ("bump", transform.position);
	}

	public void Blob(Collision col) {
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

	public void Blob() {
		float scaleX = Mathf.Lerp( 0.7f, 1.3f, Random.Range(0.0f, 1.0f));
		float scaleZ = Mathf.Lerp( 0.8f, 1.2f, Random.Range(0.0f, 1.0f));
		
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
