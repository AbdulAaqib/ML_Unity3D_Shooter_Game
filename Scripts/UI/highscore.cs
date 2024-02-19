using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class highscore : MonoBehaviour
{
    //This code opens and closes the highscore screen.
    public string firstLevel;
    //Declares a public string variable called firstLevel.
    public GameObject highscoreScreen;
    //Declares a public GameObject variable called highscoreScreen.
    public void OpenHighScore()
    {
        //Declares a public void method called OpenHighScore.
        highscoreScreen.SetActive(true);
        //Sets the highscoreScreen GameObject to active.
        SceneManager.LoadScene("highscoreTable");
        //Loads the scene "highscoreTable".
    }
    public void CloseHighScore()
    {
        //Declares a public void method called CloseHighScore. 
        highscoreScreen.SetActive(false);
        //Sets the highscoreScreen GameObject to inactive. 
        SceneManager.LoadScene("Main Menu"); 
        //Loads the scene "Main Menu". 
    }
}
