using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public Transform spawnPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Seed"))
        {
            collision.gameObject.transform.position = spawnPosition.position;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        }
    }
}
