using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float moveSpeed;

    protected Animator EnemyAnim;
    protected SpriteRenderer EnemySpriteRenderer;
    
    protected void Start()
    {
        
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
    
}
