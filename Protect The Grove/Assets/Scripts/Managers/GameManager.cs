using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false;
    public static bool LevelCompleted = false;

    public GameObject LevelCompletedUI;
    public GameObject gameOverUI;


    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
        {
            return;
        }

        if (Input.GetKeyDown("u"))
        {
            EndGame();
        }
        if (Input.GetKeyDown("y"))
        {
            CompleteLevel();
        }

        if (LevelCompleted == true)
        {
            CompleteLevel();
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void CompleteLevel()
    {
        GameIsOver = true;                          // Stops camera from moving
        LevelCompletedUI.SetActive(true);
    }

    void EndGame()
    {
        GameIsOver = true;

        gameOverUI.SetActive(true);
    }


}
