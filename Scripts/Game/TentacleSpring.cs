using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSpring : MonoBehaviour
{
    public int lenght;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;

    public float smoothSpeed;

    public bool canWiggle;
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    void Start()
    {
        lineRend.positionCount = lenght;
        segmentPoses = new Vector3[lenght];
        segmentV = new Vector3[lenght];
    }

    void Update()
    {
        if (canWiggle)
        {
            wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        }

        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDist;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
        }
        lineRend.SetPositions(segmentPoses);

    }
}
