using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour {
    public Logic.GameMode movableIn;
    private Logic _logic;

	// Use this for initialization
	void Start () {
        _logic = Logic.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsMovable())
        {
            rigidbody.isKinematic = false;
            renderer.material.color = new Color(255,0,255);
        }
        else
        {
            rigidbody.isKinematic = true;
            renderer.material.color = new Color(255,255,255);
        }
	}

    bool IsMovable()
    {
        return movableIn == _logic.gameMode;
    }
}
