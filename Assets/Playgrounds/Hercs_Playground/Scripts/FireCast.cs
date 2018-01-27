using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCast : MonoBehaviour {

    [SerializeField] private float rotSpeed;

    private Color thisColor;
    private GameObject child;

    void Awake(){
        child = GameObject.Find("Firecast");
        child.SetActive(false);
        this.gameObject.transform.localScale = Vector3.zero;
        thisColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(Vector3.up*rotSpeed*Time.deltaTime);
        //this.gameObject
	}
}
