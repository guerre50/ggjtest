using UnityEngine;
using System.Collections;

public class Prota : MonoBehaviour {
	public float maxVelocity = 30.0f;
	public float accel = 0.5f;
	private float velocity = 0.0f;
	public int life = 4;
	private Vector3 initialScale;
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
	private bool _tween = false;
	public Vector2 multipliers;
	public GameObject grabber;
	public GameObject grabbObject;
	public FixedJoint joint;


	void Start () {
		_logic = Logic.instance;
		initialScale = transform.localScale;
		Physics.maxAngularVelocity = speed;
		rigidbody.maxAngularVelocity = speed;
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
		Vector2 movement = InputController.GetMovement(_logic.GetCurrentPlayer());
		Vector3 velocity = rigidbody.velocity;
		Vector3 angularVelocity = rigidbody.angularVelocity;

		if (!onAir) {
			if (movement.x > 0){
				angularVelocity.y = speed;
			} else if (movement.x < 0) {
				angularVelocity.y = -speed;
			} else{
				angularVelocity.y =0;
			}
			
			jumping = false;
		} else {
			//velocity.x = 0;
			if (movement.x < 0 && velocity.y > -airMaxVelocity) {
				velocity.x -= Time.deltaTime*airAcceleration;
			} else if (movement.x > 0 && velocity.y < airMaxVelocity) {
				velocity.x += Time.deltaTime*airAcceleration;
			}
		}
		
		// jump
		if (!jumping && InputController.GetKey(InputController.Key.A, _logic.GetCurrentPlayer())) { 
			velocity.z = jump;
			
			jumping = true;
			onAir = true;
			//iTween.Stop(body);
			transform.localScale = initialScale;
			if (!_tween) {
				_tween = true;  
				iTween.ScaleFrom(body, iTween.Hash(
				"scale", new Vector3(0.7f, 0.8f, 1.1f), 
				"time", 0.5f,
				"onComplete", "OnScaleComplete",
				"onCompleteTarget", gameObject));
			}
		} else if (jumping && onAir) { 
			// variable jump
			velocity.z -= airFriction*Time.deltaTime;
		} 
		rigidbody.angularVelocity = angularVelocity;
		rigidbody.velocity = velocity;
	}

	public void TopView() {
		Vector2 mov = InputController.GetMovement(_logic.GetCurrentPlayer());
		if (mov.magnitude != 0) {
			velocity += accel;
		} else {
			velocity -= accel;
		}
		velocity = Mathf.Clamp(velocity, 0, maxVelocity);
		

		float multiplier = multipliers.x;
		if (mov.magnitude == 0) {
			multiplier = multipliers.y;
		}
		mov *= (velocity*multiplier*Time.deltaTime);
		mov = Vector3.ClampMagnitude(mov, maxVelocity);
		transform.rigidbody.velocity = Vector3.MoveTowards (transform.rigidbody.velocity, new Vector3(mov.x, 0, mov.y), 1f);


	}

	void GrabLogic(Collision col) {
		if (col.gameObject.tag == "Rock" && (int)col.gameObject.GetComponent<Pushable>().movableIn == (int)Logic.instance.gameMode) {
			grabber.transform.position = col.contacts[0].point + ((col.contacts[0].point - transform.position)*1.2f);
			col.gameObject.rigidbody.velocity = Vector3.right;//;(transform.position - col.gameObject.transform.position);
		}
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Rock" && (int)col.gameObject.GetComponent<Pushable>().movableIn == (int)Logic.instance.gameMode) {
			grabber.transform.position = col.gameObject.transform.position + ((col.gameObject.transform.position - transform.position)*1.2f);
			col.gameObject.rigidbody.velocity = (transform.position - col.gameObject.transform.position);
		}
	}

	void OnCollisionEnter(Collision col) {
		GrabLogic(col);


		if (_logic.gameMode == Logic.GameMode.SIDESCROLL) {
			Vector3 particlesPosition = col.contacts[0].point;
			particlesPosition.y = groundParticlesPosition.transform.position.y;

			GameObject particles = Instantiate(groundParticles, particlesPosition, groundParticlesPosition.transform.rotation) as GameObject;
			ParticleSystem particleSystem = particles.GetComponentInChildren<ParticleSystem>();

			if (col.transform.renderer) {
				particleSystem.startColor = col.transform.renderer.material.color;
			}
			particleSystem.transform.forward = col.contacts[0].normal;
			particleSystem.Emit((int)col.relativeVelocity.magnitude/2);
			particles.transform.parent = col.transform;
			Destroy(particles, 1.0f);
		}
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

	public void OnScale() {
		Vector3 pos = body.transform.localPosition;
		pos.z = body.transform.localScale.z/2;
	}

	public void OnScaleComplete() {
		_tween = false;
		body.transform.localScale = initialScale;
	}
	
	void OnCollisionStay(Collision col) {
		if (col.contacts.Length > 0) {
			Vector3 inverseNormal = transform.InverseTransformDirection(col.contacts[0].normal);
			if(col.contacts[0].normal.z >= 0 && Mathf.Abs(inverseNormal.x) <= 0.5f) {
				onAir = false;
				if (jumping) {
					
				}
			}
		}

	}
	
	void OnCollisionExit(Collision col) {
		onAir = true;
	}

}
