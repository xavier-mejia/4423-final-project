using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _maxHealth = 5;
    [SerializeField] protected float _moveSpeed = 0.3f;
    [SerializeField] protected float _detectionRange = 10f;
    [SerializeField] protected int _attackDamage = 1;
    [SerializeField] protected float _attackRate = 0.5f;
    private float _timeToNextAttack = 0f;
    protected bool _canMove = true;
    
    protected int _currentHealth;
    protected bool _isAlive;
    protected Animator _animator;
    
    protected void Start()
    {
        _currentHealth = _maxHealth;
        _isAlive = true;
        _animator = GetComponent<Animator>();
    }
    
    protected virtual void Move()
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    protected void Die()
    {
        _isAlive = false;
        Destroy(gameObject);
    }
    
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (Time.time >= _timeToNextAttack)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Player hit!");
                col.GetComponent<PlayerCombat>().TakeDamage(_attackDamage);
                _timeToNextAttack =  Time.time + 1f / _attackRate;
            }
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Landscape"))
        {
            Debug.Log("Snake cannot Move!");
            // _canMove = false;
        }
    }
}
