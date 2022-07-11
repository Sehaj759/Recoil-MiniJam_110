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

    float spawnTime = 3.0f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (!player.GameOver)
        {
            float x = Random.Range(left.position.x, right.position.x);
            float y = Random.Range(bottom.position.y, top.position.y);
            Instantiate(enemyPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void Restart()
    {
        StopCoroutine(SpawnEnemy());
        StartCoroutine(SpawnEnemy());
    }
}
