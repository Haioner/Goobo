using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class BossTrigger_Final : MonoBehaviour
{
    public static int state2 = 0;
    public PlayableDirector director;
    public Monster monster;
    public AudioSource music;
    public Player_Controller playerController;
    public CinemachineTargetGroup cinemachineTargetGroup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && state2 == 0)
        {
            state2 = 1;
            director.gameObject.SetActive(true);
            monster.gameObject.SetActive(true);

            playerController.canMove = false;
            playerController.rb.velocity = Vector2.zero;
            playerController.anim.SetBool("Walk", false);
            cinemachineTargetGroup.AddMember(monster.transform, 1, 1);
        }
    }

    private void Start()
    {
        ChangeToBoss();
    }

    void ChangeToBoss()
    {
        if (state2 == 1 || Manager.Instance.checkpoint > 4)
        {
            monster.gameObject.SetActive(true);
            if (!music.isPlaying)
                music.Play();
        }
    }

    private void Update()
    {
        if (director.state != PlayState.Playing)
        {
            monster.enabled = true;
            monster.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            monster.enabled = false;
            monster.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void BackThePlayer()
    {
        playerController.canMove = true;
        Invoke("BackThePlayer", 1.5f);
        if (!music.isPlaying)
            music.Play();

    }

}
