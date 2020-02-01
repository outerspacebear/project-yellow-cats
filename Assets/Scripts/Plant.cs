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

    int lastStemCount = 0;

    void Start()
	{
        stem = GetComponent<Stem>();
        growthController = GetComponentInChildren<GrowthController>();
        objectAimer2D = GetComponentInChildren<ObjectAimer2D>();

        lastHeadPosition = transform.position;
        lastStemCount = stem.stemPositions.Count;
    }


    void Update()
	{
        Vector3 newHeadPosition = head.transform.position;
        float distance = Vector3.Distance(newHeadPosition, lastHeadPosition);
        if (distance >= segmentLength)
        {
            lastHeadPosition = newHeadPosition;
            stem.AddStemPosition(newHeadPosition);
        }

        if (stem.stemPositions.Count / spawnLeafEverySegments > leaves.Count)
        {
            Leaf leaf = Instantiate(leafPrefab);
            leaf.name = "Leaf " + leaves.Count;
            leaf.transform.position = stem.GetHeadPosition();
            leaf.transform.parent = transform;
            leaf.flip = leaves.Count % 2 != 0;

            Vector3 growDirection = stem.GetGrowthDirection();
            Vector3 leafDirection = new Vector3(growDirection.y, -growDirection.x, growDirection.z);
            leaf.transform.right = leafDirection;

            leaves.Add(leaf);
        }

        if (stem.stemPositions.Count > lastStemCount)
        {
            foreach (var l in leaves)
            {
                l.Grow();
            }
            lastStemCount = stem.stemPositions.Count;
        }
    }
}
