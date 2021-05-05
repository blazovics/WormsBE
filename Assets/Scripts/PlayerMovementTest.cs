using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementTest : MonoBehaviour
{
    public float moveSpeed = 4f;

    public GameObject inventoryUI;

    public Rigidbody2D rb;

    Vector2 movement;

    public GameObject crosshair;

    public float crossHairDistance = 0.3f;

    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        

        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf)
            {
                Cursor.visible = true;
            }
            else { Cursor.visible = false; }
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Aim();


    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Aim()
    {
        var worldMousePosition =
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - rb.transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        // 4
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        SetCrosshairPosition(aimAngle);


    }

    private void SetCrosshairPosition(float aimAngle)
    {

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }
}
