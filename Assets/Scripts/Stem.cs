using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour
{
    public Color c1 = new Color(0, 0.4f, 0);
    public Color c2 = new Color(0, 0.8f, 0);
    LineRenderer lineRenderer;

    public float baseWidth = 1.0f;
    public float headWidth = 0.1f;
    public int fullyGrownAtLength = 10;

    public int shakeLastSegments = 10;
    public float shakeStrength = 5.0f;

    public float currentShakeDirection = 0.0f;

    public List<Vector3> stemPositions;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 1.0f;
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 1.0f;
        lineRenderer.endWidth = 0.1f;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    public void SetStemPositions(List<Vector3> positions)
    {
        stemPositions = positions;
    }

    public void AddStepPosition(Vector3 position)
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
        if (lineRenderer.positionCount > 0)
            return lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        return transform.position;
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
