using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class TriggerToBoss : MonoBehaviour
{

    public static int state = 0;
    public AudioSource music;
    public Player_Controller playerController;
    public PlayableDirector playableDirector;
    public CinemachineTargetGroup cinemachineTargetGroup;
    float weightTo;
    bool canAdd = false;
    public Transform monster;
    public GameObject rocks;
    public GameObject musicObj;
    public Monster monsterScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && state == 0)
        {
            state = 1;
            playerController.canMove = false;
            playerController.rb.velocity = Vector2.zero;
            playerController.anim.SetBool("Walk", false);
            playableDirector.enabled = true;

            monster.gameObject.SetActive(true);
            cinemachineTargetGroup.AddMember(monster, weightTo ,1);
            canAdd = true;
     
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(musicObj.gameObject);
        ChangeToBoss();
    }

    void ChangeToBoss()
    {
        if(state == 1 || Manager.Instance.checkpoint > 0)
        {
            monster.gameObject.SetActive(true);
            if (!music.isPlaying)
                music.Play();
        }
    }

    private void Update()
    {
        if (canAdd)
        {
            if (weightTo < 1)
            {
                weightTo += 1 * Time.deltaTime;
            }
            int targetIndex = cinemachineTargetGroup.FindMember(monster);
            cinemachineTargetGroup.m_Targets[targetIndex].weight = weightTo;
        }

        if(playableDirector.state != PlayState.Playing)
        {
            monsterScript.enabled = true;
            monster.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            monsterScript.enabled = false;
            monster.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void ShakeTheCamera()
    {
        CameraShake.Instance.ShakeCamera(5f, 1f);
        if (rocks != null)
            rocks.SetActive(false);
        Invoke("BackThePlayer", 1.5f);
        if (!music.isPlaying)
            music.Play();
        canAdd = false;
        
    }

    void BackThePlayer()
    {
        cinemachineTargetGroup.RemoveMember(monster);
        playerController.canMove = true;
    }
}
