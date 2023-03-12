using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    [SerializeField] private float maxVal;
    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            maxVal = value;
        }
    }

    [SerializeField] private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            this.currentVal = Mathf.Clamp(value, 0, maxVal);
        }
    }

    public void Initialise ()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
