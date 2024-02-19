using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public GameObject optionsScreen;
    public GameObject highscoreButton;
    //Unlocks the cursor and makes it visible
    public void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Loads the first level of the game when called
    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    //Opens the options screen and hides the highscore button when called 
    public void OpenSettings()
    {
        optionsScreen.SetActive(true);
        highscoreButton.SetActive(false);
    }
    
    //Closes the options screen and shows the highscore button when called 
    public void CloseSettings() 
    { 															  
        optionsScreen.SetActive(false); 	  	  	  	  	  	  	  	  	  	  
        highscoreButton.SetActive(true); 	  	  	  	  	  	  	  	  	  } 

    //Exits the game when called 
    public void ExitGame()  {  Application.Quit(); Debug.Log("Quitting"); }
}
