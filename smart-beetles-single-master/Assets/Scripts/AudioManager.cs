using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip changeWindow;
    public AudioClip crash;
    public AudioClip select;
    public AudioClip garbageCollected;

    private AudioSource audioSource;

   
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void playClick()
    {
        audioSource.clip = changeWindow;
        audioSource.Play();
    }

    public void playCrash()
    {
        audioSource.clip = crash;
        audioSource.Play();
    }

    public void playSelect()
    {
        audioSource.clip = select;
        audioSource.Play();
    }

    public void playGarbageCollected()
    {
        audioSource.clip = garbageCollected;
        audioSource.Play();
    }
}
