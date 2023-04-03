using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private int _swordDamage = 10;
    private Collider2D _swordCollider;
    
    void Start()
    {
        _swordCollider = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
            col.GetComponent<Enemy>().TakeDamage(_swordDamage);
        }
    }
}
