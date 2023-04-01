using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset = 0.02f;
    [SerializeField] private ContactFilter2D movementFilter;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Animator animator;

    private bool canMove = true;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                MovePlayer(movementInput);
            }
            else
            {
                // Else if no movement detected, set these animations to false (idle animation will play automatically) 
                animator.SetFloat("Horizontal", 0f);
                animator.SetFloat("Vertical", 0f);
            }

            if (movementInput.x < 0) spriteRenderer.flipX = true;
            else if (movementInput.x > 0) spriteRenderer.flipX = false;
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        int collisionCount = rb.Cast(
                // Checking for potential collisions
                direction, // X,Y values between -1-1 that represent the direction from body to look for collisions
                movementFilter, // Settings that determine where a collision can occur on (ex: layers to collide with)
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset // Amount to cast equal to movement + offset
            );

            if (collisionCount == 0)
            {
                rb.MovePosition(rb.position + movementInput * (moveSpeed * Time.fixedDeltaTime));
            }
            else
            {
                Debug.Log("Number of objects player is colliding with: " + collisionCount);
            }
            
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
    }
    
    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void LockMovement()
    {
        Debug.Log("Movement locked!");
        canMove = false;
    }

    public void UnlockMovement()
    {
        Debug.Log("Movement Unlocked!");
        canMove = true;
    }
}
