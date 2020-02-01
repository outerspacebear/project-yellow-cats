using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GrowthController : MonoBehaviour
{
    private Rigidbody2D mRigidbody;
    [SerializeField] float growthSpeed = 0.2f;
    [SerializeField] float extraDragTime = 1.0f;

    [SerializeField] float startDrag = 5f;
    private float targetDrag;
    private float dragReductionPerSecond;

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        targetDrag = mRigidbody.drag;
        dragReductionPerSecond = startDrag - targetDrag;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Grow"))
        {
            mRigidbody.drag = startDrag;
        }
        else if (Input.GetButton("Grow"))
        {
            if(mRigidbody.drag > targetDrag)
                mRigidbody.drag -= dragReductionPerSecond * Time.deltaTime;
            mRigidbody.velocity = transform.up * growthSpeed;
            //Leave checkpoints as you grow?
        }
        else if(Input.GetButtonUp("Grow"))
        {
            mRigidbody.drag = startDrag;
            foreach(var rigidbody in GetComponentsInChildren<Rigidbody2D>())
            {
                rigidbody.velocity = Vector2.zero;
                rigidbody.angularVelocity = 0f;
            }
        }

        else if(Input.GetButton("Retract"))
        {
            //retract, not just move backwards
            //maybe just turn back time - how?
        }
        else if(mRigidbody.velocity != Vector2.zero)
        {
            mRigidbody.velocity = mRigidbody.velocity * transform.up;
        }
    }
}
