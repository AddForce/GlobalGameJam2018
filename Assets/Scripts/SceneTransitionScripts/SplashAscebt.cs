using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashAscebt : MonoBehaviour {

    [SerializeField] private float txtSpeed;

    private int sceneIndex;
    private Text txt;
    private float alphaAdjust;
    private Color newColor;
    private float destination;
    private float scaleFactor;
    private float scaleAdjust;

    void Awake() {
        txt = this.gameObject.GetComponentInChildren<Text>();
        alphaAdjust = 0.005f;
        newColor = new Color(0.0f, 0.0f, 0.0f, alphaAdjust);
        destination = 200.0f;
        scaleFactor = 2.0f;
        scaleAdjust = 0.001f;
    }

	// Use this for initialization
	void Start () {
        txt.gameObject.transform.Translate(new Vector2(0.0f, -150.0f));
        txt.color = new Color(1.0f, 1.0f, 1.0f, alphaAdjust);
	}
	
	// Update is called once per frame
	void Update () {
        if (txt.color.a <= 1.0f && txt.transform.position.y < destination) txt.color += newColor;

        if (txt.transform.position.y < destination) txt.transform.Translate(new Vector2(0.0f, txtSpeed));

        if (txt.transform.position.y >= destination) {
            txt.gameObject.transform.Translate(new Vector2(0.0f, txtSpeed/4));
            if (txt.gameObject.transform.localScale.magnitude <= scaleFactor) {
                txt.gameObject.transform.localScale += new Vector3(scaleAdjust, scaleAdjust, scaleAdjust);
                txt.color -= newColor*2;
            }
        }

        if (txt.transform.position.y >= 300.0f) SceneManager.LoadScene(1);
	}
}
