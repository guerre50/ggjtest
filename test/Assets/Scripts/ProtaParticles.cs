using UnityEngine;
using System.Collections;

public class ProtaParticles : MonoBehaviour {
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

	public void EmitAt(Vector3 position) {
		Vector3 pos = particles[0].transform.position;
		particles[0].transform.position = position;
		particles[0].Emit (Random.Range (5, 20));

		//particles[0].transform.position = pos;
	}
}
