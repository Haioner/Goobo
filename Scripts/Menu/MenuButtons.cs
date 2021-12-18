using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Button continueButton;

    private void Start()
    {
        if(Manager.Instance._scene < 1)
        {
            continueButton.interactable = false;
        }
    }

    public void PlayButton()
    {
        FindObjectOfType<Transition>().StartTransition("Scene0");
    }

    public void ContinueButton()
    {

        SceneManager.LoadScene("Scene" + Manager.Instance._scene);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
