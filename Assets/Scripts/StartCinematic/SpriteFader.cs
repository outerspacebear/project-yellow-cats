using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFader : MonoBehaviour
{
    [SerializeField] float startAlpha = 1f;
    [SerializeField] float targetAlpha = 0f;
    [SerializeField] float fadeSpeed = 0.02f;
    private SpriteRenderer text;
    private bool shouldFade = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<SpriteRenderer>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, startAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            shouldFade = true;

        if (shouldFade)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - fadeSpeed);
            if (Mathf.Abs(text.color.a - targetAlpha) < 0.1)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, targetAlpha);
                Destroy(this);
            }
        }
    }
}
