using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffect;

    public AudioSource backgroundMusic, levelEndMusic;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffect[soundToPlay].Stop();
        soundEffect[soundToPlay].pitch = Random.Range(.7f, 1.3f);
        soundEffect[soundToPlay].Play();
    }
}
