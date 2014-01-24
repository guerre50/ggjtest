using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		col.gameObject.SendMessage("DealDamage", SendMessageOptions.DontRequireReceiver);
		Destroy (gameObject);
	}
}
