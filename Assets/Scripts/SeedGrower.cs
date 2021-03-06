﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SeedGrower : MonoBehaviour
{
    [SerializeField] Plant plant;
    [SerializeField] float spawnForce = 5f;
    [SerializeField] float spawnDelay = 1f;
    private float timeSinceSeedCollision = 0f;
    private bool readyToSpawn = false;
    private GameObject seed = null;
    [SerializeField] Collider2D[] moundColliders;
    [SerializeField] bool endGame = false;
    [SerializeField] GameObject animation;
    [SerializeField] Image endGameImage;

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

                var plantGuy = Instantiate(plant, new Vector3(seed.transform.position.x, seed.transform.position.y, seed.transform.position.z), transform.rotation) as Plant;
                plantGuy.GetComponentInChildren<Rigidbody2D>().AddForce(transform.up * spawnForce);
                PlantLocator.GetInstance().SwitchToPlant(plantGuy);
                if (endGame)
                {
                    foreach(var sprite in plantGuy.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sprite.enabled = false;
                    }
                    animation.GetComponent<Animator>().SetBool("play", true);
                    //endGameImage.enabled = true;
                    endGameImage.gameObject.GetComponent<ImageExpander>().enabled = true;
                }
                SeedCounter.IncrementSeedsPlanted();
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
            seed = collision.gameObject;
            seed.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            foreach (var collider in moundColliders)
            {
                collider.enabled = false;
            }
            readyToSpawn = true;
            GetComponent<AudioSource>().Play();
            GetComponentInParent<Animator>().SetBool("animateEnable", true);
        }
    }
}
