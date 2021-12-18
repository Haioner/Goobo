using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SineLight : MonoBehaviour
{
    public Light2D ligt;
    public float frequency;
    public float magnitude;

    void Update()
    {
        ligt.intensity = magnitude + Mathf.Sin(Time.timeSinceLevelLoad * frequency) * magnitude;
    }
}
