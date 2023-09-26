using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;                 // To keep track of enemies alive

    public Wave[] waves;                                // List with all of the enemies in the wave
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();         // Queue to store enemies

    Wave _wave = new Wave();                            // I reference the wave script

    public GameObject enemy;                            // The enemy GO

    public static bool bossActive = false;

    const float timeBetweenWaves = 3f;                  // Literally the time between waves
    private float countdown = 2f;                       // A countdown to keep track of time passed
    public int waveIndex = 0;                          // To keep track of the enemies
    private const float secondsBetweenEnemies = 0.5f;

    public const float enemyInterval = 3.5f;


    private void OnEnable()
    {
        bossActive = false;
    }
    private void Start()
    {
        bossActive = false;
    }

    void Update()
    {
        if (EnemiesAlive > 0)                   // Checks if there are any enemies alive 
        {
            return;                             // If there are, return
        }
        if (countdown <= 0f)                    // Checks to see if countdown reached 0 and if the previous wave is dead
        {
            if (waveIndex > waves.Length)
            {
                GameManager.LevelCompleted = true;
                bossActive = false;
                this.enabled = false;
            }

            StartCoroutine(SpawnWave());        // New wave is spawned
            countdown = timeBetweenWaves;       // countdown is reset
        }

        countdown -= Time.deltaTime;            // Decreases time constantly

        if (waveIndex > waves.Length)
        {
            GameManager.LevelCompleted = true;
            this.enabled = false;
        }
    }

    IEnumerator SpawnWave()                     // coroutine to spawn waves
    {
        if (waveIndex > waves.Length)
        {
            GameManager.LevelCompleted = true;
            this.enabled = false;
        }
        Wave wave = waves[waveIndex];           // import wave

        for (int i = 0; i < wave.count; i++)    // Checking the wave count
        {
            SpawnEnemy(wave.enemy);                             // I spawn the enemy specified in the wave.enemy
            yield return new WaitForSeconds(1 / wave.rate);     // At the specified rate
        }

        waveIndex++;
        PlayerStats.Rounds++;

        if (waveIndex > waves.Length)
        {
            GameManager.LevelCompleted = true;
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject newEnemy = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
        AddEnemyToQueue(enemy);

        //_wave.Enqueue(enemy);                   // I put the enemy inside the Queue
        if (waveIndex == 5)
        {
            bossActive = true;
        }

        EnemiesAlive++;
    }

    public void AddEnemyToQueue(GameObject enemy)
    {
        enemyQueue.Enqueue(enemy);
    }

    public void RemoveEnemyFromQueue(GameObject enemy)
    {
        if (enemyQueue.Contains(enemy))
        {
            enemyQueue.Dequeue(); // Remove the enemy from the front of the queue
            Destroy(enemy); // Destroy the enemy GameObject
        }

    }
}
