using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : MonoBehaviour
{
    public Animator anim;
    public AudioSource stepAudio;
    public float pitch;
    public float min, max;
    public void StepSound()
    {
        pitch = Random.Range(min, max);
        stepAudio.pitch = pitch;

        stepAudio.Play();
    }

    private void Start()
    {
        if (Manager.Instance.checkpoint > 0)
        {
            anim.enabled = true;
        }
    }
}
