using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 5;
    [SerializeField] protected float moveSpeed = 0.3f;
    [SerializeField] protected float detectionRange = 10f;
    
    protected int currentHealth;
    protected bool isAlive;

    protected void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }
    
    protected void Update()
    {
        
    }

    protected virtual void Move()
    {
        
    }
    
    protected virtual void Attack()
    {
        
    }

    public void TakeDamage(int attackDamage)
    {
        currentHealth -= attackDamage;
        if (currentHealth <= 0) Die();
    }

    protected void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }
}
