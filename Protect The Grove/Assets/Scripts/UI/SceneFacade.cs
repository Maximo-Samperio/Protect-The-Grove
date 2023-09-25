using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFacade : MonoBehaviour
{
    public GameObject LevelCompletedUI;
    public GameObject gameOverUI;

    private void Start()
    {
        Debug.Log("Resetting static variable");
        ResetStatic.ResetStaticVariable();

    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextScene()
    {
        EnemySpawner.bossActive = false;
        Debug.Log("Resetting static variable");

        LevelCompletedUI.SetActive(false);
        gameOverUI.SetActive(false);

        ResetStatic.ResetStaticVariable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
