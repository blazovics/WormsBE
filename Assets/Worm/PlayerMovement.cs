using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //public Rigidbody2D bulletPrefab;
    public Transform currentGun;
    public CharacterController2D controller;
    public Animator animator;

    public bool IsTurn { get { return RoundManager.singleton.IsMyTurn(wormId); } }

    public int wormId;

    public GameObject worm;

    public string teamColor;

    public GameObject gun;

    public float health;
    public float maxHealth = 100;
    public Text playerHealth;

    public Rigidbody2D rb;

    public GameObject inventoryUI;

    public GameObject crosshair;

    public float crossHairDistance = 0.1f;

    public float runSpeed = 0.8f;
    public float misileForce = 5;

    float horizontalMove = 0f;
    bool jump = false;
    bool movingDone = false;
    bool canMove = true;

    public float targetTime = 60.0f;


    void Awake()
    {
        Cursor.visible = false;
        
    }

    // Start is called before the first frame update
    void Start()
    {

        
        health = maxHealth;
        if (teamColor == "blue")
        {            
            playerHealth.color = Color.blue;
        }
        else if (teamColor == "yellow")
        {
            playerHealth.color = Color.yellow;
        }
        playerHealth.text = health.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTurn)
        {
            horizontalMove = 0f;
            runSpeed = 0;
            animator.SetBool("IsTime", true);
            animator.SetFloat("Speed", 0);
            if (gun != null) 
            {
                gun.SetActive(false);
                gun = null;
            }
            

            crosshair.SetActive(false);
          return; 
        }


        runSpeed = 0.8f;
        animator.SetBool("IsTime", false);

        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf)
            {
                crosshair.SetActive(false);
                Cursor.visible = true;
            }
            else { Cursor.visible = false; crosshair.SetActive(true); }
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
  
                
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            canMove = false;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (movingDone) 
        {
            RotateGun();
            Aim();
            
        }

        if (horizontalMove == 0)
        {
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsTime", true);

            if (inventoryUI.activeSelf)
            {
                crosshair.SetActive(false);
                
            }
            else { crosshair.SetActive(true); }
            currentGun.gameObject.SetActive(true);
            /*if (Input.GetMouseButtonDown(0))
            {
             
                    Cursor.visible = false;
                    inventoryUI.SetActive(false);
                    Timer.instance.secondsLeft = 0;

                
            }*/



        }
        else
        {
            
            crosshair.SetActive(false);
            currentGun.gameObject.SetActive(false);
            transform.position += Vector3.right * horizontalMove *Time.deltaTime * runSpeed;
        }



    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        //Move character
        if (canMove)
        {
            
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
            movingDone = true;
        }
    }

    void RotateGun()
    {
        var diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        currentGun.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
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
        
        var x = transform.position.x + crossHairDistance * Mathf.Cos(aimAngle);
        var y = transform.position.y + crossHairDistance * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }


    public void TakeDamage(float damage)
    {

        health -= damage;
        RoundManager.singleton.lerpTimer = 0f;
        if (health <= 0) 
        {
            health = 0;
            worm.SetActive(false);
            Timer.instance.secondsLeft = 0;
        }
        playerHealth.text = health.ToString();


    }
    /*
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        RoundManager.singleton.lerpTimer = 0f;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        playerHealth.text = health.ToString();
    }*/

    
}
