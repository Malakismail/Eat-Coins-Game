using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------- Audio Source ----------------------------")]

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------------- Audio Clip ----------------------------")]

    public AudioClip background;
    public AudioClip menu;
    public AudioClip levelUp;
    public AudioClip eatCoins;

    // Start is called before the first frame update
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
