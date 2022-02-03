using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public enum PlayerState
{
    walk,
    run,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private Vector2 currentMoveDirection;

    private Vector2 attackDirection;
    private Vector2 lastAttackDirection;

    public VectorValue startingPosition;

    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    public bool isRunning;
    public bool isMoving;
    public bool isAttacking;

    void Start()
    {
        currentState = PlayerState.walk;
    }

    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }

        if (currentState == PlayerState.walk)
        {
            ProcessInputs();
            Animate();
            Move();
            Running();
            Walking();
            Attacking();
        }

        if (Input.GetButtonDown("SwordSlash") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Is Attacking", true);
        currentState = PlayerState.attack;
        yield return null;                      // This delays by a single frame.
        animator.SetBool("Is Attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        // Remembers the last direction to keep character facing the same way after stopping.
        if ((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }

        // Remembers the last direction to keep character facing the same way after stopping.
        if ((moveX != 0 || moveY != 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            currentMoveDirection = moveDirection;
        }

        // Normalises the vector to direction only. 
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    // Sets the parameters in the Player's Animator.
    void Animate()
    {
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetFloat("Last Horizontal", lastMoveDirection.x);
        animator.SetFloat("Last Vertical", lastMoveDirection.y);
        animator.SetBool("Is Running", isRunning);
        animator.SetBool("Is Moving", isMoving);
        animator.SetBool("Is Attacking", isAttacking);
    }

    
    // Hold Shift to double speed. Mostly for development reasons. Gotta go fast.
    void Running()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            animator.SetBool("Is Running", true);
            moveSpeed = 10f;
            Debug.Log("Running.");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            animator.SetBool("Is Running", false);
            moveSpeed = 5f;
        }
    }

    void Walking()
    {

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            isMoving = true;
        }
        else if ((Input.GetKeyUp(KeyCode.A) == false || Input.GetKeyUp(KeyCode.W) == false || Input.GetKeyUp(KeyCode.S) == false && Input.GetKeyUp(KeyCode.D) == false))
        {
            if ((Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.W) == false) && (Input.GetKey(KeyCode.S) == false) && (Input.GetKey(KeyCode.D) == false))            //{
            {
                isMoving = false;
            }
        }
    }

    void Attacking()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isAttacking = true;
            animator.SetBool("Is Attacking", true);
            Debug.Log("Attacking.");
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            isAttacking = false;
            animator.SetBool("Is Attacking", false);
        }
    }

    public void GainItem()
    {
        if (currentState != PlayerState.interact)
        {
            // Freeze player movement upon interaction
            currentState = PlayerState.interact;

            receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
        }
        else
        {
            currentState = PlayerState.idle;
            receivedItemSprite.sprite = null;
        }
    }
}


