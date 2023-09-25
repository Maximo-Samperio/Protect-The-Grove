using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFacade : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextScene()
    {
        EnemySpawner.bossActive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
