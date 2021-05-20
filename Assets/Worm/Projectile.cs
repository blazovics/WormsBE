using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float damage;

    public bool isFL;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyPojectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);




        if (isFL == true)
        {
            transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
