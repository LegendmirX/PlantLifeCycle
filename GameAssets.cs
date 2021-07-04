using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour 
{
    [Header("GameObjects")]
    public GameObject Plant;
    public GameObject Fruit;
    public GameObject Seed;
    
    [Space]
    [Header("Sounds")]
    public SoundAudioClip[] soundAudioClipArray;

    public static GameAssets i;

    public void Awake()
    {
        if (i != null)
        {
            Debug.LogError("There should only be one GameAssets");
            Destroy(this);
        }
        else
        {
            i = this;
        }
    }

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
