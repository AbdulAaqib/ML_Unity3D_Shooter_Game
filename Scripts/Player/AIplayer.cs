using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIplayer : MonoBehaviour
{
    //sends the player`s position to the AI cube
    public Transform playerPosition;
    private void Update()
    {
        transform.position = playerPosition.position;
    }
}
