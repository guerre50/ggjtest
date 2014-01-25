using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour {
    public enum MoveMode {SIDESCROLL, TOPVIEW, BOTH}; 
    public MoveMode movableIn;
    private Logic _logic;
    private Bounce bounceScript;
	public GameObject graphics;

	// Use this for initialization
	void Start () {
        _logic = Logic.instance;
        bounceScript = GetComponent<Bounce>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsMovable())
        {
            bounceScript.enabled = true;
            rigidbody.isKinematic = false;
			graphics.renderer.material.color = new Color(255,0,255);

        }
        else
        {
            bounceScript.enabled = false;
            rigidbody.isKinematic = true;
			graphics.renderer.material.color = new Color(255,255,255);
        }
	}

    bool IsMovable()
    {
        return (movableIn == (MoveMode)_logic.gameMode) || (movableIn == MoveMode.BOTH);
    }
}
