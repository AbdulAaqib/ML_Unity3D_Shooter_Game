using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraCoords : MonoBehaviour
{
    //gives the camera coordinates
    public Transform cameraPosition;
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
