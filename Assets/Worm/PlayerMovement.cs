using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;
    public Transform currentGun;
    public CharacterController2D controller;
    public Animator animator;
    SpriteRenderer ren;

    public float runSpeed = 40f;
    public float misileForce = 5;

    float horizontalMove = 0f;
    bool jump = false;
    bool movingDone = false;
    bool canMove = true;

    public float targetTime = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

}
