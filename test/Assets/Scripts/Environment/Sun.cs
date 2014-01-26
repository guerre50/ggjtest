using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	public GameObject innerSun;
	public GameObject outerSun;

	public void Start() {
		iTween.MoveTo (gameObject, iTween.Hash(
			"position", transform.position + transform.up*0.3f,
			"time", 3.0f,
			"looptype", iTween.LoopType.pingPong
			));

		/*iTween.ScaleTo (innerSun, iTween.Hash(
			"scale", innerSun.transform.localScale*1.05f,
			"time", 2.0f,
			"easeType", iTween.EaseType.easeInBack,
			"looptype", iTween.LoopType.pingPong
			));*/

		_.Wait (0.0f).Done (()=>{iTween.ScaleTo (outerSun, iTween.Hash(
			"scale", outerSun.transform.localScale*0.95f,
			"time", 3.0f,
			"looptype", iTween.LoopType.pingPong
				));});
	}

}
