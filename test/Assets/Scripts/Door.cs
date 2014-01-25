using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Vector3 translationToOpen;
	public float openTime = 1.5f;

	private bool open = false;
	private Vector3 originalPos;
	private Vector3 finalPos;
	private float timeFactor = 1.0f;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		finalPos = originalPos + new Vector3(translationToOpen.x * transform.lossyScale.x,
		                                     translationToOpen.y * transform.lossyScale.y,
		                                     translationToOpen.z * transform.lossyScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (timeFactor < 1.0f) {
			timeFactor += Time.deltaTime/openTime;
			transform.position = Vector3.Lerp (originalPos, finalPos, (open) ? timeFactor : 1.0f - timeFactor);
		}
		else timeFactor = 1.0f;
	}

	void SetState (bool o) {
		open = o;
		timeFactor = 1.0f - timeFactor;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, transform.position + new Vector3(translationToOpen.x * transform.lossyScale.x,
		                                                                      translationToOpen.y * transform.lossyScale.y,
		                                                                      translationToOpen.z * transform.lossyScale.z));
	}
}
