using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Piece_Kill : MonoBehaviour
{

    public void KillPlayerAlert()
    {
        Scene _scene = SceneManager.GetActiveScene();
        string sceneName = _scene.name;
        Transition.Instance.StartTransition(sceneName);
    }
}
