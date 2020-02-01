using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float maxScale = 1.0f;

    public float growSpeed = 0.01f;
    public bool flip = false;

    float currentScale = 1.0f;
    
    private void Awake()
    {
        currentScale = transform.localScale.x;
    }

    public void Grow()
    {
        currentScale = Mathf.Min(currentScale + growSpeed, maxScale);
        transform.localScale = new Vector3(currentScale * (flip ? -1 : 1), currentScale, currentScale);
    }

}
