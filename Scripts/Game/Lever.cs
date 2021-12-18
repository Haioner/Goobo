using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public static int isOpen = 0; //0 no // 1 yes
    public Animator anim;
    public AudioSource leverSound;
    public AudioSource gateSound;
    public Animator gate;

    private void Start()
    {
        if(isOpen == 1)
        {
            anim.Play("Idle");
            gate.Play("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isOpen == 0)
        {
            isOpen = 1;
            anim.Play("Idle");
            leverSound.Play();
            gate.Play("Open");
            gateSound.Play();
        }
    }
}
