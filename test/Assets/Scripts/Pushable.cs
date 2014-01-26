using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour {
    public enum MoveMode {SIDESCROLL, TOPVIEW, BOTH}; 
    public MoveMode movableIn;
    private Logic _logic;
    private Bounce bounceScript;
	public GameObject graphics;
    public Color currentColor;
	private ColorControl _color;
	private Color initialColor;

	
	public Color KEYBOXBOTH;
	public Color KEYBOXTOP;
	public Color KEYBOXSIDE;
	
	public Color KEYBOXNEUTRAL;
	
	// Use this for initialization
	void Start () {
		KEYBOXBOTH = new Color(255.0f/255.0f,58.0f/255.0f,245.0f/255.0f);
		KEYBOXTOP = new Color(232.0f/255.0f,58.0f/255.0f,31.0f/255.0f);
		KEYBOXSIDE = new Color(90.0f/255.0f, 41.0f/255.0f, 232.0f/255.0f);
		KEYBOXNEUTRAL = new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);


		initialColor = renderer.material.color;
        _logic = Logic.instance;
		_color = ColorControl.instance;
        bounceScript = GetComponent<Bounce>();
		currentColor = (movableIn == MoveMode.SIDESCROLL)? KEYBOXSIDE : ((movableIn == MoveMode.TOPVIEW)? KEYBOXTOP : KEYBOXBOTH);
		graphics = transform.GetChild (0).gameObject;
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


		if (_logic.gameMode == Logic.GameMode.SIDESCROLL) {
			if (rigidbody.drag != 0)	rigidbody.drag = 0;
		} else {
			if (rigidbody.drag != 10) rigidbody.drag = 10;
		}

        if (movableIn == MoveMode.SIDESCROLL)
        {
            if (_logic.gameMode == Logic.GameMode.SIDESCROLL)
            {
                graphics.renderer.material.color = KEYBOXSIDE;
            }
            else
            {
                graphics.renderer.material.color = Color.Lerp(initialColor, _color.color, 0.9f);
            }

            //currentColor = _logic.KEYBOXSIDE;
        }
        else if (movableIn == MoveMode.TOPVIEW)
        {
            if (_logic.gameMode == Logic.GameMode.TOPVIEW)
            {
                graphics.renderer.material.color = KEYBOXTOP;
            }
            else
            {
				graphics.renderer.material.color = Color.Lerp(initialColor, _color.color, 0.9f);
            }
            //currentColor = _logic.KEYBOXTOP;
        }
        else
        {
            graphics.renderer.material.color = KEYBOXBOTH;
            //currentColor = _logic.KEYBOXBOTH;
        }
        
	}

    bool IsMovable()
    {
        return (_logic.gameState == Logic.GameState.PLAYING) && ((movableIn == (MoveMode)_logic.gameMode) || (movableIn == MoveMode.BOTH));
    }
}
