using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[System.Serializable]
	public class AudioClipRecord{
		public string key;
		public AudioClip clip;
	}

	private static SoundManager _instance;

	public static SoundManager instance{
		get{
			if (_instance == null) {
				_instance = FindObjectOfType<SoundManager> ();
				if (_instance == null) {
					GameObject obj = new GameObject ();
					_instance = obj.AddComponent<SoundManager> ();
					DontDestroyOnLoad (_instance);
				}
			} else {
				SoundManager ob = FindObjectOfType<SoundManager> ();
				if (ob != null && ob != _instance) {
					Destroy (ob);
				}
			}
			return _instance;
		}
	}

	public AudioClipRecord[] clipIndex;

	AudioSource source;
	Dictionary<string, AudioClip> bgms;
	string currentBGM = null;

	// Use this for initialization
	void Start() {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (this);
		}
		source = GetComponent<AudioSource> ();
		bgms = new Dictionary<string, AudioClip> ();

		for (int i = 0; i < clipIndex.Length; ++i) {
			bgms.Add (clipIndex [i].key, clipIndex [i].clip);
		}
	}

	public void playBGM(string key){
		if (currentBGM == key) {
			return;
		}

		AudioClip cl;
		bgms.TryGetValue (key, out cl);
		if (cl == null) {
			Debug.LogWarning ("Undefined key '" + key + "' requested to SoundManager");
		} else {
			source.clip = cl;
			source.Play ();
			currentBGM = key;
		}
	}

	public void stopBGM(){
		source.Stop ();
	}

    public void PlaySFX(string key)
    {
        AudioClip cl;
        bgms.TryGetValue(key, out cl);
        if (cl == null)
        {
            Debug.LogWarning("Undefined key '" + key + "' requested to SoundManager");
        }
        else
        {
            source.PlayOneShot(cl);
        }
    }

	public void PlaySFX(AudioClip cl){
		source.PlayOneShot (cl);
	}

	public void AddBGM(string key, AudioClip clip){
		bgms.Add (key, clip);
	}

}
