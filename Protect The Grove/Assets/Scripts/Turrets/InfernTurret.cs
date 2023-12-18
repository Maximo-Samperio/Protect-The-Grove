using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernTurret : MonoBehaviour
{
    [Header("Attributes")]
    private Transform target, previoustarget;
    const float range = 10f;
    const float fireRate = 1f;
    private float fireCountdown = 0f;
    private float damage = 0;
    [SerializeField]
    private float damageIncrease = 0.5f;
    private EnemyMovement enemy;

    [Header("Setup fields")]
    public GameObject laser;
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    const float turnSpeed = 10f;

    // New property for the cost
    public int Cost;

    private TargettingMode targettingMode = TargettingMode.Close;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null) { return; }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        damage += damageIncrease * Time.deltaTime;
        Damage();
    }

    void LockOnTarget()
    {
        // Activa el objeto laser
        laser.SetActive(true);

        // Actualiza la posición del laser
        laser.transform.position = partToRotate.position;
        laser.transform.LookAt(target);

        // Puedes ajustar el escalado, color, o cualquier otra propiedad aquí
    }


    void Damage()
    {

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

        }
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
        if(targetGO == null)
        {
            target = null;
            laser.SetActive(false);
            return;
        }
        target = targetGO.transform;
        if (target!= previoustarget)
        {
            damage = 0;
            if (target != null)
            {
                enemy = target.GetComponent<EnemyMovement>();
            }
            previoustarget = target;
        }
    }

    GameObject GetLowestHealthTarget(GameObject[] enemies)
    {
        if(enemies.Length <= 0) return null;

        var tree = GetEnemyTree(enemies);
        NodoABB node = tree.raiz;
        var previousNode = node;

        while (node != null)
        {
            node = node.hijoIzq.raiz;
            if(node != null)
            {
                previousNode = node;
            }
        }

        return previousNode.go;
    }

    GameObject GetHighestHealthTarget(GameObject[] enemies)
    {
        if (enemies.Length <= 0) return null;

        var tree = GetEnemyTree(enemies);
        NodoABB node = tree.raiz;
        var previousNode = node;

        while (node != null)
        {
            node = node.hijoDer.raiz;
            if (node != null)
            {
                previousNode = node;
            }
        }

        return previousNode.go;
    }

    ABB GetEnemyTree(GameObject[] enemies)
    {
        var tree = new ABB();
        foreach (var enemyGO in enemies)
        {
            var enemy = enemyGO.GetComponent<EnemyMovement>();
            tree.AgregarElem((int)enemy.currentHealth, enemyGO);
        }
        return tree;
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


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
