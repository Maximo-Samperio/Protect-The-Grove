using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardCannon()
    {
        Debug.Log("Standard cannon selected");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);       // I pass the GO prefab
    }
}
