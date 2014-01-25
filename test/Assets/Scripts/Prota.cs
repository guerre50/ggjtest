using UnityEngine;
using System.Collections;

public class Prota : MonoBehaviour {
	public float maxVelocity = 30.0f;
	public float accel = 0.5f;
	private float velocity = 0.0f;
	public int life = 4;
	public int traps = 5;
	public GameObject trap;
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
	public GameObject body;
	private Logic _logic;

	void Start () {
		_logic = Logic.instance;
	}

	void Update () {
		UpdateMovement();
		Actions();
	}

	public void UpdateMovement() {
		if (_logic.gameMode == Logic.GameMode.TOPVIEW) {
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
		} else if (jumping && onAir) { 
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

	void OnCollisionEnter(Collision col) {
		GameObject particles = Instantiate(groundParticles, groundParticlesPosition.transform.position, groundParticlesPosition.transform.rotation) as GameObject;
		ParticleSystem particleSystem = particles.GetComponentInChildren<ParticleSystem>();
		if (col.transform.renderer) {
			particleSystem.startColor = col.transform.renderer.material.color;
		}
		particleSystem.Emit((int)col.relativeVelocity.magnitude);
		particles.transform.parent = col.transform;
		Destroy(particles, 0.5f);
		float scaleX = Mathf.Lerp( 0.5f, 1.0f, 2.0f/Mathf.Abs(col.relativeVelocity.x));
		float scaleZ = Mathf.Lerp( 0.5f, 1.0f, 2.0f/Mathf.Abs(col.relativeVelocity.z));
		//Debug.Log(scaleX + " " + scaleZ);
		iTween.ScaleFrom(body, iTween.Hash(
			"value", new Vector3(scaleX, 0.8f, scaleZ), 
			"time", 0.3f,
			"onupdate", "OnScale"));
	}

	public void OnScale() {
		Vector3 pos = body.transform.localPosition;
		pos.z = body.transform.localScale.z/2;
	}
	
	void OnCollisionStay(Collision col) {
		onAir = false;
		if (jumping) {

		}
	}
	
	void OnCollisionExit(Collision col) {
		onAir = true;
	}

}
