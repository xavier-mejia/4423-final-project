using UnityEngine;

public class Snake : Enemy
{
    private Vector2 _previousPosition;
    private Transform _playerTransform;
    private static readonly int MoveHorz = Animator.StringToHash("MoveHorz");
    private static readonly int MoveVert = Animator.StringToHash("MoveVert");
    
    private new void Start()
    {
        base.Start();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (_canMove)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                _playerTransform.position, 
                _moveSpeed * Time.fixedDeltaTime
            );
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.collider.gameObject.name);
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Landscape"))
        {
            Debug.Log("Snake cannot Move!");
            _canMove = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Landscape")) {
            _canMove = true;
        }
    }
}
