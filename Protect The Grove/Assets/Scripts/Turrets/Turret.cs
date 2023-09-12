using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    private Transform target;
    const float range = 10f;
    const float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Setup fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;                          // Because the turret is made of a base/tower and a weapon
    const float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

  

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);          // As to not overload with too many calls/sec
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);     // Enter all enemies in array
        float shortestDistance = Mathf.Infinity;                                // Find the one thats the closest
        GameObject nearestEnemy = null;                                         // There is none when the game starts

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);     // Check the distance to the enemy
            
            if (distanceToEnemy < shortestDistance)                             // see if the distance is shorter than any other before
            { 
                shortestDistance = distanceToEnemy;                             
                nearestEnemy = enemy;                                           // Switch to this new enemy
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) 
        {
            target = nearestEnemy.transform;                                    // Allows us to switch to the first enemy
        }
        else
        {
            target = null;                                                      // To reset if the enemy leaves range
        }
    }

    void Update()
    {
        if (target == null) { return; }                                       // I tried to use this to save memory but didnt work

        // Target lock on 
        Vector3 dir = target.position - transform.position;                     // I get the direction of the enemy to start working on the rotation of the turret
        Quaternion lookRotation = Quaternion.LookRotation(dir);                 // I pass said direction as a Quaternion variable
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;    // I convert that value to Euler Angles to handle it easier in the Y axis + I use Lerp to smooth it out
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);          // And I rotate the pivot of the turret

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;                                      // This allows me to modify fire rate
        }

        fireCountdown -= Time.deltaTime;                                        // Every second fc is reduced by 1

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);    // Instantiate bullet prefab

        Cannonball bullet = bulletGO.GetComponent<Cannonball>();                // I pass that to the cannonball script

        if (bullet != null)
        {
            bullet.Seek(target);                                                // If there's a bullet, there's a target
        }
    }

    void OnDrawGizmosSelected()                                                 // Done to view the radius and range in editor
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
