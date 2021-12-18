using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public int ID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Manager.Instance.SaveInt("_scene", ID);
            Manager.Instance.LoadScene();

            Manager.Instance.SaveInt("checkpoint", 0);
            Manager.Instance.LoadCheckpoint();

            Transition.Instance.StartTransition("Scene"+ ID);
        }
    }
}
