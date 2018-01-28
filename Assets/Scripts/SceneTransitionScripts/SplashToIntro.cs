using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashToIntro : MonoBehaviour {

    private float waitingTime; // should coincide with the animation...
    private int introIndex;

    void Awake() {
        waitingTime = 3.6f;
        introIndex = 1;
        Invoke("ChangeScene", waitingTime);
    }

    void ChangeScene() { SceneManager.LoadScene(introIndex); }
}
