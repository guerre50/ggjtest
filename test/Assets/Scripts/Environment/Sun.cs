using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	public GameObject overlay;
	
	private Vector3 _overlayTarget;
	private Vector3 _nightPosition;
	private Vector3 _dayPosition;
	private float _overlaySpeed;
	private Logic _logic;
	
	public float inSpeed;
	public float outSpeed;
	
	void Start() {
		_logic = Logic.instance;
		//InitOverlay();
	}
	
	
	void Update () {
		overlay.transform.position = Vector3.MoveTowards(overlay.transform.position, _overlayTarget, Time.deltaTime*_overlaySpeed);
	}
	
	void InitOverlay() {
		Vector3 center = Vector3.zero;//worldRect.center;
		center.z = overlay.transform.position.z;
		_dayPosition = center;
		_nightPosition = _dayPosition + Vector3.up*(overlay.transform.localScale.y*10);
		overlay.transform.position = _nightPosition;
		_overlayTarget = _nightPosition;
		overlay.SetActive(true);	
	}
	
	
	public  void OnSelect() {
		_overlayTarget = _dayPosition;
		_overlaySpeed = inSpeed;
	}
	
	public  void OnDeselect() {
		_overlayTarget = _nightPosition;
		_overlaySpeed = outSpeed;
	}
}
