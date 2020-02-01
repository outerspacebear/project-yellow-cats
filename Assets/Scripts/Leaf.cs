using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float maxScale = 1.0f;

    public float growSpeed = 0.01f;
    public bool flip = false;

    public int spawnedAtStemLength = 0;

    public void Spawn(int stemLength)
    {
        spawnedAtStemLength = stemLength;
    }

    public void Grow(int stemLength)
    {
        int age = Mathf.Max(stemLength - spawnedAtStemLength, 0);

        float currentScale = Mathf.Min(age * growSpeed, maxScale);

        transform.localScale = new Vector3(currentScale * (flip ? -1 : 1), currentScale, currentScale);
    }

}
