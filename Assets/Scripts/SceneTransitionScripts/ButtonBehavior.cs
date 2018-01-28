using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour {

    [SerializeField] private int sceneIndex;

    public void DoSceneChange() { SceneManager.LoadScene(sceneIndex); }

    public void QuitGame() { Application.Quit(); }
}
