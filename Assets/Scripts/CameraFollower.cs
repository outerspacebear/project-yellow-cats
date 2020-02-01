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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!followThis)
        {
            if (PlantLocator.currentPlant)
                followThis = PlantLocator.currentPlant.GetComponentInChildren<GrowthController>().gameObject.transform;
        }

        if(followThis)
        {
            //if(Camera.main.WorldToScreenPoint(followThis.position).x <= leftEdge)
            if(Camera.main.WorldToViewportPoint(followThis.position).x <= leftMostPercentage)  
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, followThis.position.x, movePerFrame), transform.position.y, transform.position.z);
            }
            else if(Camera.main.WorldToViewportPoint(followThis.position).x >= rightMostPercentage)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, followThis.position.x, movePerFrame), transform.position.y, transform.position.z);
            }
        }
    }
}
