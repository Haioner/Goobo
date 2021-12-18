using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullAndPush : MonoBehaviour
{
    public LayerMask boxMask;
    public float radius;
    public Transform frontChecker;
    public Player_Controller playerController;
    public AudioSource rockSource;
    public Animator anim;
    public Rigidbody2D rb;
    public GameObject box;
    bool doOnce = true;

    void Update()
    {
        CheckBox();
    }


    void CheckBox()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(frontChecker.position, transform.right, radius, boxMask);

        if(hit.collider != null)
        {
            if (doOnce)
            {
                anim.SetBool("Pushing", true);

                if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
                {
                    rockSource.Play();
                    doOnce = false;
                }
            }
        }
        else
        {
            anim.SetBool("Pushing", false);
            rockSource.Stop();
            doOnce = true;
        }

        if(rockSource.isPlaying && Input.GetAxisRaw("Horizontal") == 0)
        {
            rockSource.Stop();
            doOnce = true;
        }

        if ((Input.GetKey(KeyCode.E)|| Input.GetMouseButton(1)) && hit.collider != null && hit.collider.gameObject.CompareTag("Pushable") && playerController.isGrounded)
        {
            box = hit.collider.gameObject;
            FixedJoint2D boxFixedJoint = box.GetComponent<FixedJoint2D>();
            boxFixedJoint.enabled = true;
            boxFixedJoint.connectedBody = rb;

            anim.SetBool("Pushing", true);
        }
        else if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(1))
        {
            if (box != null)
                box.GetComponent<FixedJoint2D>().enabled = false;
            box = null;
            anim.SetBool("Pushing", false);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(frontChecker.position, transform.right * radius);
    }
}
