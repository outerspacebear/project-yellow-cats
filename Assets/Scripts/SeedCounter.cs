using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCounter : MonoBehaviour
{
    [SerializeField] AudioSource two = null;
    [SerializeField] AudioSource three = null;
    [SerializeField] AudioSource four = null;
    [SerializeField] AudioSource five = null;
    static bool updatedCount = false;
    static int seedsPlanted = 0;

    public static int GetSeedsPlanted()
    { return seedsPlanted; }

    public static void IncrementSeedsPlanted()
    {
        seedsPlanted += 1;
        updatedCount = true;
    }

    private void Update()
    {
        if(updatedCount)
        {
            updatedCount = false;
            switch (seedsPlanted)
            {
                case 2:
                    two.Play();
                    return;
                case 3:
                    three.Play();
                    return;
                case 4:
                    four.Play();
                    return;
                case 5:
                    five.Play();
                    return;
            }
        }
    }
}
