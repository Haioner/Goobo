using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class FinalSceneTrigger : MonoBehaviour
{
    public PlayableDirector director;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        director.gameObject.SetActive(true);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
