using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : MonoBehaviour
{
    [Header("Attributes")]
    private Transform target;
    const float range = 30f;
    const float fireRate = 0.5f;
    private float fireCountdown = 0f;

    [Header("Setup fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    const float turnSpeed = 10f;

    public GameObject missilePrefab; // Updated variable name
    public Transform firePoint;

    // New property for the cost
    public int Cost;

    private TargettingMode targettingMode = TargettingMode.HighHealth;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject targetGO = null;
        switch (targettingMode)
        {
            case TargettingMode.Close:
                targetGO = GetClosestTarget(enemies);
                break;
            case TargettingMode.LowHealth:
                targetGO = GetLowestHealthTarget(enemies);
                break;
            case TargettingMode.HighHealth:
                targetGO = GetHighestHealthTarget(enemies);
                break;
        }
        if (targetGO == null)
        {
            target = null;
            return;
        }
        target = targetGO.transform;
    }

    GameObject GetLowestHealthTarget(GameObject[] enemies)
    {
        if (enemies.Length <= 0) return null;

        GameObject lowestHealthEnemy = null;
        float lowestHealth = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();

            if (enemyMovement != null && enemyMovement.currentHealth < lowestHealth)
            {
                lowestHealth = enemyMovement.currentHealth;
                lowestHealthEnemy = enemy;
            }
        }

        if (IsEnemyValidTarget(lowestHealthEnemy))
        {
            return lowestHealthEnemy;
        }

        return null;
    }

    GameObject GetHighestHealthTarget(GameObject[] enemies)
    {
        if (enemies.Length <= 0) return null;

        GameObject highestHealthEnemy = null;
        float highestHealth = float.MinValue;

        foreach (GameObject enemy in enemies)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();

            if (enemyMovement != null && enemyMovement.currentHealth > highestHealth)
            {
                highestHealth = enemyMovement.currentHealth;
                highestHealthEnemy = enemy;
            }
        }

        if (IsEnemyValidTarget(highestHealthEnemy))
        {
            return highestHealthEnemy;
        }

        return null;
    }

    GameObject GetClosestTarget(GameObject[] enemies)
    {
        if (enemies.Length <= 0) return null;

        GameObject nearestEnemy = null;
        float shortestDistance = range + 1;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (IsEnemyValidTarget(nearestEnemy))
        {
            return nearestEnemy;
        }

        return null;
    }

    bool IsEnemyValidTarget(GameObject enemy)
    {
        if (enemy == null)
        {
            return false;
        }

        return Vector3.Distance(transform.position, enemy.transform.position) <= range;
    }

    void Update()
    {
        if (target == null) { return; }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Missile");

        GameObject missileGO = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);

        Missile missile = missileGO.GetComponent<Missile>();

        if (missile != null)
        {
            missile.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

public enum TurretTargettingMode
{
    Close = 0,
    LowHealth = 1,
    HighHealth = 2
}

