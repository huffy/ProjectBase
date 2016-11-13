using UnityEngine;
using System.Collections;

public class AudioEvent : MonoBehaviour {

    AudioSource source;
    public delegate void OnClipFinish();
    public OnClipFinish onclipfinish;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        
	}
	


	// Update is called once per frame
	void Update () {
        if (!source.isPlaying)
        {
            if (onclipfinish != null) {
                onclipfinish();     
            }
           
        }
    }
}
