using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public float damage;

    public PlayerMovement[] worms;

    void Start()
    {
        worms = GameObject.FindObjectsOfType<PlayerMovement>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        //Debug.Log("Projectile Collision with " + other.gameObject);
        if (other.gameObject.name == "WormBlue1")
        {
            foreach (PlayerMovement w in worms)
            {
                if (w.worm.name == "WormBlue1")
                {
                    w.TakeDamage(damage);
                }
            }
        }
        if (other.gameObject.name == "WormBlue2")
        {
            foreach (PlayerMovement w in worms)
            {
                if (w.worm.name == "WormBlue2")
                {
                    w.TakeDamage(damage);
                }
            }
        }
        if (other.gameObject.name == "WormYellow1")
        {
            foreach (PlayerMovement w in worms)
            {
                if (w.worm.name == "WormYellow1")
                {
                    w.TakeDamage(damage);
                }
            }
        }
        if (other.gameObject.name == "WormYellow2")
        {
            foreach (PlayerMovement w in worms)
            {
                if (w.worm.name == "WormYellow2")
                {
                    w.TakeDamage(damage);
                }
            }
        }

    }
}
