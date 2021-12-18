using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBossTrigger : MonoBehaviour
{
    public Monster monster;
    public AudioSource source;
    public Rigidbody2D[] rb;
    public Transform particlePos;
    public GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            CameraShake.Instance.ShakeCamera(5f, 1f);
            source.Play();
            Instantiate(particle, particlePos.position, Quaternion.identity);
            for(int i = 0; i < rb.Length; i++)
            {
                rb[i].gravityScale = 4;
            }
        }
    }
}
