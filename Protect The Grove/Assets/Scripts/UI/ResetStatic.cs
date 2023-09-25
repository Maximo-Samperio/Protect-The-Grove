using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatic : MonoBehaviour
{
    public GameObject LevelCompletedUI;
    public GameObject gameOverUI;

    private void Awake()
    {
        LevelCompletedUI.SetActive(false);
        gameOverUI.SetActive(false);
    }
    private void Start()
    {
        LevelCompletedUI.SetActive(false);
        gameOverUI.SetActive(false);
    }
    public static void ResetStaticVariable ()
    {
        EnemySpawner.bossActive = false;
    }

}
