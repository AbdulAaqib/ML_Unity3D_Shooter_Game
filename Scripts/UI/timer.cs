using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class timer : MonoBehaviour
{
    //Maximum time to complete level (in seconds)
    //Maximum time to complete level (in seconds)
    //This declares a float variable called MaxTime which stores the max time in seconds allowed to complete the level
    public float MaxTime;

    //SerializeField allows this variable to be accessible in the Inspector
    [SerializeField] public float CountDown = 0;

    //Declares a TextMeshProUGUI variable called updateTimer which will store the TextMeshProUGUI component
    public TextMeshProUGUI updateTimer;

    //Declares a TextMeshProUGUI variable called updateTimerText which will store the TextMeshProUGUI component
    public TextMeshProUGUI updateTimerText;

    //This is the start function which will run once when the script starts
    void Start () 
    {
        //Sets the CountDown variable to the MaxTime variable
        CountDown = MaxTime;
    }
    void Update () 
    {
        // Decrement the countdown timer by the time elapsed since the last frame
        CountDown -= Time.deltaTime;

        // Debug log to check the value of the countdown timer (commented out)
        //Debug.Log("CountDown");
        //Debug.Log(CountDown);

        // Update the timer text to display the remaining time
        updateTimer.SetText("Time Left: " + (Mathf.Round(CountDown)).ToString() + " s");
        //Debug.Log("Time Left: " + (Mathf.Round(CountDown)).ToString() + " seconds");

        // Check if the countdown timer has reached zero
        if(CountDown < 0)
        {
            // Find the "PauseCanvas" GameObject in the scene
            GameObject killGame = GameObject.Find("PauseCanvas");

            // Set the "isDead" flag in the "PauseCanvas" object to indicate the player has lost
            killGame.GetComponent<MenuController>().isDead = true;

            // Find the "PauseCanvas" GameObject again
            GameObject timeReason = GameObject.Find("PauseCanvas");

            // Set the "isDead" flag in the "PauseCanvas" object to indicate the player has lost
            timeReason.GetComponent<MenuController>().isDead = true;

            // Update the timer text to display the reason for the player's loss
            updateTimerText.SetText("Time Death");
        }
    }
}