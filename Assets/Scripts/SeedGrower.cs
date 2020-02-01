using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrower : MonoBehaviour
{
    [SerializeField] GameObject plant;
    [SerializeField] Vector2 spawnOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Seed"))
        {
            Destroy(collision.gameObject);
            Instantiate(plant, transform.position + new Vector3(spawnOffset.x, spawnOffset.y), transform.rotation);
        }
    }
}
