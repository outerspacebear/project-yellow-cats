using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform followThis = null;
    [SerializeField] float leftMostPercentage = 0.05f;
    [SerializeField] float rightMostPercentage = 0.3f;
    [SerializeField] float followOffsetPercentage = -0.3f;
    [SerializeField] float movePerFrame = 0.5f;
    [SerializeField] float minXPos;
    [SerializeField] float maxXPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!followThis)
        {
            if (PlantLocator.GetInstance().currentPlant)
                followThis = PlantLocator.GetInstance().currentPlant.GetComponentInChildren<GrowthController>().gameObject.transform;
        }

        if(followThis)
        {
            if(Camera.main.WorldToViewportPoint(followThis.position).x >= rightMostPercentage || Camera.main.WorldToViewportPoint(followThis.position).x <= leftMostPercentage)
            {
                if (transform.position.x > followThis.position.x && transform.position.x <= minXPos)
                    return;
                else if (transform.position.x < followThis.position.x && transform.position.x >= maxXPos)
                    return;

                float newX = Mathf.Lerp(transform.position.x, followThis.position.x, movePerFrame);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }
    }
}
