using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTester : MonoBehaviour {

	Text text;
	public int heals = 0;
	public int kills = 0;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		for (int i = 0; i < heals; ++i) {
			GameManager.instance.EnemyHeal ();
		}

		for (int i = 0; i < kills; ++i) {
			GameManager.instance.EnemyKill ();
		}

		SoundManager.instance.playBGM ("title");
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Time Left: " + GameManager.instance.GetTimeText ();
	}
}
