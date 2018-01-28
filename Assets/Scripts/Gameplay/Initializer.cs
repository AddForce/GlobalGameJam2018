using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {

	public string bgmName;

	// Use this for initialization
	void Start () {
		SoundManager.instance.playBGM (bgmName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
