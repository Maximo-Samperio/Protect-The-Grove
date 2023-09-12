using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    const float timeBetweenWaves = 10f;
    private float countdown = 2f;
    private int waveIndex = 0;
    private const float secondsBetweenEnemies = 0.5f;

    public const float enemyInterval = 3.5f;

    void Update()
    {
        if (countdown <= 0f)                    // Checks to see if countdown reached 0
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;            // Decreases time constantly
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(secondsBetweenEnemies);
        }

    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
    }
}
