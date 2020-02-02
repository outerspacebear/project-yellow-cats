using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLocator : MonoBehaviour
{
    [SerializeField] public Plant currentPlant = null;

    public float autoGrowthLength = 0.5f;
    public float growthRemaining = 0.0f;


    static PlantLocator instance;

    public static PlantLocator GetInstance()
    {
        return instance;
    }

    public void SwitchToPlant(Plant nextPlant)
    {
        growthRemaining = autoGrowthLength;
        currentPlant = nextPlant;
        currentPlant.StartGrowing();
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (currentPlant == null)
            return;

        if (growthRemaining > 0.0f)
        {
            growthRemaining -= Time.deltaTime;
            if (growthRemaining > 0.0f)
                currentPlant.Grow(false);
            else
            {
                growthRemaining = 0.0f;
                currentPlant.FinishGrowing();

                if (Hint.GetInstance())
                {
                    Hint.GetInstance().ShowLeftButton();
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Grow"))
                currentPlant.StartGrowing();
            else if (Input.GetButton("Grow"))
                currentPlant.Grow(true);
            else if (Input.GetButtonUp("Grow"))
                currentPlant.FinishGrowing();
            else if (Input.GetButton("Retract"))
                currentPlant.Retract();
        }
    }
}
