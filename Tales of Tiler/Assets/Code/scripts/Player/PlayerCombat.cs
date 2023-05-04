using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRate = 2f;

    private PlayerController _playerController;
    private PlayerUIController _playerUIController;
    private Animator _animator;
    private float _timeToNextAttack = 0f;
    private bool _isAttacking;
    private static readonly int SwordAttack = Animator.StringToHash("swordAttack");
    public GameObject fireball;
    public Transform firepointUp, firepointDown, firepointRight, firepointLeft;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerUIController = GetComponent<PlayerUIController>();
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
        if (!_playerController.isPaused)
        {
            _isAttacking = true; 
        }
    }

    public void OnMagic()
    {
        _isAttacking = true;
        ShootFireball();
    }

    private void ShootFireball()
    {
        _isAttacking = false;
    }
    public void TakeDamage(int damage)
    {
        _playerUIController.TakeDamage(damage);
    }

    public void Heal(int healAmount)
    {
        _playerUIController.Heal(healAmount);
    }
}
