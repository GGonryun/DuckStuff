using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("Loading Next Scene");
        SceneManager.LoadScene("Main Scene");
        
;    }

    public void QuitGame()
    {
        Debug.Log("Quitting Application");
        //EditorApplication.isPlaying = false;//quits the editor
        //EditorApplication.Exit(0);//Quits unity as a whole
        Application.Quit();//quits the build
    }

    public void CreditScreen()
    {
        Debug.Log("Credits loading...");
        SceneManager.LoadScene("Credits");
    }

    public void GoBack()
    {
        Debug.Log("Going back to Main Menu...");
        SceneManager.LoadScene("Main Menu");
    }
    public void Nothing()
    {
        Debug.Log("Duck should quak");
    }
    
   

}
