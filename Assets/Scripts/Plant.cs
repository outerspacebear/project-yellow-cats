using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Stem stem;
    public GameObject head;

    public GrowthController growthController;
    public ObjectAimer2D objectAimer2D;

    public Vector3 lastHeadPosition;
    public float segmentLength = 1.0f;

    void Start()
	{
        stem = GetComponent<Stem>();
        growthController = GetComponentInChildren<GrowthController>();
        objectAimer2D = GetComponentInChildren<ObjectAimer2D>();

        lastHeadPosition = transform.position;
    }


    void Update()
	{
        Vector3 newHeadPosition = head.transform.position;
        float distance = Vector3.Distance(newHeadPosition, lastHeadPosition);
        Debug.Log("Distance = " + distance + " lastHeadPosition: " + lastHeadPosition + " newHeadPosition: " + newHeadPosition);
        if (distance >= segmentLength)
        {
            lastHeadPosition = newHeadPosition;
            stem.AddStepPosition(newHeadPosition);
        }
    }
}
