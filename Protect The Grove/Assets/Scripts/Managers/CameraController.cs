using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float panSpeed = 30f;                     // Usage of const variables to optimize in a flyweight manner
    const float panBorderThickness = 10f;           // Buffer varr to just make the panning a bit more responisve
    const float scrollSpeed = 5f;                   // Speed for scrollwheel zoom
    const float minY = 10f;                         // Ground for camera to not go through objects
    const float maxY = 50f;                         // Ceiling for camera to not go outside of boundaries

    private bool doMovement = true;

    void Update()
    {
        if (GameManager.GameIsOver)                 // So that the camera does not keep moving on game over
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;               // this just allows me to toggle on-off the movement to edit while playing
        }

        if (!doMovement)
        {
            return;
        }

        // -- Input section -- //
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)   // I check fot the W key or if the mouse is at the top of the screen
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);      // If pressed the cam moves forward at the set pan speed (note that I set the axis to the world instead of the camera's)
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)                   // I check fot the S key or if the mouse is at the top of the screen
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);         // If pressed the cam moves backward at the set pan speed (note that I set the axis to the world instead of the camera's)
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)                   // I check fot the A key or if the mouse is at the left of the screen
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);         // If pressed the cam moves leftward at the set pan speed (note that I set the axis to the world instead of the camera's)
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)    // I check fot the W key or if the mouse is at the top of the screen
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);        // If pressed the cam moves rightward the set pan speed (note that I set the axis to the world instead of the camera's)
        }

        // -- Scrolling input -- //
        float scroll = Input.GetAxis("Mouse ScrollWheel");              // I get the input for the wheel

        Vector3 pos = transform.position;                               // I save the position

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;          // I multiply said scroll by a var

        pos.y = Mathf.Clamp(pos.y, minY, maxY);                         // I set a limit for camera to not go off boundaries

        transform.position = pos;                                       // I move the camera

    }
}
