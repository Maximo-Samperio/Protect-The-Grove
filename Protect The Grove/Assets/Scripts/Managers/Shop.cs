using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardCannon()
    {
        Debug.Log("Standard cannon selected");
        buildManager.SelectTurretToBuild(standardTurret);       // I pass the GO prefab
    }
    public void SelectMissileTurret()
    {
        Debug.Log("Standard Missile selected");
        buildManager.SelectTurretToBuild(missileTurret);       // I pass the GO prefab
    }
}
