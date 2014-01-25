using UnityEngine;
using System.Collections;

public class Prota : MonoBehaviour {
	public float maxVelocity = 30.0f;
	public float accel = 0.5f;
	private float velocity = 0.0f;
	public int life = 4;
	public int traps = 5;
	public GameObject trap;
	private bool topView = true;
	private bool onAir = false;
	private bool jumping = false;
	public float speed = 15.0f;
	public float airMaxVelocity = 10.0f;
	public float airFriction = 60.0f;
	public float airAcceleration = 90.0f;
	public float jump = 20.0f;
	public float gravityNum = 9.87f;
	public GameObject groundParticles;
	public GameObject groundParticlesPosition;

	void Start () {
	}

	void Update () {
		UpdateMovement();
		Actions();

		if (Input.GetKeyDown(KeyCode.Q)){ 
			topView = !topView;
			Vector3 gravity = Physics.gravity;
			if (topView) {
				gravity.y = -gravityNum;
				gravity.z = 0;
			} else {
				gravity.z = -gravityNum;
				gravity.y = 0;
			}
			Physics.gravity = gravity;
		}
	}

	public void UpdateMovement() {
		if (topView) {
			TopView();
		} else {
			SideScroller();
		}
	}

	public void Actions() {
	}

	public void DealDamage() {
		life -= 1;
	}

	public void SideScroller() {
		Vector2 movement = InputController.GetMovement(1);
		Vector3 velocity = rigidbody.velocity;

		if (!onAir) {
			if (movement.x > 0){
				velocity.x = speed;
			} else if (movement.x < 0) {
				velocity.x = -speed;
			} else{
				velocity.x =0;
			}
			
			jumping = false;
		} else {
			//velocity.x = 0;
			if (movement.x < 0 && velocity.x > -airMaxVelocity) {
				velocity.x -= Time.deltaTime*airAcceleration;
			} else if (movement.x > 0 && velocity.x < airMaxVelocity) {
				velocity.x += Time.deltaTime*airAcceleration;
			}
		}
		
		// jump
		if (!jumping && InputController.GetKey(InputController.Key.A, 1)) { 
			velocity.z = jump;
			
			jumping = true;
		} else if (jumping && onAir && velocity.z > 0 && !InputController.GetKey(InputController.Key.A, 1)) { 
			// variable jump
			velocity.z -= airFriction*Time.deltaTime;
		} 
		rigidbody.velocity = velocity;
	}

	public void TopView() {
		Vector2 mov = InputController.GetMovement(1);
		if (mov.magnitude != 0) {
			velocity += accel;
		} else {
			velocity -= accel;
		}
		velocity = Mathf.Clamp(velocity, 0, maxVelocity);
		
		mov *= (velocity*Time.deltaTime);
		transform.Translate(mov.x, 0, mov.y);
	}

	void OnCollisionStay(Collision col) {
		onAir = false;
		if (jumping) {
			GameObject particles = Instantiate(groundParticles, groundParticlesPosition.transform.position, groundParticlesPosition.transform.rotation) as GameObject;
			ParticleSystem particleSystem = particles.GetComponentInChildren<ParticleSystem>();
			if (col.transform.renderer) {
				particleSystem.startColor = col.transform.renderer.material.color;
			}
			particleSystem.Emit(20);
			particles.transform.parent = col.transform;
			Destroy(particles, 0.5f);
		}
	}
	
	void OnCollisionExit(Collision col) {
		onAir = true;
	}

}
