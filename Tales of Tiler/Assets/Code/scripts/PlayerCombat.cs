using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private LayerMask enemyLayerMask;
    
    private Animator _animator;
    private Transform _attackPoint;
    private float _timeToNextAttack;
    private bool _isAttacking;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _attackPoint = new GameObject("Attack Point").transform;
        _attackPoint.SetParent(transform);
        _timeToNextAttack = Time.time;
    }

    private void Update()
    {
        if (Time.time >= _timeToNextAttack)
        {
            if (_isAttacking)
            {
                StartCoroutine(PerformAttack());
                _timeToNextAttack = Time.time + 1f / attackRate;
            }
        }
    }

    IEnumerator PerformAttack()
    {
        _animator.SetTrigger("swordAttack");
        _isAttacking = false;
        yield return new WaitForSeconds(0.3f); // wait for the attack animation to start

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            _attackPoint.position,
            attackRange,
            enemyLayerMask
            );
        foreach (Collider2D enemy in enemies)
        {
            // Apply damage to the enemy here. This assumes the enemy has a script called "EnemyHealth".
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDisable()
    {
        if (_attackPoint != null)
        {
            Destroy(_attackPoint.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint != null)
        {
            Gizmos.DrawWireSphere(_attackPoint.position, attackRange);
        }
    }

    public void OnFire()
    {
        _isAttacking = true;
    }
}
