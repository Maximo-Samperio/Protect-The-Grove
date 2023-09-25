using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner.bossActive = false;
    }

}
