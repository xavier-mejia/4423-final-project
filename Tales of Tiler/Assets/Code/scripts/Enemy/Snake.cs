using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : Enemy
{
    private Vector2 _previousPosition;
    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Collider2D _collider;
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
        _collider = GetComponent<Collider2D>();
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

        }
    }
    
    protected override void Move()
    {
        if (_canMove)
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            _rb.MovePosition((Vector2)transform.position + direction * (_moveSpeed * Time.fixedDeltaTime));
            _animator.SetFloat(MoveHorz, direction.x);
            _animator.SetFloat(MoveVert, direction.y);

            // Rotate Collider based off sprite position
            if (direction.x < 0)
            {
                _collider.transform.localScale = new Vector2(-.8f, .8f);
            }
            else if (direction.x > 0)
            {
                _collider.transform.localScale = new Vector2(.8f, .8f);
            }
        }
    }
}
