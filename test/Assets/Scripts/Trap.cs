using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	public int Player { get; set; }
	public GameObject explosion;

	void Update() {
		if (Player != Logic.instance.GetCurrentPlayer()) {
			Color c = renderer.material.color;
			c.a = 0;
			renderer.material.color = c;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (Player != Logic.instance.GetCurrentPlayer()) {
			col.gameObject.SendMessage("DealDamage", SendMessageOptions.DontRequireReceiver);

			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
