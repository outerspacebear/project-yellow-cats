using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityToggler : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] float startGravity = 0f;
    [SerializeField] float targetGravity = 1f;
    // Start is called before the first frame update

    public float delayForHint = 2.0f;

    void Start()
    {
        rigidbody.gravityScale = startGravity;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (Hint.GetInstance())
            {
                Hint.GetInstance().HideHints();
            }

            rigidbody.gravityScale = targetGravity;
            Destroy(this);
        }

        if (delayForHint > 0.0f)
        {
            delayForHint -= Time.deltaTime;
            if (delayForHint <= 0.0f && Hint.GetInstance())
            {
                Hint.GetInstance().ShowAnyButton();
            }
        }
    }
}
