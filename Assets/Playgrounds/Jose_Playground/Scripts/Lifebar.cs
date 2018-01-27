using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour {

	RectTransform rect;

	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform> ();
	}

	public void SetPerc(float p){
		rect.anchorMax = new Vector2 (p, 1f);
		rect.offsetMax = Vector2.zero;
	}
}
