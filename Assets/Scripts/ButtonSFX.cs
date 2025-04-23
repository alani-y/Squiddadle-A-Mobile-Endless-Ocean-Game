using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource sfxPlayer;

    void Start()
    {
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        sfxPlayer.PlayOneShot(clickSound);
    }
}
