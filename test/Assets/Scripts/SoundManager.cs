using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : GameObjectSingleton<SoundManager> {
    public List<AudioClip> clips = new List<AudioClip>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Play(string clipName, Vector3 point)
    {
        AudioClip clip = clips.Find(x => x.name == clipName);
        if (clip)
        {
            AudioSource.PlayClipAtPoint(clip, point);
        }
        else
        {
            Debug.Log("Sound " + clipName + " not found");
        }
    }
}
