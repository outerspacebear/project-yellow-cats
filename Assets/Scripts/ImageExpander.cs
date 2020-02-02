using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ImageExpander : MonoBehaviour
{
    [SerializeField] float targetScale;
    [SerializeField] float speed;
    private RectTransform image;
    private Vector3 targetVector;
    [SerializeField] GameObject activatWhenDying;
    [SerializeField] float delay = 6f;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RectTransform>();
        targetVector = new Vector3(targetScale, targetScale, image.localScale.z);
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > delay)
        {
            GetComponent<Image>().enabled = true;
            image.localScale = Vector3.Lerp(image.localScale, targetVector, speed);
            if (Vector3.Distance(image.localScale, targetVector) < 60)
            {
                activatWhenDying.SetActive(true);
                Destroy(this);
            }
        }
    }
}
