using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ProgressSpriteFader : MonoBehaviour
{
    [SerializeField] int totalSeeds;
    [SerializeField] float startAlpha = 1f;
    [SerializeField] float targetAlpha = 0f;
    [SerializeField] float lerpBy = 0.014f;
    private int seedCount = 0;
    private float step;
    private float currentTarget;
    private bool isFading = false;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        step = (float)(Mathf.Abs(startAlpha - targetAlpha)) / (float)totalSeeds;
    }

    // Update is called once per frame
    void Update()
    {
        if(SeedCounter.GetSeedsPlanted() > seedCount)
        {
            isFading = true;
            seedCount = SeedCounter.GetSeedsPlanted();
            //sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, startAlpha - step * seedCount);
            currentTarget = startAlpha - step * seedCount;
        }
        if(isFading)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(sprite.color.a, currentTarget, lerpBy));
            if(Mathf.Abs(sprite.color.a - currentTarget) < lerpBy * 3)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentTarget);
                isFading = false;
            }
        }
    }
}
