using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class Snake : MonoBehaviour
{
    private int _health = 3;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _health--;
            Debug.Log("Snake has been hit!");
        }

        if (_health <= 0)
        {
            Object.Destroy(this.GameObject());
            Debug.Log("Snake has been slain!");
        }
    }
}
