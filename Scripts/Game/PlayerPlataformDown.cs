using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlataformDown : MonoBehaviour
{
    private GameObject currentOneWayPlataform;
    [SerializeField] private CapsuleCollider2D playerCollider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOneWayPlataform != null)
                StartCoroutine("DisableCollision");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlataform"))
            currentOneWayPlataform = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlataform"))
            currentOneWayPlataform = null;
    }

    private IEnumerator DisableCollision()
    {
        EdgeCollider2D plataformCollider = currentOneWayPlataform.GetComponent<EdgeCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, plataformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, plataformCollider, false);
    }
}
