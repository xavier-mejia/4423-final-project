using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class Snake : Enemy
{
    private Vector2 previousPosition;
    private Transform _playerTransform;
    private Animator _animator;
    private Rigidbody2D _rb;
    private new void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = 0.3f;
        detectionRange = 10f;
        isAlive = true;
        _animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    private new void Update()
    {
        if (isAlive)
        {
            float distanceFromPlayer = Vector2.Distance(transform.position, _playerTransform.position);

            if (distanceFromPlayer <= detectionRange)
            {
                previousPosition = transform.position;
                MoveTowardsPlayer();
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 dir = transform.position;
        Vector2 curDir = (dir - previousPosition).normalized;
        
        _animator.SetFloat("MoveHorz", curDir.x);
        _animator.SetFloat("MoveVert", curDir.y);
        previousPosition = dir;
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            _playerTransform.position, 
            moveSpeed * Time.fixedDeltaTime
            );
    }
}
