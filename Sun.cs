using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public static Sun current;
    public Action<float> sunOutputCallback;
    [SerializeField] private float energyPerSecond = 1;

    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
        else
        {
            Debug.LogError("There should only be one sun");
            Destroy(this);
        }
    }

    void Update()
    {
        if(sunOutputCallback!= null)
        {
            sunOutputCallback(energyPerSecond * Time.deltaTime);
        }
    }
}
