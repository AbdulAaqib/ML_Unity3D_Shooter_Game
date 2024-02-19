using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class MenuController : MonoBehaviour
{
    PlayerInput playerInput;
    public string mainMenuScene;
    public GameObject pauseMenu;
    public GameObject deadMenu;
    public bool isPaused;
    public bool isDead = false;
    public bool useOldInputSystem;
    float currentTime;
    public int newplayerhealth;
    // This code is used to manage menus in a game. It contains two methods, OldInputSystem and NewInputSystem, for handling input from the player. 
    //It also contains methods for resuming the game, returning to the main menu, and displaying a pause or dead menu. The isPaused and isDead booleans 
    //are used to keep track of the state of the game. The useOldInputSystem boolean is used to determine which input system should be used. 
    //The currentTime variable is used to track the amount of time that has elapsed. The newplayerhealth variable is used to store the health of the player.
    //this code controls when the player input needs to considered and not considered.
    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    void Update()
    {
        useOldInputSystem = true;
        if(useOldInputSystem) OldInputSystem();
        else NewInputSystem();
    }
    void OldInputSystem()
    {
        currentTime += Time.deltaTime;
        if(isDead == true)
        {
            deadMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Debug.Log("Old Input System: Escape");
                ResumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    void NewInputSystem()
    {
        currentTime += Time.deltaTime;
        bool isEscapeKeyHeld = playerInput.OnFoot.Escape.ReadValue<float>() > 1f;
        if(isDead == true)
        {
            deadMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if(isEscapeKeyHeld)
        {
            if(isPaused)
            {
                Debug.Log("New Input System: Escape");
                ResumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    //turns off pause menu
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    //returns to main menu
    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
