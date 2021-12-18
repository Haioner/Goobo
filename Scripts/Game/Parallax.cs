using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public Transform[] layers;
    public float[] mult;
    private Vector3[] posOriginal;

    private void Awake()
    {
        posOriginal = new Vector3[layers.Length];

        for(int i = 0; i < layers.Length; i++)
        {
            posOriginal[i] = layers[i].position;
        }
            
    }

    void FixedUpdate()
    {
        for(int i = 0; i < layers.Length; i++)
        {
            layers[i].position = posOriginal[i] + mult[i] * (new Vector3(cam.position.x, cam.position.y, layers[i].position.z));
        }
    }
}
