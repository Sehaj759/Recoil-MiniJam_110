using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] GameObject enemyPrefab;

    // play area bounds
    [Header("Play Area Bounds")]
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    float padding = 5.0f;
    float startingSpawnTime = 3.0f;
    float spawnTime;
    float spawnTimeDecrementDelta = 0.3f;
    float minSpawnTime = 1.0f;

    void Start()
    {
        spawnTime = startingSpawnTime;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(DecreaseSpawnTime());
    }

    IEnumerator SpawnEnemy()
    {
        while (!player.GameOver)
        {
            float x = Random.Range(left.position.x + padding, right.position.x - padding);
            float y = Random.Range(bottom.position.y + padding, top.position.y - padding);
            Instantiate(enemyPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator DecreaseSpawnTime()
    {
        while (spawnTime >= minSpawnTime)
        {
            yield return new WaitForSeconds(5.0f);
            spawnTime -= spawnTimeDecrementDelta;
        }
    }

    public void Restart()
    {
        spawnTime = startingSpawnTime;
        StopCoroutine(SpawnEnemy());
        StartCoroutine(SpawnEnemy());

        StopCoroutine(DecreaseSpawnTime());
        StartCoroutine(DecreaseSpawnTime());
    }
}
