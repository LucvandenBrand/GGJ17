using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioImpactListener : MonoBehaviour {
    public abstract void audioImpact(float speed, float intensity);
}
