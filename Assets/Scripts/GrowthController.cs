﻿using System.Collections;
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
    void Awake()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        targetDrag = mRigidbody.drag;
        dragReductionPerSecond = startDrag - targetDrag;
    }

    public void StartGrowing()
    {
        mRigidbody.drag = startDrag;
    }

    public void Grow()
    {
        if (mRigidbody.drag > targetDrag)
            mRigidbody.drag -= dragReductionPerSecond * Time.deltaTime;
        mRigidbody.velocity = transform.up * growthSpeed;
        //Leave checkpoints as you grow?
    }

    public void FinishGrowing()
    {
        mRigidbody.drag = startDrag;
        foreach (var rigidbody in GetComponentsInChildren<Rigidbody2D>())
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0f;
        }
    }

    public void Retract(Vector3 position, Vector3 forward)
    {
        transform.position = position;
        transform.up = forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(mRigidbody.velocity != Vector2.zero)
        {
        //    mRigidbody.velocity = mRigidbody.velocity * transform.up;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        }
    }
}
