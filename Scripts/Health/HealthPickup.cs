using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This code checks for a collision between the game object and the player.
//If the collision is with the player, it sets the boolean pickedUphealth to true.
//If pickedUphealth is true, it sets the player's health to 100 using the GetComponent method.
//Finally, it prints a message to the console.
public class HealthPickup : MonoBehaviour
{
    public GameObject player;
    public bool pickedUphealth = false;
    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.name == "Player")
        {
            pickedUphealth = true;
            if (pickedUphealth == true)
            {
                player.GetComponent<PlayerHealth>().health = 100;
                Debug.Log("Picked Up Health: newhealth = 100");
            }
        } 
    }
}
