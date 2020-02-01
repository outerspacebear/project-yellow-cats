using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledPlant : MonoBehaviour
{
    public Vector3 lastSegmentPosition = Vector2.negativeInfinity;
    public float segmentLength = 1.0f;

    public Stem mainStem;

    private void Start()
    {
        mainStem = GetComponent<Stem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastSegmentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lastSegmentPosition.z = 0;

            var stemPositions = new List<Vector3>();
            stemPositions.Add(lastSegmentPosition);

            mainStem.SetStemPositions(stemPositions);
        }
        else if (Input.GetMouseButton(0))
        {
            var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;

            var delta = newPosition - lastSegmentPosition;

            if (delta.magnitude > segmentLength)
            {
                lastSegmentPosition = newPosition;
                mainStem.AddStemPosition(newPosition);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            mainStem.RemoveStemPosition();
        }
    }


}
