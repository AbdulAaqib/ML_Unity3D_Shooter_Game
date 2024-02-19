using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using TMPro;
public class updateScore : MonoBehaviour
{
    public string[] Url;
    public string player_name;
    public TextMeshProUGUI newscore;
    public float playerPrefsScore;
    public int scoreToDatabase;
    //public string name;
    //This is the Update() method which gets called every frame
    //It is responsible for retrieving the values of "name" and "databaseScore" from the PlayerPrefs
    //Then it sets the text of "newscore" to a string that contains the rounded value of playerPrefsScore
    //Finally, it starts a coroutine called updateScoreSQL()
    public void Update()
        {
            player_name = PlayerPrefs.GetString("name"); 
            //Retrieve the value of "name" from the PlayerPrefs 
            playerPrefsScore = PlayerPrefs.GetFloat("databaseScore"); 
            //Retrieve the value of "databaseScore" from the PlayerPrefs 
            newscore.SetText("Score: " + (Mathf.Round(playerPrefsScore)).ToString()); 
            //Set the text of "newscore" to a string which contains the rounded value of playerPrefsScore
            StartCoroutine(updateScoreSQL()); 
            //Start the coroutine called updateScoreSQL()
        }
    //This function starts a coroutine that will update the score in the SQL database
    //It also prints a debug message to the console
    public void PerformupdateScoreSQL()
    {
        StartCoroutine(updateScoreSQL());
        Debug.Log("PerformupdateScoreSQL");
    }
    //This function is responsible for updating the player's score in the database
    IEnumerator updateScoreSQL()
    {
        //Declare a local variable which holds the score that needs to be sent to the database
        scoreToDatabase = Mathf.RoundToInt(playerPrefsScore);
        //Create a new WWWForm object which allows us to send data to the php script
        WWWForm updateScoreSQLForm = new WWWForm();
        //Add the player's name and score to the WWWForm
        updateScoreSQLForm.AddField("username", player_name);
        updateScoreSQLForm.AddField("score", scoreToDatabase);
        //Use the UnityWebRequest class to send the data to the php script
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity_login/insertscore.php", updateScoreSQLForm);
        //Wait for the php script to respond
        yield return www.SendWebRequest();
        //Check if the web request was successful
        if(www.result != UnityWebRequest.Result.Success)
        {
            //Print an error if the request was not successful
            Debug.Log(www.error);
        }
        else
        {
            //Store the response from the web request
            string results = www.downloadHandler.text;
            //Print the response
            Debug.Log(results);
        }
        //Dispose the web request object
        www.Dispose();
    }
}
