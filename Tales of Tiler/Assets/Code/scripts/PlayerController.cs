using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset;
    private ContactFilter2D _movementFilter;
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    private Vector2 _movementInput;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private bool _canMove = true;
    private Animator _animator;


    private static readonly int Attack = Animator.StringToHash("swordAttack");
    private static readonly int MoveHorz = Animator.StringToHash("MoveHorz");
    private static readonly int MoveVert = Animator.StringToHash("MoveVert");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Updates animator with the current movement input.
    private void Update()
    {
        float movementMagnitude = _movementInput.magnitude;

        if (movementMagnitude > 0)
        {
            _animator.SetFloat(MoveHorz, _movementInput.x);
            _animator.SetFloat(MoveVert, _movementInput.y);
        }
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
                // if no movement detected, tell animator player is not moving (idle animation will play automatically) 
                _animator.SetBool(IsMoving, false);
            }
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        int collisionCount = _rb.Cast(
            direction,
            _movementFilter,
            _castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

        if (collisionCount == 0)
        {
            _animator.SetBool(IsMoving, true); 
            _rb.MovePosition((Vector2)transform.position + _movementInput * (moveSpeed * Time.fixedDeltaTime));
        }
    }
    
    private void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
        
        // Restricting movement to 4 directions (up, down, left, right)
        _movementInput.x = Mathf.Round(_movementInput.x);
        _movementInput.y = Mathf.Round(_movementInput.y);
        if (Mathf.Abs(_movementInput.x) > Mathf.Abs(_movementInput.y))
        {
            _movementInput.y = 0; 
        }
        
        else
        { 
            _movementInput.x = 0;
        }
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
