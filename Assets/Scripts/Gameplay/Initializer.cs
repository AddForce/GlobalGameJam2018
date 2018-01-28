using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {

	public string bgmName;
    public bool isBGM = true;
    public bool stopPrevious = false;

	// Use this for initialization
	void Start () {
        if (isBGM){
            SoundManager.instance.playBGM(bgmName);
        }else{
			if (stopPrevious){
				SoundManager.instance.stopBGM();
			}
            SoundManager.instance.PlaySFX(bgmName);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
