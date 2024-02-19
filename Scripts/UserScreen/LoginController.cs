using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class UserProperty
{
    public string name;
    public string username;
    public string score;
    public int id;
}
public class LoginController : MonoBehaviour
{
    public InputField username;
    public InputField Password;
    public InputField myName;
    public InputField myUsername;
    public InputField myPassword;
    public InputField myComPass;
    public string[] Url;
    public TextMeshProUGUI registerText;
    public TextMeshProUGUI loginText;
    public bool registerError = false;
    public bool registered = false;
    public TextMeshProUGUI changeLoginResult;
    //This code is used to update the text displayed on the screen when a user attempts to register an account. It checks if there is an error in 
    //the registration process and displays an appropriate message. It also checks if the registration was successful and displays a message accordingly.

    void Update()
        {
            //Check if there is an error in the registration process
            if (registerError == true)
            {
                //Set the text to display an appropriate message
                registerText.SetText("Password and confirm password must be the same!");
                //Set registerError to false so that this block of code does not run again until another error occurs
                registerError = false;
            }

            //Check if the registration was successful 
            if (registered == true)
            {
                //Set the text to display a success message 
                registerText.SetText("Account created");

                //Set registered to false so that this block of code does not run again until another registration occurs 
                registered = false;
            }
        }

        //This function is called when a user attempts to log in 
        public void PerformLogin()
        {
            //Start a coroutine which will handle the login process 
            StartCoroutine(login());
        }

        IEnumerator login() 
        {   //Create a new WWWForm object which will contain the data from the login form 
            WWWForm loginForm = new WWWForm();

            //Add data from the login form to the WWWForm object 
            loginForm.AddField("email", username.text);   //Add username from form 
            loginForm.AddField("pass", Password.text);    //Add password from form

            //Create a UnityWebRequest object which will send data from the WWWForm object to a URL specified in Url[0] 
            UnityWebRequest www = UnityWebRequest.Post(Url[0], loginForm);

            //Send data from WWWForm object and wait for response 
            yield return www.SendWebRequest();

            //Check if response has been received  																  		  	  	  	  	  	  	  	  	  	  	  	    
            // If yes, proceed with further processing of response 
            if(www.isDone)  {    

                //Store response in result variable as string type                                                                                                                                                                              This response will be in JSON format, so it can be parsed into an object of type UserProperty later on 
                var result = www.downloadHandler.text;

                Debug.Log(result + "Login Unsuccesful");

                //Check if response contains "0 results" string, which means that no user with such credentials was found in database    
                //If yes, display appropriate message on screen 
                if(result == "0 results"){    

                    Debug.Log("Error");

                    changeLoginResult.SetText("Login Unsuccesful");    

                } else {    

                    //If user with such credentials was found, create an instance of UserProperty class and parse JSON response into it      
                    //This will allow us to access user's properties such as name, score, etc...      
                    //Store these properties in PlayerPrefs so they can be accessed later on by other scripts      
                    //Finally, load Main Menu scene for user 

                    UserProperty user = new UserProperty();    

                    user = JsonUtility.FromJson<UserProperty>(result);    

                    PlayerPrefs.SetString("name", user.name);    

                    PlayerPrefs.SetString("score", user.score);    

                    PlayerPrefs.SetString("username", user.username);    

                    Debug.Log("my name is: " + user.name);    

                    SceneManager.LoadScene("Main Menu");    

                }         }      }
        //This method checks if the password and confirm password are the same. If they are, it calls the Register() coroutine. If not, it displays an error message.
        public void PerformRegister()
        {
            if(myPassword.text == myComPass.text)
            {
                StartCoroutine(Register());
                registerText.SetText("Account Registered");
            }
            else
            {
                registerError = true;
                Debug.LogWarning("Password and confirm password must be the same!");
                registerText.SetText("Password and confirm password must be the same!");
            }
        }
        //This coroutine sends a POST request to the server with the user's email, password, and name in order to register an account. 
        //It then checks for a response from the server and logs any errors that may occur. 
        IEnumerator Register()
        {
            WWWForm REGISTERForm = new WWWForm(); //creates a new WWWForm object to store data for the POST request 
            REGISTERForm.AddField("email", myUsername.text); //adds user's email to form data 
            REGISTERForm.AddField("pass", myPassword.text); //adds user's password to form data 
            REGISTERForm.AddField("name", myName.text); //adds user's name to form data 

            UnityWebRequest www = UnityWebRequest.Post(Url[1], REGISTERForm); //creates a POST request with form data and sends it to server 

            yield return www.SendWebRequest(); //waits for response from server

            if(www.isDone) //if response is received 
            {   var result = www.downloadHandler.text; //stores response from server in result variable 

                Debug.Log(www.downloadHandler.text); //logs response from server

                if(result == "0") //if response is 0 (success) 
                {   Debug.Log(result); //logs success message 

                    registered = true; //sets registered boolean to true 

                } else {   Debug.Log("Error"); //logs error message if response is not 0 (failure)   
                }   } }
}