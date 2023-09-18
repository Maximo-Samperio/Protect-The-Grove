using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Transform target;
    const float speed = 20f;                                        // More const int for optimization using flyweight
    const int damage = 25;
    public GameObject impactEffect;


    public void Seek (Transform _target)
    {
        target = _target;                                           // I pass the value from the turret script
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;         // Direction the bullet needs to look at
        float distanceThisFrame = speed * Time.deltaTime;           // Actual distance to move per frame relative to the set speed

        if (dir.magnitude <= distanceThisFrame)                     // To check if it has hit an target
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);    // Normal to make speed constant
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);      // I play the particle effect
        Destroy(effectIns, 2f);

        Damage(target);

        Destroy(gameObject);                                        // I destroy the bullet
    }

    void Damage(Transform enemy)
    {
        EnemyMovement e = enemy.GetComponent<EnemyMovement>();


        if (e != null) 
        {
            e.TakeDamage(damage);
        }
    }

}
