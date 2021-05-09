using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float launchForce;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyPojectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
