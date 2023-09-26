using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //--Start of singleton section--//

    // Usage of singleton to reference the build manager instance only once for all nodes
    public static BuildManager instance;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build manager in scene");     // Just a troubleshooting in case this fails
            return;
        }

        instance = this;                        // I reference the singleton of the build manager instance
    }

    //--End of singeleton section--//

    //--Start of turret instantiation section--//

    public GameObject standardTurretPrefab;

    public GameObject buildEffect;

    public TurretBlueprint turretToBuild;

    private Stack<GameObject> placedTurrets = new Stack<GameObject>();      // I define a stack to track placed turrets
    

    public bool CanBuild { get { return turretToBuild != null; } }                      // I check what turret to build
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }    // I check if the player has money


    public void BuildTurretOn(Node node)                    // Method to create turrets
    {
        if (PlayerStats.Money < turretToBuild.cost)         // I check if the player has enough money
        {
            Debug.Log("Not enough gold!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;            // I substract the cost

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);    // I instantiate the turret
        node.turret = turret;

        placedTurrets.Push(turret);         // I push the new turret on top of the stack

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);     // I play the build FX casted into a game object
        Destroy(effect, 5f);    // I destroy that temporary GO

        Debug.Log("Turret built. Money left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;         // I import the selected turret fropm the BP script
    }

    public void UndoTurretPlacement()
    {
        if (placedTurrets.Count > 0)
        {
            GameObject removedTurret = placedTurrets.Pop();
            PlayerStats.Money += turretToBuild.cost;
            Destroy(removedTurret);
        }
    }


}
