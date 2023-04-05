using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class Snake : Enemy
{
    private Vector2 _previousPosition;
    private Transform _playerTransform;
    private Animator _animator;
    private static readonly int MoveHorz = Animator.StringToHash("MoveHorz");
    private static readonly int MoveVert = Animator.StringToHash("MoveVert");

    private new void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = 0.3f;
        detectionRange = 10f;
        isAlive = true;
        _animator = GetComponent<Animator>();
        _previousPosition = transform.position;
    }

    private new void Update()
    {
        if (isAlive)
        {
            float distanceFromPlayer = Vector2.Distance(transform.position, _playerTransform.position);

            if (distanceFromPlayer <= detectionRange)
            {
                _previousPosition = transform.position;
                Move();
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 dir = transform.position;
        Vector2 curDir = (dir - _previousPosition).normalized;
        
        _animator.SetFloat(MoveHorz, curDir.x);
        _animator.SetFloat(MoveVert, curDir.y);
        _previousPosition = dir;
    }

    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            _playerTransform.position, 
            moveSpeed * Time.fixedDeltaTime
        );
    }
}
