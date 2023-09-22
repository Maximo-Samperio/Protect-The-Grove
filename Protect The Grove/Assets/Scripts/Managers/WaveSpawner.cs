using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;                 // To keep track of enemies alive

    public Wave[] waves;                                // List with all of the enemies in the wave
    public GameObject enemy;                            // The enemy GO

    const float timeBetweenWaves = 3f;                  // Literally the time between waves
    private float countdown = 2f;                       // A countdown to keep track of time passed
    private int waveIndex = 0;                          // To keep track of the enemies
    private const float secondsBetweenEnemies = 0.5f;

    public const float enemyInterval = 3.5f;

    void Update()
    {
        if (EnemiesAlive > 0)                   // Checks if there are any enemies alive 
        {
            return;                             // If there are, return
        }   
        if (countdown <= 0f)                    // Checks to see if countdown reached 0 and if the previous wave is dead
        {
            StartCoroutine(SpawnWave());        // New wave is spawned
            countdown = timeBetweenWaves;       // countdown is reset
        }

        countdown -= Time.deltaTime;            // Decreases time constantly
    }

    IEnumerator SpawnWave()                     // coroutine to spawn waves
    {
        Wave wave = waves[waveIndex];           // import wave

        for (int i = 0; i < wave.count; i++)    // Checking the wave count
        {
            SpawnEnemy(wave.enemy);                             // I spawn the enemy specified in the wave.enemy
            yield return new WaitForSeconds(1 / wave.rate);     // At the specified rate
        }

        waveIndex++;
        PlayerStats.Rounds++;

        if (waveIndex == waves.Length)
        {
            Debug.Log("Level 1 completed!");
            this.enabled = false;
        }
    }

    void SpawnEnemy (GameObject enemy)
    {
        GameObject newEnemy = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
        //Wave.Enqueue(newEnemy);

        EnemiesAlive++;
    }
}
