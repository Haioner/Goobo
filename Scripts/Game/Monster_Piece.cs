using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster_Piece : MonoBehaviour
{
    public Transform player;
    public Player_Controller playerController;
    public Animator anim;
    public GameObject[] faces;
    public float lerpSpeed = 3;
    public Vector2 clamp;
    public bool canClamp = true;
    int chooseFace;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
            playerController = player.GetComponent<Player_Controller>();
        }

        FaceChoose();
    }

    private void Update()
    {
        LookAtBone();
    }

    void LookAtBone()
    {
        if (canClamp)
        {
            Vector3 difference = player.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.z = Mathf.Lerp(currentRotation.z, rotationZ, Time.deltaTime * lerpSpeed);
            transform.eulerAngles = currentRotation;


            Vector3 euler = transform.eulerAngles;
            if (euler.z > 180) euler.z = euler.z - 360;
            euler.z = Mathf.Clamp(euler.z, clamp.x, clamp.y);
            transform.eulerAngles = euler;
        }
        else
        {
            Vector3 difference = player.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }

    void FaceChoose()
    {
        chooseFace = Random.Range(0, faces.Length);
        faces[chooseFace].SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.Play("Kill");
            playerController.canMove = false;
        }
    }

}
