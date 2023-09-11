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
    public GameObject turretToBuild;

    public GameObject GetTurretToBuild ()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

}
