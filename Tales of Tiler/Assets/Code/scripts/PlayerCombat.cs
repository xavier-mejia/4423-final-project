using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRate = 2f;

    private PlayerController _playerController;
    private Animator _animator;
    private float _timeToNextAttack = 0f;
    private bool _isAttacking;
    private static readonly int SwordAttack = Animator.StringToHash("swordAttack");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _timeToNextAttack = Time.time;
    }

    private void Update()
    {
        if (Time.time >= _timeToNextAttack)
        {
            if (_isAttacking)
            {
                _isAttacking = false;
                _timeToNextAttack =  Time.time + 1f / attackRate;
                PerformAttack();
            }
        }
    }

    private void PerformAttack()
    {
        _animator.SetTrigger(SwordAttack);
        _isAttacking = false;
    }
    
    public void OnFire()
    {
        _isAttacking = true;
    }
}
