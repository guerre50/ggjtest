using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	private ParticleSystem _rain;
	private int _maxRain = 50;
	private float _rainIncreaseTime = 0.2f;
	private float _rainTime;
	private CloudMovement[] _clouds;
	private Logic _logic;
	
	private bool _raining = true;
	private int _targetRain = 50;
	
	public float RainPercent {
		get {
			return ((float)_rain.emissionRate)/_maxRain;	
		}
	}
	
	void Awake () {
		_targetRain = _maxRain;
		_rain = transform.GetComponentInChildren<ParticleSystem>();
		_clouds = transform.GetComponentsInChildren<CloudMovement>();
		_logic = Logic.instance;
	}
	
	void Update () {
		RainAnimation();
	}
	
	public void StartRain() {
		_rain.Play();
		_rain.emissionRate = 1;
		_rainTime = _rainIncreaseTime;
		foreach(CloudMovement cloud in _clouds) {
			cloud.StartRain();	
		}
	}
	
	public void StopRain() {
		_rain.Stop ();
		foreach(CloudMovement cloud in _clouds) {
			cloud.StopRain();	
		}
	}
	
	public void RainAnimation() {
		float diff = _targetRain - _rain.emissionRate;
		if (diff != 0) {
			_rainTime -= Time.deltaTime;
			float diffSign = Mathf.Sign(diff);
			
			if (_rainTime <= 0.0f) {
				_rainTime = _rainIncreaseTime;
				_rain.emissionRate += diffSign;
				_rain.emissionRate = Mathf.Clamp(_rain.emissionRate, 0 , _maxRain);
			}
		}
	}
	
	public  void OnDeselect() {
		StopRain();
	}
	
	public  void OnSelect() {
		AnimateRain();
	}
	
	void AnimateRain() {
		if (_raining) {
			StartRain();
		} else {
			StopRain();	
		}	
	}
}
