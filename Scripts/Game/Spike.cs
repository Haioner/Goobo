using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    bool doOnce = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && doOnce)
        {
            doOnce = false;
            GetComponent<AudioSource>().Play();
            FindObjectOfType<Player_Controller>().rb.velocity = Vector3.zero;
            FindObjectOfType<Player_Controller>().canMove = false;
            CameraShake.Instance.ShakeCamera(5f, .1f);

            Scene _scene = SceneManager.GetActiveScene();
            string sceneName = _scene.name;
            Transition.Instance.StartTransition(sceneName);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && doOnce)
        {
            doOnce = false;
            GetComponent<AudioSource>().Play();
            FindObjectOfType<Player_Controller>().rb.velocity = Vector3.zero;
            FindObjectOfType<Player_Controller>().canMove = false;
            CameraShake.Instance.ShakeCamera(5f, .1f);

            Scene _scene = SceneManager.GetActiveScene();
            string sceneName = _scene.name;
            Transition.Instance.StartTransition(sceneName);

        }
    }
}
