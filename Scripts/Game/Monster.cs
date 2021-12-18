using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Transform player;
    public float distance;
    public float speedTransition;
    bool doOnce = true;
    bool canAdd = false;
    bool facingRight = false;
    float weightTo;
    [Header("Move")]
    public float speed = 5;
    public Vector2 distanceFromPlayer;
    float initialYDistance;
    public LayerMask ground;
    public float radius;
    bool hitGround;

    [Header("Attack")]
    public bool canAttack = false;
    bool isAttacking = false;
    public float attackForce = 2;
    public float cooldown = 5;
    float initialCooldown;
    public AudioSource source;
    public GameObject dustParticle;
    public Transform dustPos;

    private void Awake()
    {
        initialCooldown = cooldown;
        initialYDistance = distanceFromPlayer.y;
    }

    void Update()
    {
        AttackThePlayer();
        MoveToPlayer();
        NearPlayer();
        Flip();
    }

    void AttackThePlayer()
    {
        //Cooldown to attack
        if(cooldown <= 0)
        {
            canAttack = true;
            cooldown = initialCooldown;
        }
        else
        {
            cooldown -= 1 * Time.deltaTime;
        }

        //If can attack > do the attack
        if (canAttack)
        {
            isAttacking = true;
            canAttack = false;
            Vector3 dir = player.position - transform.position;
            dir = dir.normalized;
            StartCoroutine("attack", dir);
        }

        //Desacelerate
        if (rb.velocity.x > 0)
        {
            Vector3 vel = rb.velocity;
            vel.x -= 1 * Time.deltaTime;
            vel.y -= 1 * Time.deltaTime;
            vel.z -= 1 * Time.deltaTime;
            rb.velocity = new Vector3(vel.x, vel.y, vel.z);
        }
    }

    IEnumerator attack(Vector3 targetPos)
    {
        yield return new WaitForSeconds(1);
        rb.AddForce((player.position - transform.position) * attackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
        isAttacking = false;
    }

    void MoveToPlayer()
    {
        if (!isAttacking)
        {
            //bool hitGround = Physics2D.OverlapCircle(transform.position, radius, ground);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, ground);
            Vector3 pos = player.position;
            pos.x = player.position.x + distanceFromPlayer.x;
            if (hit.distance < 10)
                distanceFromPlayer.y -= 1;
            else if(hit.distance > 25)
                distanceFromPlayer.y = initialYDistance;
            pos.y = player.position.y - distanceFromPlayer.y;

            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);

        }
    }

    void Flip()
    {
        if (!isAttacking)
        {
            this.spriteRenderer.flipY = player.transform.position.x < this.transform.position.x;

            Vector3 difference = player.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }

    public void NearPlayer()
    {
        float dist =Vector2.Distance(player.position, transform.position);
        if(dist < distance)
        {
            if (doOnce)
            {
                doOnce = false;
                canAdd = true;
                targetGroup.AddMember(this.gameObject.transform, weightTo, 1);
            }
        }
        else
        {
            doOnce = true;
            canAdd = false;
        }

        if (canAdd)
        {
            if (weightTo < 1)
            {
                weightTo += speedTransition * Time.deltaTime;
            }
            int targetIndex = targetGroup.FindMember(this.gameObject.transform);
            targetGroup.m_Targets[targetIndex].weight = weightTo;
        }
        
        else if (targetGroup.FindMember(this.gameObject.transform) != -1)
        {
            if (weightTo > -0.01f)
            {
                weightTo -= speedTransition * Time.deltaTime;
                int targetIndex = targetGroup.FindMember(this.gameObject.transform);
                targetGroup.m_Targets[targetIndex].weight = weightTo;
            }
            else
            {
                targetGroup.RemoveMember(this.gameObject.transform);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject && isAttacking)
        {
            Instantiate(dustParticle, dustPos.position, Quaternion.identity);
            CameraShake.Instance.ShakeCamera(5f, 1f);
            source.Play();
        }

        if (collision.gameObject.CompareTag("Player") && isAttacking)
        {
            Scene _scene = SceneManager.GetActiveScene();
            string sceneName = _scene.name;
            Transition.Instance.StartTransition(sceneName);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position,radius);
        Gizmos.DrawRay(transform.position, transform.right * radius);
    }
}
