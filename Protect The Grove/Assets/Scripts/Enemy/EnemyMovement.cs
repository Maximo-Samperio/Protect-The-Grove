using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyType enemyType;
    private EnemySpawner spawner;

    private float currentHealth;            // Enemy current health

    private Transform target;               // Target meaning the next waypoint in the path
    private int wavepointIndex = 0;         // Index to keep track of position

    public Image healthBar;                 // Healthbar for a prettier UI
    Wave _wave = new Wave();


    public WaypointGraph waypointGraph;
    public float speed = 5.0f;
    private List<Waypoints> currentPath;
    private int currentWaypointIndex = 0;

    void Start ()
    {
        spawner = GameObject.FindObjectOfType<EnemySpawner>();
        waypointGraph = FindObjectOfType<WaypointGraph>();
        FindAndFollowPath();
        currentHealth = enemyType.maxHealth;        // I set the current health to be that of the max health on start
    }
    void FindAndFollowPath()
    {
        Waypoints startWaypoint = waypointGraph.waypoints[0]; // Starting waypoint
        Waypoints endWaypoint = waypointGraph.waypoints[1];   // End waypoint
        currentPath = Dijkstra.FindShortestPath(startWaypoint, endWaypoint);
    }

    void Update () 
    {
        if (currentPath != null && currentWaypointIndex < currentPath.Count)
        {
            Waypoints targetWaypoint = currentPath[currentWaypointIndex];
            Vector3 targetPosition = targetWaypoint.Position;

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }

    }


    public void TakeDamage(float amount)              // Allows the enemy to take damage drom each shot
    {
        currentHealth -= amount;                    // I substract the damage from the current health

        healthBar.fillAmount = currentHealth / enemyType.maxHealth;     // For no reason this bugs the bullet impact fx

        if (currentHealth <= 0)                     // I check if its <= to cero
        {
            Die();                                  // If so, I destroy the GO and add the value of this enemy type to the players money
        }
    }

    void Die()
    {
        PlayerStats.Money += enemyType.value;       // I give the player the corresponding ammount of gold

        GameObject effect = (GameObject)Instantiate(enemyType.deathEffect, transform.position, Quaternion.identity);      // I instantiate the death effect
        Destroy(effect, 5f);                        // I stored the FX as a temporary GO so that I can now easily destroy it 

        EnemySpawner.EnemiesAlive--;                // I substract one from the list of enemies alive
        Wave _wave = new Wave();
        //FindObjectOfType<AudioManager>().Play("enemyDeath");

        spawner.RemoveEnemyFromQueue(gameObject);


        Destroy(gameObject);                        // I destroy the GO
        //_wave.Dequeue(gameObject);                  // I remove the enemy from the Queue

        if (EnemySpawner.bossActive == true)
        {
            GameManager.LevelCompleted = true;
            EnemySpawner.bossActive = false;
        }

    }

    void EndPath()                                  // It activates if the enemy has entered the player's tower
    {
        if (EnemySpawner.bossActive == true)
        {
            GameManager.LevelCompleted = true;
        }

        PlayerStats.Lives--;                        // The player loses one live
        EnemySpawner.EnemiesAlive--;                // I substract one from the list of enemies alive
        spawner.RemoveEnemyFromQueue(gameObject);
        Destroy(gameObject);                        // The enemy is destroyed

        //_wave.Dequeue(gameObject);

    }
}
