using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Camera c = Camera.main;
		transform.LookAt (c.transform.position + c.transform.rotation * Vector3.forward, c.transform.rotation * Vector3.down); 
	}
}
