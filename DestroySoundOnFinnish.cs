using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySoundOnFinnish : MonoBehaviour
{
    private AudioSource audioSource;
    private float awakeTime;
    private float audioLength;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        awakeTime = Time.time;
        audioLength = audioSource.clip.length;
    }

    private void Update()
    {
        if(Time.time > awakeTime + audioLength)
        {
            Destroy(this.gameObject);
        }
    }
}

