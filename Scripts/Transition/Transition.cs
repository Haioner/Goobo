using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public static Transition Instance { get; private set; }
    public Animator anim;
    string sceneName;

    private void Awake()
    {
        Instance = this;
    }

    public void StartTransition(string _sceneName)
    {
        //Play Off transition and set the scene name
        anim.Play("Off");
        sceneName = _sceneName;
    }

    public void ChangeSceneTo()
    {
        //Change scene (in event annimation)
        SceneManager.LoadScene(sceneName);
    }
}
