using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System.IO;

public class EnemyMovement : MonoBehaviour
{
    public EnemyType enemyType;
    private EnemySpawner spawner;
    public Dijkstra dijkstra;           // Reference to the Dijkstra script
    private List<Waypoints> path;       // The calculated path for the enemy
    private int currentWaypointIndex = 0;

    public EnemySpawner Spawner;
    public Waypoints startWaypoint; 
    public Waypoints endWaypoint;   


    private int wavepointIndex = 0;
    [HideInInspector]
    public float currentHealth;            // Enemy current health

    private Transform target;               // Target meaning the next waypoint in the path

    public Image healthBar;                 // Healthbar for a prettier UI
    Wave _wave = new Wave();

    public WaypointGraph waypointManager;
    private Waypoints currentWaypoint;

    string startWaypointName = "StartWaypoint";
    string endWaypointName = "EndWaypoint";



    void Start ()
    {
        spawner = GameObject.FindObjectOfType<EnemySpawner>();
        currentHealth = enemyType.maxHealth;        // I set the current health to be that of the max health on start
        dijkstra = FindObjectOfType<Dijkstra>();

        GameObject startWaypointGO = GameObject.Find(startWaypointName);
        GameObject endWaypointGO = GameObject.Find(endWaypointName);

        if (startWaypointGO != null && endWaypointGO != null)
        {
            SetWaypoints(startWaypointGO.GetComponent<Waypoints>(), endWaypointGO.GetComponent<Waypoints>());
        }
        else
        {
            Debug.LogError("Couldn't find one or both waypoints with the specified names.");
        }



        //startWaypoint = Spawner.startWaypoint;
        //endWaypoint = Spawner.endWaypoint;


        // Calculate the path using Dijkstra
        //path = dijkstra.CalculateShortestPath(startWaypoint, endWaypoint);

    }


    void Update () 
    {
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (path == null || path.Count == 0)
            return;

        Waypoints targetWaypoint = path[currentWaypointIndex];

        // Move towards the current target waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, enemyType.speed * Time.deltaTime);

        // Check if the enemy has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.transform.position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex++;

            // Check if there are more waypoints
            if (currentWaypointIndex < path.Count)
            {
                targetWaypoint = path[currentWaypointIndex];
            }
            else
            {
                // The enemy has reached the end of the path
                EndPath();
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

    public void SetWaypoints(Waypoints start, Waypoints end)
    {
        startWaypoint = start;
        endWaypoint = end;

        // Recalculate the path using Dijkstra when waypoints are set
        path = dijkstra.CalculateShortestPath(start, end);
        currentWaypointIndex = 0; // Reset waypoint index
    }

}
