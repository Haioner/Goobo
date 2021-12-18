using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlataformDetector : MonoBehaviour
{
    public Transform player;
    public Player_Controller playerController;
    public bool check;
    [Header("WallSide")]
    public bool canWallSide = false;
    public Transform facePos;
    public Rigidbody2D rb;
    bool doOnce = true;

    void Update()
    {
        if (!canWallSide)
        {
            if (!playerController.isGrounded)
            {
                check = false;
                if (Input.GetAxisRaw("Horizontal") > .25f || Input.GetAxisRaw("Horizontal") < -.25f)
                    player.SetParent(null);
            }

            if (!check)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .125f);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Plataform"))
                    {
                        player.SetParent(hit.transform);
                    }
                    else
                    {
                        player.SetParent(null);
                    }
                    check = true;
                }
            }
        }
        else
        {
            //Wall side
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxisRaw("Horizontal") > .25f || Input.GetAxisRaw("Horizontal") < -.25f)
            {
                Invoke("BackDoOnce", .3f);
                player.SetParent(null);
                rb.gravityScale = 3;
            }

            RaycastHit2D hit2 = Physics2D.Raycast(facePos.position, transform.right, .5f);
            if (hit2.collider != null && !playerController.isGrounded)
            {
                if (hit2.collider.CompareTag("Plataform"))
                {
                    player.SetParent(hit2.transform);
                    if (doOnce)
                    {
                        doOnce = false;
                        rb.gravityScale = 0;
                        rb.velocity = Vector2.zero;
                        playerController.anim.SetBool("WallSliding", true);
                    }
                }
                else
                {
                    doOnce = true;
                    player.SetParent(null);
                    rb.gravityScale = 3;
                }
            }
        }
    }

    void BackDoOnce()
    {
        doOnce = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(facePos.position, transform.right * .5f);
    }
}
