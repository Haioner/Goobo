using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFace : MonoBehaviour
{
    public GameObject[] faces;

    void Start()
    {
        int rand = Random.Range(0, faces.Length);
        faces[rand].SetActive(true);
    }

}
