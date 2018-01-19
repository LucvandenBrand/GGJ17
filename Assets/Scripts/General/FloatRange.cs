using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Returns a random float between a given range. */
[System.Serializable]
public class FloatRange
{
    [Range(0,1)]
    public float min;
    [Range(0,2)]
    public float max;

    public FloatRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float GetRandomBetween() {
        return Random.Range(min, max);
    }
}
