using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Transform target;


    public void Seek (Transform _target)
    {
        target = _target;                                           // I pass the value from the turret script
    }

    void Update()
    {
        if (target != null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;         // Direction the bullet needs to look at

    }
}
