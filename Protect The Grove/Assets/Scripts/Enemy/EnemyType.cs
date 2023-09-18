using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "New Enemy Type", order = 1)]
public class EnemyType : ScriptableObject
{
    public float speed;             // Speed
    public float maxHealth;         // Max health
    public int value;               // Value (money given on kill)

    public GameObject deathEffect;  // Death effect
}
