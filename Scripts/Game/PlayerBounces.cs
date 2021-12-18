using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounces : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float force;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trampolim"))
        {
            anim.SetTrigger("Jump");
            collision.gameObject.GetComponent<Animator>().Play("On");
            collision.gameObject.GetComponent<AudioSource>().Play();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        }
    }

}
