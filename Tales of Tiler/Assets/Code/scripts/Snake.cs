using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : Enemy
{
    private Vector2 _previousPosition;
    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private static readonly int MoveHorz = Animator.StringToHash("MoveHorz");
    private static readonly int MoveVert = Animator.StringToHash("MoveVert");
    
    private new void Start()
    {
        base.Start();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _moveSpeed = 0.3f;
        _detectionRange = 10f;
        _previousPosition = transform.position;
    }

    private void Update()
    {
        if (_isAlive)
        {
            float distanceFromPlayer = Vector2.Distance(transform.position, _playerTransform.position);
            if (distanceFromPlayer <= _detectionRange)
            {
                _previousPosition = transform.position;
                Move();
            }
            
            Vector2 dir = transform.position;
            Vector2 curDir = (dir - _previousPosition).normalized;
            float magnitude = curDir.magnitude;
            if (magnitude > 0)
            {

            }
        }
    }
    
    void FixedUpdate()
    {
        // Vector2 dir = transform.position;
        // Vector2 curDir = (dir - _previousPosition).normalized;
        //
        // _animator.SetFloat(MoveHorz, curDir.x);
        // _animator.SetFloat(MoveVert, curDir.y);
        // _previousPosition = dir;
    }

    protected override void Move()
    {
        if (_canMove)
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            _rb.MovePosition((Vector2)transform.position + direction * (_moveSpeed * Time.fixedDeltaTime));
            _animator.SetFloat(MoveHorz, direction.x);
            _animator.SetFloat(MoveVert, direction.y);
        }
    }
}
