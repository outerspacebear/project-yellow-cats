using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityToggler : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] float startGravity = 0f;
    [SerializeField] float targetGravity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.gravityScale = startGravity;

        if (Hint.GetInstance())
        {
            Hint.GetInstance().ShowAnyButton();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            rigidbody.gravityScale = targetGravity;
            Destroy(this);
        }
    }
}
