using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    public Color hoverColor;                    // The color I want the tile to turn on hover
    public Color notEnoughMoneyColor;           // The color I want the tile to turn on hover
    private Renderer rend;                      // Optimized to just find the renderer once and not every time the mouse hovers above it
    private Color startColor;                   // The original color of the tile
    public Vector3 positionOffset;

    public GameObject turret;

    private void Start()
    {
                // This allows me to just store the renderer in a private var and thats it
        
        if (rend = GetComponent<Renderer>())
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()                  // This builds a turret on click
    {
        if (EventSystem.current.IsPointerOverGameObject())       // I check there is no UI element overlaping
        {
            return;
        }

        if (!buildManager.CanBuild) 
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("Can't build here!");     // Checks if there is not already a turret in that tile
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())       // I check there is no UI element overlaping
        {
            return;
        }

        if (!buildManager.CanBuild)            // So that it does not highlight when no turret is selected
        {
            return;
        }

        if(buildManager.HasMoney) 
        {
            rend.material.color = hoverColor;       // Set transition to hover color on hover
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;       // Return to the normal color on exit
    }
}
