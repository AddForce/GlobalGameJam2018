using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class showTimer : MonoBehaviour {

    public Text timerText;
    public Image fillImage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer();
		
	}


    void Timer() {
        float time = GameManager.instance.GetTimeLeft();
        float totalTime = GameManager.instance.timeLimit;
        if (time > 0) {
            fillImage.fillAmount = time / totalTime;
            timerText.text = time.ToString("F");
            if (time >= totalTime / 2 - 0.1f && time <= totalTime / 2 + 0.1f) {
                fillImage.color = new Color32(255, 255, 0, 255);
            }
            if (time >= totalTime / 3 - 0.1f && time <= totalTime / 3 + 0.1f) {
                fillImage.color = new Color32(255, 0, 0, 255);
            }
        } else {
            timerText.text = "Times up!";
        }
    }
}
