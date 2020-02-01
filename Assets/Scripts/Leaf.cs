using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float maxScale = 1.0f;

    public float growSpeed = 0.1f;

    public void Grow()
    {
        float currentScale = Mathf.Min(transform.localScale.x + growSpeed, maxScale);
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

}
