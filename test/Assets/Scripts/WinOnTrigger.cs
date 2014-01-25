using UnityEngine;
using System.Collections;

public class WinOnTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player")
			Application.LoadLevel ((Application.loadedLevel + 1)%Application.levelCount);
	}
}
