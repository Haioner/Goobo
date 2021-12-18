using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    public ParticleSystem laserEndParticles;
    Player_Controller playerController;
    public float lineLenght = 50;
    public LayerMask layerMask;
    public AudioSource source;

    LineRenderer line;
    bool endParticlesPlaying = false;
    private RaycastHit2D hit;
    bool doOnce = true;

    [Header("Disabler")]
    public bool canDisable = false;
    public float frequency;
    public float magnitude;
    bool isDisabled = false;
    float widhtValue = 1;
    public float cooldown = 5;
    public AudioSource laserSound;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        playerController = FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, transform.right, lineLenght, layerMask);
        if (hit)
        {
            if (!isDisabled)
            {
                if (endParticlesPlaying == false)
                {
                    endParticlesPlaying = true;
                    laserEndParticles.Play(true);
                }
            }
            else
            {
                if (endParticlesPlaying == true)
                {
                    endParticlesPlaying = false;
                    laserEndParticles.Stop(true);
                }

            }

            laserEndParticles.gameObject.transform.position = hit.point;
            float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
            //float dist = Vector2.Distance(hit.point, transform.position);
            line.SetPosition(1, new Vector3(distance, 0, 0));

            if (hit.collider.CompareTag("Player") && doOnce && !isDisabled)
            {
                doOnce = false;
                Scene _scene = SceneManager.GetActiveScene();
                string sceneName = _scene.name;
                Transition.Instance.StartTransition(sceneName);

                CameraShake.Instance.ShakeCamera(5f, 1f);
                source.Play();
                playerController.canMove = false;
                playerController.rb.velocity = Vector2.zero;
            }
        }
        else
        {
            line.SetPosition(1, new Vector3(lineLenght, 0, 0));
            endParticlesPlaying = false;
            laserEndParticles.Stop(true);
        }
        DisableLaser();
    }

    void DisableLaser()
    {
        if (canDisable)
        {
            cooldown -= 1 * Time.deltaTime;
            if (cooldown <= 0)
            {
                widhtValue = magnitude + Mathf.Sin(Time.timeSinceLevelLoad * frequency) * magnitude;
                line.startWidth = widhtValue;
                line.endWidth = widhtValue;
            }

            if (widhtValue <= 0.15f)
            {
                if (!isDisabled)
                {
                    isDisabled = true;
                    cooldown = 5;
                    laserSound.Stop();
                }
            }
            else
            {
                if (isDisabled)
                {
                    laserSound.Play();
                    isDisabled = false;
                }

            }

        }
    }
    IEnumerator widthLaserControl()
    {
        yield return new WaitForSeconds(2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * lineLenght);
    }
}
