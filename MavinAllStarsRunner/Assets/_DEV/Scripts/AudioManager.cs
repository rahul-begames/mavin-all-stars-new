using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource[] soundAudioSource;
   

    public AudioClip[] artistMusic;


    public void ButtonClickSound(int soundNumber)
    {
        if (PlayerPrefs.GetFloat("SFXValue") == 1)
        {
            if(soundNumber == 0)
                soundAudioSource[2].Play();
            if(soundNumber == 1)
                soundAudioSource[3].Play();
        }
        
        
    }
}
