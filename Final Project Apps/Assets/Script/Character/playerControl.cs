using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControl : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    private Rigidbody2D rb;

    //player combat data
    public GameObject getPlayerCombat;
    private playerCombat playerCombat;

    //walk
    Vector2 move;
    public float walkSpeed = 500f;
    float walk = 0f;

    //jump
    public bool jump = false;

    //crouch
    Vector2 onCrouch;
    public bool crouch = false;

    //dash
    public float dashPower = 200f;
    bool dash = false;
    public float dashRate = 1f;
    float nextDashTime = 0f;

    public bool gameFinish = false;
    public bool gameStart = false;

    public string charName;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //mangambil data dari player combat
        playerCombat = getPlayerCombat.GetComponent<playerCombat>();
        charName = playerCombat.charName;
    }

    void Update()
    {
        //flip
        if(this.gameObject.transform.localScale.x == -1)
        {
            animator.SetBool("flip", true);
        }

        //walk
        if(playerCombat.lightAttack || playerCombat.heavyAttack || playerCombat.cover || gameFinish || Time.timeScale == 0f || !gameStart)
        {
            walk = 0;
        }
        else
        {
            walk = move.x * walkSpeed;
            animator.SetFloat("walk", walk);
        }

        //crouch
        if (onCrouch.y == -1)
        {
            if (!animator.GetBool("jump"))
            {
                crouch = true;
            }
        } else if(onCrouch.y == 0)
        {
            crouch = false;
        }

        //dash delay
        if (Time.time >= nextDashTime)
        {
            dash = false;
        }

    }

    void FixedUpdate()
    {
        controller.Move(walk * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }


    //walk
    private void OnWalk(InputValue value)
    {
            move = value.Get<Vector2>();
    }

    //jump
    private void OnJump()
    {
        if(!gameFinish && Time.timeScale != 0f && gameStart)
        {
            if (!crouch)
            {
                jump = true;
                animator.SetBool("jump", true);
            }
         }
    }

    public void OnLanding()
    {
        animator.SetBool("jump", false);
    }

    //crouch
    private void OnCrouch(InputValue value)
    {
        if(!gameFinish && Time.timeScale != 0f && gameStart)
        {
            onCrouch = value.Get<Vector2>();
        }
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("crouch", isCrouching);
    }

    //dash
    //right
    private void OnDashR()
    {
        if (!gameFinish && Time.timeScale != 0f && gameStart)
        {
            if(dash == false)
            {
                rb.AddForce(new Vector2(dashPower, 0f));
                animator.SetTrigger("dash");
                nextDashTime = Time.time + 1f / dashRate;
                dash = true;
                if(charName == "bode")
                {
                    budiDash();
                }
            }
        }
    }

    //left
    private void OnDashL()
    {
        if (!gameFinish && Time.timeScale != 0f && gameStart)
        {
            if(dash == false)
            {
                rb.AddForce(new Vector2(dashPower * -1, 0f));
                animator.SetTrigger("dash");
                nextDashTime = Time.time + 1f / dashRate;
                dash = true;
                if (charName == "bode")
                {
                    budiDash();
                }
            }
        }
    }

    void budiDash()
    {

        FindObjectOfType<audioManager>().play("budiDash");
    }
}
