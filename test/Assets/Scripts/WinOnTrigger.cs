using UnityEngine;
using System.Collections;

public class WinOnTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player")
			if (Application.loadedLevel + 1 == Application.levelCount) 
					Application.LoadLevel (1);
			else Application.LoadLevel (Application.loadedLevel + 1);
	}
}
