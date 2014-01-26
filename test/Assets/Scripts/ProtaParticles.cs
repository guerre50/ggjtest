using UnityEngine;
using System.Collections;

public class ProtaParticle : MonoBehaviour {
	ParticleSystem[] particles;
	// Use this for initialization
	void Start () {
		particles = GetComponentsInChildren<ParticleSystem>();
	}

	public void Emit() {
		foreach(ParticleSystem ps in particles) {
			ps.Emit(10);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
