using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;
    public Transform currentGun;
    public CharacterController2D controller;
    public Animator animator;


    private float health;
    public float maxHealth = 100;
    private float lerpTimer;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;
    public Text playerHealth;

    public Rigidbody2D rb;

    public GameObject inventoryUI;

    public GameObject crosshair;

    public float crossHairDistance = 0.1f;

    public float runSpeed = 40f;
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
        frontHealthBar.color = Color.blue;
        playerHealth.color = Color.blue;
    }

    // Update is called once per frame
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


        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RestoreHealth(Random.Range(5, 10));
        }


        /*if (!IsTurn)
            return;*/
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
            currentGun.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                var p = Instantiate(bulletPrefab,
                                   currentGun.position - currentGun.right,
                                   currentGun.rotation);

                p.AddForce(-currentGun.right * misileForce, ForceMode2D.Impulse);

                //if (IsTurn) WormyManager.singleton.NextWorm();
            }
        }
        else
        {
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


    public void UpdateHealthUI()
    {

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {

            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(health * 100 / maxHealth) + "%";
        playerHealth.text = health.ToString();
    }

    public void TakeDamage(float damage)
    {

        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

}
