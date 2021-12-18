using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Manager manager;
    public int CheckPointID;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.SaveInt("checkpoint", CheckPointID);
        manager.LoadCheckpoint();
    }


}
