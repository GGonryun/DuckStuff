using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    public GameObject gameOverMenu;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        instance = this;   
    }
    
    public void DisplayGameOverScreen(bool set)
    {
        gameOverMenu.SetActive(set);
    }
    
}
