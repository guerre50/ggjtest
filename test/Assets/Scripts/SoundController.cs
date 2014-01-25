using UnityEngine;
using System.Collections;
using System;


public class SoundController : GameObjectSingleton<SoundController> {
	SfxrSynth synth = new SfxrSynth();
	public enum Sound {EXPLOSION };


	public void Play(Sound sound) {
		switch(sound) {
		case Sound.EXPLOSION:
			synth.parameters.SetSettingsString("3,,0.2343,0.5106,0.4512,0.0767,,0.0285,,,,,,,,0.3334,-0.0558,-0.0112,1,,,,,0.5");
			break;
		}
		synth.CacheSound(() => synth.PlayMutated());
	}

}