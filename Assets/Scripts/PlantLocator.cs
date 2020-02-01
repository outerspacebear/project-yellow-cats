using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLocator : MonoBehaviour
{
    [SerializeField] public static Plant currentPlant = null;


    void Update()
    {
        if (currentPlant == null)
            return;

        if (Input.GetButtonDown("Grow"))
            currentPlant.StartGrowing();
        else if (Input.GetButton("Grow"))
            currentPlant.Grow();
        else if (Input.GetButtonUp("Grow"))
            currentPlant.FinishGrowing();
        else if (Input.GetButton("Retract"))
            currentPlant.Retract();
    }
}
