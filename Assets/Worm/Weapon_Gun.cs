using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon_Gun : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;

    public string teamColor;

    public Sprite icon;

    public float offset;

    private bool canFire;

    public bool isSG;
    public bool isFL;
    public bool isBB;

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + offset);

        if (canFire == true)
        {
            if (isSG == false && isFL == false && isBB == false)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                }
            }
            else if (isSG == true)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rot_z + offset + Random.Range(-7.0f, 7.0f)));
                    Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rot_z + offset + Random.Range(-7.0f, 7.0f)));
                    Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rot_z + offset + Random.Range(-7.0f, 7.0f)));
                    Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rot_z + offset + Random.Range(-7.0f, 7.0f)));
                    Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rot_z + offset + Random.Range(-7.0f, 7.0f)));
                }
            }
            else if (isFL == true)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                }
            }else if (isBB == true)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (Input.GetMouseButtonDown(0))
                {

                }
            }
        }

        
    }
}
