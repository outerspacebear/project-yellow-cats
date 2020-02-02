using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
public class Attractor : MonoBehaviour
{
    [SerializeField] float attractionStopAngle = 10f;
    [SerializeField] Plant targetObject;
    [SerializeField] string triggerObjectTag = "Player";
    [SerializeField] string groundTag = "Ground";
    private SpringJoint2D spring;
    [SerializeField] private bool readyForAttraction = false;
    private float timeSinceCollision = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spring = GetComponent<SpringJoint2D>();
        targetObject = PlantLocator.GetInstance().currentPlant;
    }

    // Update is called once per frame
    void Update()
    {
        if (spring.enabled)
        {
            if(Vector2.Angle(spring.connectedBody.transform.up, Vector2.up) < attractionStopAngle)
            {
                spring.enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        targetObject = PlantLocator.GetInstance().currentPlant;
        if (collision.gameObject.CompareTag(groundTag))
        {
            readyForAttraction = true;
        }
        else if(collision.gameObject.CompareTag(triggerObjectTag) && readyForAttraction && Vector2.Angle(targetObject.transform.up, Vector2.up) >= attractionStopAngle)
        {
            spring.connectedBody = targetObject.GetComponent<Rigidbody2D>();
            spring.enabled = true;
            readyForAttraction = false;
        }
    }
}
