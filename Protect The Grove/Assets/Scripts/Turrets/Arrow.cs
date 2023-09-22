using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    private GameObject owner;                   // The object that fired the bullet so that it doesnt collide with itself

    const float damage = 75f;                    // Damage done to enemy
    const float speed = 60f;
    public void SetOwner(GameObject owner)
    {
        this.owner = owner;                     // I set the owner to itself
    }

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;     // I apply velocity to the arrow
    }

    private void Update()
    {
        CheckForCollisions();                   // Collision check
    }

    private void CheckForCollisions()
    {
        Ray ray = new Ray(transform.position, transform.forward);   // Create a ray from the bullet's current position to its forward direction
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, speed * Time.deltaTime))  // I perform the Raycast
        {
            HandleCollision(hit.collider);                          // I manage the collision
        }
    }

    private void HandleCollision(Collider collider)                         // Method for handling collisions
    {
        if (collider.CompareTag("Enemy"))                                   // I check if what has been hit is an enemy
        {
            EnemyMovement e = collider.GetComponent<EnemyMovement>();       // I get the data from the enemy script

            if (e != null)                          // Double check its an enemy
            {   
                e.TakeDamage(damage);               // He takes damage
            }
        }
        
        Destroy(gameObject);                        // I destroy the arrow
    }
}
