using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public static PausedMenu instance;
    public GameObject resumeButton;

    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape button pressed. Game Paused.");
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    //Make sure to check animations to unscaled time to not get effected by the pause
    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading back to main menu.");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Application");
        //EditorApplication.isPlaying = false;//quits the editor
        //EditorApplication.Exit(0);//Quits unity as a whole
        Application.Quit();//quits the build
    }

    public void Restart()
    {
        Debug.Log("Restarting level");
        SceneManager.LoadScene("Main Scene");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;

    }

    public void GameOver()
    {
        Paused();
        resumeButton.SetActive(false);
    }
    
}
