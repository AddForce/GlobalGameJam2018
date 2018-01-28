using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if (hit) {
				if (hitInfo.transform.gameObject.tag == "Enemy") {
					hitInfo.transform.gameObject.GetComponent<Enemy> ().Heal (0.2f);
				} else {
					Debug.Log ("nopz");
				}
			}
		} else if (Input.GetMouseButtonDown (1)) {

			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if (hit) {
				if (hitInfo.transform.gameObject.tag == "Enemy") {
					hitInfo.transform.gameObject.GetComponent<Enemy> ().DealDamage (2);
				} else {
					Debug.Log ("nopz");
				}
			}
		}
	}
}
