using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;              // I define the lives as a text

    private void Update()
    {
        livesText.text = PlayerStats.Lives.ToString() + " Lives";      // I convert the value to a string
    }

}
