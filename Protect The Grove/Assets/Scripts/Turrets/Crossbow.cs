using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public Transform bulletSpawnPoint; // The point where bullets will be spawned
    public GameObject bulletPrefab;    // The bullet prefab
    public Animator animator;

    public float fireRate = 0f;      // The rate of fire
    public float reloadTime = 2f;    // The time it takes to reload the weapon
    public int magazineSize = 5;       // mag size, since its a crossbow its one
    private bool canShoot = true;
    private int currentAmmo;

    private void Start()
    {
        currentAmmo = magazineSize;
        canShoot = true;
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
        if (currentAmmo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);    // Bullet instantiation
            animator.SetBool("Fire", true);


            Arrow arrowScript = bullet.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                arrowScript.SetOwner(gameObject); // Set the owner of the bullet (for collision detection).
            }

            currentAmmo--;                  // Decrease ammo


            if (currentAmmo <= 0)           // Start the reload coroutine if we're out of ammo
            {
                animator.SetBool("Fire", false);
                StartCoroutine(Reload());               // Begin reload
            }

            //StartCoroutine(ShotCooldown());             // Fire rate cooldown
            //animator.SetBool("Fire", false);            // Animation change
        }
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        //animator.SetBool("NoAmmo", true);       // Animation

        yield return new WaitForSeconds(reloadTime);


        //animator.SetBool("NoAmmo", false);       // Animation
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
