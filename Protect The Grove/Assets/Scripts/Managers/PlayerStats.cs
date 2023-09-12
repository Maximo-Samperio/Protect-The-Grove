using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    const int startMoney = 100;         // Const int to optimize using flyweight design pattern

    public static int Lives;
    const int startLives = 1;          // Const int to optimize using flyweight design pattern

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}
