using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Animator animator;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero) 
        {
            bool moveOccured = TryMove(movementInput);
            if (!moveOccured)
            {
                moveOccured = TryMove(new Vector2(movementInput.x, 0));
                moveOccured = TryMove(new Vector2(0, movementInput.y));
            }

            if (movementInput.x != 0) animator.SetBool("isMovingHorz", moveOccured);
            else if (movementInput.x != 0 && movementInput.y > 0) animator.SetBool("isMovingHorz", moveOccured);
            else if (movementInput.y > 0) animator.SetBool("isMovingForward", moveOccured);
            else if (movementInput.y < 0) animator.SetBool("isMovingBack", moveOccured);
        }
        else
        {
            animator.SetBool("isMovingHorz", false);
            animator.SetBool("isMovingForward", false);
            animator.SetBool("isMovingBack", false);
        }

        if (movementInput.x < 0) spriteRenderer.flipX = true;
        else if (movementInput.x > 0) spriteRenderer.flipX = false;
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                // Checking for potential collisions
                direction, // X,Y values between -1-1 that represent the direction from body to look for collisions
                movementFilter, // Settings that determine where a collision can occur on (ex: layers to collide with)
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset // Amount to cast equal to movement + offset
            );

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * (moveSpeed * Time.fixedDeltaTime));
                return true;
            }
            else
                return false;
    }
    
    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
