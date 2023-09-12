using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const float speed = 3f;                 // Enemy speed (const, flyweight)

    const int maxHealth = 100;              // Enemy health (const, flyweight)
    public int currentHealth;               // Enemy current health

    public const int value = 50;            // Value in money for turret shop (const, flyweight)

    public GameObject deathEffect;          // Death effect for when the enemy is killed

    private Transform target;               // Target meaning the next waypoint in the path
    private int wavepointIndex = 0;         // Index to keep track of position

    public Image healthBar;                 // Healthbar for a prettier UI

    void Start ()
    {
        target = Waypoints.points[0];       // I establish that the enemy will target the waypoints in the path
        currentHealth = maxHealth;          // I set the current health to be that of the max health on start
    }

    void Update () 
    {
        Vector3 dir = target.position - transform.position;                             // calculate where the next waypoint is
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);      // I move the enemy towards it

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

    public void TakeDamage(int amount)              // Allows the enemy to take damage drom each shot
    {
        currentHealth -= amount;                    // I substract the damage from the current health

        //healthBar.fillAmount = currentHealth / 100f;

        if (currentHealth <= 0)                     // I check if its <= to cero
        {
            Die();                                  // If so, I destroy the GO and add the value of this enemy type to the players money
        }
    }

    void Die()
    {
        PlayerStats.Money += value;                 // I give the player the corresponding ammount of gold

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);      // I instantiate the death effect
        Destroy(effect, 5f);                        // I stored the FX as a temporary GO so that I can now easily destroy it 

        Destroy(gameObject);                        // I destroy the GO
    }

    void EndPath()                                  // It activates if the enemy has entered the player's tower
    {
        PlayerStats.Lives--;                        // The player loses one live
        Destroy(gameObject);                        // The enemy is destroyed
    }
}
