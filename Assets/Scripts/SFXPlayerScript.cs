using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayerScript : MonoBehaviour
{
    public static SFXPlayerScript Instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playSFX(AudioClip clip)
    {

        audioSource.PlayOneShot(clip);
    }
}
