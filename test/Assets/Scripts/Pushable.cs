using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour {
    public enum MoveMode {SIDESCROLL, TOPVIEW, BOTH}; 
    public MoveMode movableIn;
    private Logic _logic;
    private Bounce bounceScript;
	public GameObject graphics;
    public Color currentColor = new Color(255,255,255);

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
        }
        else
        {
            bounceScript.enabled = false;
            rigidbody.isKinematic = true;
        }

        if (movableIn == MoveMode.SIDESCROLL)
        {
            if (_logic.gameMode == Logic.GameMode.SIDESCROLL)
            {
                graphics.renderer.material.color = _logic.KEYBOXSIDE;
            }
            else
            {
                graphics.renderer.material.color = _logic.KEYBOXNEUTRAL;
            }

            currentColor = _logic.KEYBOXSIDE;
        }
        else if (movableIn == MoveMode.TOPVIEW)
        {
            if (_logic.gameMode == Logic.GameMode.TOPVIEW)
            {
                graphics.renderer.material.color = _logic.KEYBOXTOP;
            }
            else
            {
                graphics.renderer.material.color = _logic.KEYBOXNEUTRAL;
            }
            currentColor = _logic.KEYBOXTOP;
        }
        else
        {
            graphics.renderer.material.color = _logic.KEYBOXBOTH;
            currentColor = _logic.KEYBOXBOTH;
        }
        
	}

    bool IsMovable()
    {
        return (movableIn == (MoveMode)_logic.gameMode) || (movableIn == MoveMode.BOTH);
    }
}
