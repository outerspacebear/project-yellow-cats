using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrower : MonoBehaviour
{
    [SerializeField] Plant plant;
    [SerializeField] float spawnForce = 5f;
    [SerializeField] float spawnDelay = 1f;
    private float timeSinceSeedCollision = 0f;
    private bool readyToSpawn = false;
    private GameObject seed = null;
    [SerializeField] Collider2D[] moundColliders;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToSpawn)
        {
            timeSinceSeedCollision += Time.deltaTime;
            if(timeSinceSeedCollision >= spawnDelay)
            {
                readyToSpawn = false;
                var plantGuy = Instantiate(plant, seed.transform.position, transform.rotation) as Plant;
                plantGuy.GetComponentInChildren<Rigidbody2D>().AddForce(transform.up * spawnForce);
                PlantLocator.currentPlant = plantGuy;
                Destroy(seed);
                //foreach (var collider in moundColliders)
                //{
                //    collider.enabled = true;
                //}
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Seed"))
        {
            foreach(var collider in moundColliders)
            {
                collider.enabled = false;
            }
            seed = collision.gameObject;
            readyToSpawn = true;
        }
    }
}
