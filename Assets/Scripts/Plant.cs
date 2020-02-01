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

    public Leaf leafPrefab;
    public List<Leaf> leaves = new List<Leaf>();
    public int spawnLeafEverySegments = 10;


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
            stem.AddStemPosition(newHeadPosition);
        }

        if (stem.stemPositions.Count / spawnLeafEverySegments > leaves.Count)
        {
            foreach(Leaf leaf in leaves)
            {
                leaf.Grow();
            }

            Leaf newLeaf = Instantiate(leafPrefab);
            newLeaf.transform.position = stem.GetHeadPosition();
            newLeaf.transform.parent = transform;

            Vector3 growDirection = stem.GetGrowthDirection();
            newLeaf.transform.rotation = Quaternion.Euler(growDirection.y, growDirection.x, growDirection.z);

            leaves.Add(newLeaf);
        }
    }
}
