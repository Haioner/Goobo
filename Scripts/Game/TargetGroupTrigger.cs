using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetGroupTrigger : MonoBehaviour
{
    public Transform posToLookAt;
    public float _weight;
    public float _radius;
    public float speedTransition;
    float weightTo;
    CinemachineTargetGroup targetGroup;
    bool canAdd;

    private void Awake()
    {
        targetGroup = FindObjectOfType<CinemachineTargetGroup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAdd = true;
            targetGroup.AddMember(posToLookAt, weightTo, _radius);
        }
    }
    
    private void Update()
    {
        if (canAdd)
        {
            if (weightTo < _weight)
            {
                weightTo += speedTransition * Time.deltaTime;
            }

            int targetIndex = targetGroup.FindMember(posToLookAt);
            targetGroup.m_Targets[targetIndex].weight = weightTo;
        }
        else if(targetGroup.FindMember(posToLookAt) != -1)
        {
            if (weightTo > -0.01f)
            {
                weightTo -= speedTransition * 2 * Time.deltaTime;
                int targetIndex = targetGroup.FindMember(posToLookAt);
                targetGroup.m_Targets[targetIndex].weight = weightTo;
            }
            else
            {
                targetGroup.RemoveMember(posToLookAt);
            }
        }
    }
    
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAdd = false;
           
        }
    }
}
