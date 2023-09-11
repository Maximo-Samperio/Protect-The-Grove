using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;                    // The color I want the tile to turn on hover
    private Renderer rend;                      // Optimized to just find the renderer once and not every time the mouse hovers above it
    private Color startColor;                   // The original color of the tile
    public Vector3 positionOffset;

    private GameObject turret;

    private void Start()
    {
        rend = GetComponent<Renderer>();        // This allows me to just store the renderer in a private var and thats it
        startColor = rend.material.color;
    }

    private void OnMouseDown()                  // This builds a turret on click
    {
        if (turret != null)
        {
            Debug.Log("Can't build here!");     // Checks if there is not already a turret in that tile
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();        // I pass the GO from the build manager script
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);    // I instantiate the turret
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;       // Set transition to hover color on hover
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;       // Return to the normal color on exit
    }
}
