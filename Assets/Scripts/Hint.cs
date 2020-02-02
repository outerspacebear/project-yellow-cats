using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public GameObject mouseIcon;
    public GameObject lmb;
    public GameObject rmb;

    public float tutorialLength = 5.0f;

    public float blinkSpeed = 0.5f;

    public float tutorialLeftTime = 0.0f;
    public float blinkLeftTime = 0.0f;

    public List<GameObject> animatedObjects;

    public bool lmbShownAlready = false;
    public bool rmbShownAlready = false;
    public bool anyButtonShownAlready = false;

    static Hint instance;

    public static Hint GetInstance()
    {
        return instance;
    }

    public void ShowAnyButton()
    {
        if (anyButtonShownAlready)
            return;

        anyButtonShownAlready = true;

        mouseIcon.SetActive(true);
        lmb.SetActive(false);
        rmb.SetActive(false);

        animatedObjects.Clear();
        animatedObjects.Add(lmb);
        animatedObjects.Add(rmb);

        blinkLeftTime = blinkSpeed;
        tutorialLeftTime = tutorialLength;
    }

    public void ShowLeftButton()
    {
        if (lmbShownAlready)
            return;

        lmbShownAlready = true;

        mouseIcon.SetActive(true);
        lmb.SetActive(false);
        rmb.SetActive(false);

        animatedObjects.Clear();
        animatedObjects.Add(lmb);

        blinkLeftTime = blinkSpeed;
        tutorialLeftTime = tutorialLength;
    }

    public void ShowRightButton()
    {
        if (rmbShownAlready)
            return;

        rmbShownAlready = true;

        mouseIcon.SetActive(true);
        lmb.SetActive(false);
        rmb.SetActive(false);

        animatedObjects.Clear();
        animatedObjects.Add(rmb);

        blinkLeftTime = blinkSpeed;
        tutorialLeftTime = tutorialLength;
    }

    public void Awake()
    {
        instance = this;
        mouseIcon.SetActive(false);
        lmb.SetActive(false);
        rmb.SetActive(false);
    }

    private void Update()
    {
        if (tutorialLeftTime > 0.0f)
        {
            if (blinkLeftTime > 0.0f)
            {
                blinkLeftTime -= Time.deltaTime;

                if (blinkLeftTime <= 0.0f)
                {
                    blinkLeftTime = blinkSpeed;
                    foreach (var animatedObject in animatedObjects)
                    {
                        animatedObject.SetActive(!animatedObject.activeSelf);
                    }
                    mouseIcon.SetActive(!mouseIcon.activeSelf);
                }
            }

            tutorialLeftTime -= Time.deltaTime;
            if (tutorialLeftTime <= 0.0f)
            {
                tutorialLeftTime = 0.0f;

                mouseIcon.SetActive(false);
                lmb.SetActive(false);
                rmb.SetActive(false);
            }
        }
    }

}
