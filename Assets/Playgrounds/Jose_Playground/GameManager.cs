using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public static GameManager instance{
		get{
			if (_instance == null) {
				_instance = FindObjectOfType<GameManager> ();
				if (_instance == null) {
					GameObject obj = new GameObject ();
					_instance = obj.AddComponent<GameManager> ();
					DontDestroyOnLoad (_instance);
				}
			} else {
				GameManager ob = FindObjectOfType<GameManager> ();
				if (ob != null && ob != _instance) {
					Destroy (ob);
				}
			}
			return _instance;
		}
	}

	public float timeLimit = 120f;

	private int kills;
	private int heals;
	private float morality;
	private int remainingBases;
	private bool gameEnded = false;
	private float timeLeft;

	void Awake(){
		Reset ();
	}

	void Start(){
		StartCoroutine (StartTimer ());
	}

	IEnumerator StartTimer(){
		while (timeLeft > 0f && !gameEnded) {
			timeLeft -= Time.deltaTime;
			yield return null;
		}

		if (gameEnded) {
			LoseGame ();
		} else {
			WinGame ();
		}
	}	

	void Reset(){
		kills = 0;
		heals = 0;
		morality = 0.5f;
		timeLeft = timeLimit;
		remainingBases = 0;
		gameEnded = false;
	}

	public void AddBase(){
		remainingBases += 1;
	}

	public void RemoveBase(){
		remainingBases -= 1;
		if (remainingBases <= 0) {
			gameEnded = true;
		}
	}

	public void EnemyKill(){
		kills += 1;
		UpdateMorality ();
	}

	public void EnemyHeal(){
		heals += 1;
		UpdateMorality ();
	}

	private void UpdateMorality(){
		morality = (float)heals / (float)(kills + heals);
	}

	public float GetTimeLeft(){
		return timeLeft;
	}

	public string GetTimeText(){
		int it = Mathf.CeilToInt (timeLeft);
		int mins = it / 60;
		int secs = it % 60;
		string minutesText = (mins < 10 ? "0" : "") + mins + ":";
		string secondsText = (secs < 10 ? "0" : "") + secs;
		return minutesText + secondsText;
	}

	void WinGame(){
		Debug.Log ("Time over. You won!");
		Debug.Log ("Final Morality: " + morality);
	}

	void LoseGame(){
		Debug.Log ("Base is Destroyed. Game Over");
	}
}
