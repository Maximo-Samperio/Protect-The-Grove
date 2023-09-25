using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyType enemyType;

    private float currentHealth;            // Enemy current health

    private Transform target;               // Target meaning the next waypoint in the path
    private int wavepointIndex = 0;         // Index to keep track of position

    public Image healthBar;                 // Healthbar for a prettier UI
    Wave _wave = new Wave();

    void Start ()
    {
        target = Waypoints.points[0];               // I establish that the enemy will target the waypoints in the path
        currentHealth = enemyType.maxHealth;        // I set the current health to be that of the max health on start
    }

    void Update () 
    {
        Vector3 dir = target.position - transform.position;                             // calculate where the next waypoint is
        transform.Translate(dir.normalized * enemyType.speed * Time.deltaTime, Space.World);      // I move the enemy towards it

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)              // I add a small buffer so that it doesnt glitch
        {
            GetNextWaypoint();                                                          // I find the next waypoint in the array
        }
    }

    void GetNextWaypoint()                                          // Does exactly that
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)          // I check if the enemy has reached the end
        {
            EndPath();                                              // I substract a live and destroy GO
            return;
        }
            
        wavepointIndex++;                                           // I advance to the next index in the array
        target = Waypoints.points[wavepointIndex];                  // I set that as the new target
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
        PlayerStats.Money += enemyType.value;                 // I give the player the corresponding ammount of gold

        GameObject effect = (GameObject)Instantiate(enemyType.deathEffect, transform.position, Quaternion.identity);      // I instantiate the death effect
        Destroy(effect, 5f);                        // I stored the FX as a temporary GO so that I can now easily destroy it 

        EnemySpawner.EnemiesAlive--;                // I substract one from the list of enemies alive
        Wave _wave = new Wave();
        //FindObjectOfType<AudioManager>().Play("CrabDeath");

        Destroy(gameObject);                        // I destroy the GO
        //_wave.Dequeue();                            // I remove the enemy from the Queue

    }

    void EndPath()                                  // It activates if the enemy has entered the player's tower
    {
        PlayerStats.Lives--;                        // The player loses one live
        EnemySpawner.EnemiesAlive--;                // I substract one from the list of enemies alive
        Destroy(gameObject);                        // The enemy is destroyed
        //_wave.Dequeue();

    }
}
