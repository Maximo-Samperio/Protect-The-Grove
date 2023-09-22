using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public Transform bulletSpawnPoint; // The point where bullets will be spawned
    public GameObject bulletPrefab;    // The bullet prefab
    public Animator animator;

    public float fireRate = 0.5f;      // The rate of fire
    public float reloadTime = 1.5f;    // The time it takes to reload the weapon
    public int magazineSize = 1;       // mag size, since its a crossbow its one
    private bool canShoot = true;
    private int currentAmmo;

    private void Start()
    {
        currentAmmo = magazineSize;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        animator.SetBool("Fire", true);
        if (currentAmmo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);    // Bullet instantiation

            Arrow arrowScript = bullet.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                arrowScript.SetOwner(gameObject); // Set the owner of the bullet (for collision detection).
            }

            currentAmmo--;                  // Decrease ammo


            if (currentAmmo <= 0)           // Start the reload coroutine if we're out of ammo

            {
                animator.SetBool("NoAmmo", true);       // Animation
                StartCoroutine(Reload());               // Begin reload
            }

            StartCoroutine(ShotCooldown());             // Fire rate cooldown
            animator.SetBool("Fire", false);            // Animation change
        }
    }

    private IEnumerator Reload()
    {



        canShoot = false;
        // Play your reload animation here if needed

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        canShoot = true;
    }

    private IEnumerator ShotCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f / fireRate);
        canShoot = true;
    }
}
