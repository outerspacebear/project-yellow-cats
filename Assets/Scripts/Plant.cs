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
    public float growthThreashold = 0.1f;   // threshold for preventing growing on tiny movements.
                                            // E.g. when physics objects collide or
                                            // plant hits the wall

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


    public void StartGrowing()
    {
        growthController.StartGrowing();
    }

    public void Grow()
    {
        growthController.Grow();
    }

    public void FinishGrowing()
    {
        growthController.FinishGrowing();
    }

    public void Retract()
    {
        stem.RemoveStemPosition();
        growthController.Retract(stem.GetHeadPosition(), stem.GetGrowthDirection());
    }

    void Update()
	{
        Vector3 newHeadPosition = head.transform.position;
        if (Vector3.Distance(lastHeadPosition, newHeadPosition) > growthThreashold)
        {
            lastHeadPosition = newHeadPosition;
            stem.AddStemPosition(newHeadPosition);
        }

        int expectedLeavesCount = stem.stemPositions.Count / spawnLeafEverySegments;
        if (expectedLeavesCount > leaves.Count)
        {
            // There should be more leaves. Spawn a new one
            Leaf leaf = Instantiate(leafPrefab);
            leaf.name = "Leaf " + leaves.Count;
            leaf.transform.position = stem.GetHeadPosition();
            leaf.transform.parent = transform;
            leaf.flip = leaves.Count % 2 != 0;

            Vector3 growDirection = stem.GetGrowthDirection();
            Vector3 leafDirection = new Vector3(growDirection.y, -growDirection.x, growDirection.z);
            leaf.transform.right = leafDirection;
            leaf.Spawn(stem.stemPositions.Count);

            leaves.Add(leaf);
        }
        else if (expectedLeavesCount < leaves.Count && leaves.Count > 0)
        {
            // There is too many leaves. Remove the last one
            int last = leaves.Count - 1;
            Destroy(leaves[last]);
            leaves.RemoveAt(last);
        }

        foreach (var l in leaves)
        {
            l.Grow(stem.stemPositions.Count);
        }
    }
}
