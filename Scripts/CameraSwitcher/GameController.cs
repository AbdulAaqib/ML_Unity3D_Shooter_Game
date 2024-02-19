using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    //changes the game camera to a different viewpoint
    public GameObject[] gameCameras;
    private int gameCameraIndex = 0;
    void Start()
    {
        Debug.Log("The Game has started");
        FocusOnCamera(gameCameraIndex);
    }
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
                ChangeCamera(0);
        }
    }
    void FocusOnCamera (int index)
    {
        for (int i = 0; i < gameCameras.Length; i++){
            gameCameras [i].SetActive (i == index);
        }
    }
    void ChangeCamera(int direction)
    {
        gameCameraIndex += direction;
        if (gameCameraIndex == 1)
        {
            gameCameraIndex = 0;
        }
        else if (gameCameraIndex == 0)
        {
            gameCameraIndex = 1;
        }
        FocusOnCamera(gameCameraIndex);
    }
}
