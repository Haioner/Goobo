using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DoorCutscene : MonoBehaviour
{
    public GameObject _director;
    public Player_Controller player;
    public Animator playerAnimator;
    public AudioSource music;
    bool canDisableMusic = false;
    public Transform particlePos;
    public GameObject particleDust;
    public int IDscene = 4;

    private void Update()
    {
        if (canDisableMusic && music.volume > 0)
        {
            music.volume -= 0.1f * Time.deltaTime;
        }
        if (music.volume <= 0)
            music.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayAnimation();
    }

    void PlayAnimation()
    {
        playerAnimator.SetBool("Walk", false);
        player.canMove = false;
        canDisableMusic = true;
        _director.SetActive(true);
    }

    public void ShakeCamera()
    {
        CameraShake.Instance.ShakeCamera(5f, 1f);
        Instantiate(particleDust, particlePos.position, Quaternion.identity);
    }

    public void ChangeScene()
    {
        Manager.Instance.SaveInt("_scene", IDscene);
        Manager.Instance.LoadScene();

        Manager.Instance.SaveInt("checkpoint", 0);
        Manager.Instance.LoadCheckpoint();

        Scene scene = SceneManager.GetActiveScene();
        string _sceneName = scene.name;
        Transition.Instance.StartTransition("Scene"+IDscene);
    }
}
