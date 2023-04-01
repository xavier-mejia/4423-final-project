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
    private Vector2 _movementInput;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private readonly List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    
    private Animator _animator;
    private bool _canMove = true;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Attack = Animator.StringToHash("swordAttack");
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            if (_movementInput != Vector2.zero)
            {
                MovePlayer(_movementInput);
            }
            else
            {
                // Else if no movement detected, set these animations to false (idle animation will play automatically) 
                _animator.SetFloat(Horizontal, 0f);
                _animator.SetFloat(Vertical, 0f);
            }

            if (_movementInput.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_movementInput.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        int collisionCount = _rb.Cast(
                // Checking for potential collisions
                direction, // X,Y values between -1-1 that represent the direction from body to look for collisions
                movementFilter, // Settings that determine where a collision can occur on (ex: layers to collide with)
                _castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset // Amount to cast equal to movement + offset
            );

            if (collisionCount == 0)
            {
                _rb.MovePosition(_rb.position + _movementInput * (moveSpeed * Time.fixedDeltaTime));
            }
            else
            {
                Debug.Log("Number of objects player is colliding with: " + collisionCount);
            }
            
            _animator.SetFloat(Horizontal, direction.x);
            _animator.SetFloat(Vertical, direction.y);
    }
    
    private void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        _animator.SetTrigger(Attack);
    }

    private void LockMovement()
    {
        _canMove = false;
    }

    private void UnlockMovement()
    {
        _canMove = true;
    }
}
