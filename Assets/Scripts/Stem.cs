﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float baseWidth = 1.0f;
    public float headWidth = 0.1f;
    public int fullyGrownAtLength = 10;

    public int shakeLastSegments = 10;
    public float shakeStrength = 5.0f;

    public float currentShakeDirection = 0.0f;

    public List<Vector3> stemPositions = new List<Vector3>();

    void Awake()
    {
        lineRenderer.startWidth = baseWidth;
        lineRenderer.endWidth = headWidth;
    }

    public void SetStemPositions(List<Vector3> positions)
    {
        stemPositions = positions;
    }

    public void AddStemPosition(Vector3 position)
    {
        stemPositions.Add(position);
    }

    public void RemoveStemPosition()
    {
        if (stemPositions.Count > 0)
        {
            stemPositions.RemoveAt(stemPositions.Count - 1);
        }
    }

    public Vector3 GetHeadPosition()
    {
        if (stemPositions.Count > 0)
        {
            Vector3 headPosition = stemPositions[stemPositions.Count - 1];
            return headPosition;
        }

        return transform.position;
    }

    public int Length()
    {
        return stemPositions.Count;
    }

    public Vector3 GetGrowthDirection()
    {
        if (stemPositions.Count > 1)
        {
            return stemPositions[stemPositions.Count - 1] - stemPositions[stemPositions.Count - 2];
        }
        return Vector3.zero;
    }

    void Update()
    {
        // Start from the second segment so we don't shake the base
        int shakeStartsFrom = Mathf.Max(stemPositions.Count - shakeLastSegments, 1);

        float widthDifference = baseWidth - headWidth;
        lineRenderer.startWidth = headWidth + widthDifference * Mathf.Min((float)stemPositions.Count / fullyGrownAtLength, 1.0f);
        lineRenderer.endWidth = headWidth;

        lineRenderer.positionCount = stemPositions.Count;
        float localShakeStrength = 0;
        for (int i = 0; i < stemPositions.Count; ++i)
        {
            if (i < shakeStartsFrom)
            {
                lineRenderer.SetPosition(i, stemPositions[i]);
            }
            else
            {
                var growthDirection = (stemPositions[i] - stemPositions[i - 1]);
                var shakeDirection = new Vector3(growthDirection.y, -growthDirection.x, growthDirection.z).normalized;

                localShakeStrength += currentShakeDirection * shakeStrength;
                var localShake = shakeDirection * localShakeStrength;

                lineRenderer.SetPosition(i, lineRenderer.GetPosition(i - 1) + growthDirection + localShake);

                Debug.DrawLine(lineRenderer.GetPosition(i-1), lineRenderer.GetPosition(i), Color.red);
                Debug.DrawLine(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i) + localShake, Color.blue);
            }
        }
    }
}
