using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRate = 2f;

    private Animator _animator;
    private float _timeToNextAttack;
    private bool _isAttacking;
    private static readonly int SwordAttack = Animator.StringToHash("swordAttack");

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
        _animator.SetTrigger(SwordAttack);
        _isAttacking = false;
        yield return new WaitForSeconds(0.3f);
    }
    
    public void OnFire()
    {
        _isAttacking = true;
    }
}
