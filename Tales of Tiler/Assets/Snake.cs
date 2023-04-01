using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class Snake : Enemy
{
    void Start()
    {
        health = 3;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            health--;
            Debug.Log("Snake has been hit!");
        }

        if (health <= 0)
        {
            Object.Destroy(this.GameObject());
            Debug.Log("Snake has been slain!");
        }
    }
}
