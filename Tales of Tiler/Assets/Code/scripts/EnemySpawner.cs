using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private GameObject colorfulSnakePrefab;
    
    private float snakeInterval;
    private float colorfulSnakeInterval;
    
    // Start is called before the first frame update
    void Start()
    {
        snakeInterval = 20f;
        colorfulSnakeInterval = 60f;
        StartCoroutine(SpawnEnemy(snakePrefab, snakeInterval));
        StartCoroutine(SpawnEnemy(colorfulSnakePrefab, snakeInterval));
    }

    private IEnumerator SpawnEnemy(GameObject enemy, float interval)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(
            Random.Range(-5f, 5),
            Random.Range(-6f, 6f),
            0
        ), Quaternion.identity);

        StartCoroutine(SpawnEnemy(enemy, snakeInterval));
        StartCoroutine(SpawnEnemy(enemy, colorfulSnakeInterval));
    }
}
