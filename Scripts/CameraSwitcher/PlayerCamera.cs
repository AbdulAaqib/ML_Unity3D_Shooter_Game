using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
    //allows the player to controller the view
    public float sensX;
    public float sensY;
    public Transform orientation;
    public GameObject isPausedObject;
    float xRotation;
    float yRotation;
    //This code is used to control the rotation of the camera in the game. The rotation is based on the mouse movement.
    //The camera will rotate on the x-axis (mouseY) and y-axis (mouseX). 
    //The xRotation is clamped to a range of -90 to 90 so the camera will be restricted to that range.
    //The transform.rotation and the orientation.rotation are set using the Quaternion.Euler to assign the rotation values.
    //Finally, if the game is paused, the cursor will be visible and unlocked. Else, the cursor will be invisible and locked.
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        if(isPausedObject.GetComponent<MenuController>().isPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Cursor Visible - Player camera");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Cursor Visible - Player camera");
        }
    }

}
