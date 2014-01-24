using UnityEngine;
using System.Collections;

public abstract class GameObjectSingleton<T> : MonoBehaviour where T : GameObjectSingleton<T> {
	private static T _Instance = null;
	
	public static T instance {
        get {
            if (_Instance == null) {
				_Instance = FindObjectOfType(typeof (T)) as T;
	            if (_Instance == null) {
	                GameObject obj = new GameObject(typeof(T).ToString());
	                _Instance = obj.AddComponent(typeof (T)) as T;
	            }
			}
 
            return _Instance;
        }
    }
	
	public virtual void Init(){}
	
	private void Awake() {
		if (_Instance == null) {
			_Instance = this as T;
			_Instance.Init();
		}
	}
	
    void OnApplicationQuit() {
        _Instance = null;
    }
}