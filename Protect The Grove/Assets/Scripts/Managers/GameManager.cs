using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false;
    public static bool LevelCompleted = false;

    public GameObject LevelCompletedUI;
    public GameObject gameOverUI;

    private AnalyticsManager analyticsManager;

    private void OnEnable()
    {
        GameIsOver = false;
        LevelCompleted = false;
        LevelCompletedUI.SetActive(false);

        // Obtener la instancia de AnalyticsManager
        analyticsManager = FindObjectOfType<AnalyticsManager>();
    }

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
        // Aquí puedes enviar el evento cuando se complete el nivel
        float goldSpentThisLevel = 100f; // Aquí pones la cantidad de oro gastado en este nivel o partida
        analyticsManager.SendGoldSpentEvent(goldSpentThisLevel);

        // Mostrar la interfaz de nivel completado
        LevelCompletedUI.SetActive(true);
    }

    void EndGame()
    {
        // Aquí puedes enviar el evento cuando termine el juego
        float goldSpentThisGame = 50f; // Aquí pones la cantidad de oro gastado en todo el juego
        analyticsManager.SendGoldSpentEvent(goldSpentThisGame);

        // Mostrar la interfaz de game over
        gameOverUI.SetActive(true);
    }
}
