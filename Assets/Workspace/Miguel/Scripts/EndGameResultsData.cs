using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameResultsData : MonoBehaviour
{
    public static EndGameResultsData instance;
    public int deadDucklings = 0;
    public int savedDucklings = 0;
    public int totalHats = 0;
    public int addict = 0;
    public int enemiesKilled = 0;
    public bool accomplishment;
    public void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    public string CreateResults()
    {
        string result = "";
        if(totalHats > 4)
        {
            result += $"Fancy ";
        }
        if(enemiesKilled > 7)
        {
            result += $"Vicious ";
        }
        else if(enemiesKilled < 1) 
        {
            result += $"Pacifist ";
        }
        if(addict > 1)
        {
            result += $"Caffine-Addicted ";
        }
        if(deadDucklings > savedDucklings)
        {
            result += $"Bad Mother";
        }
        else
        {
            result += $"Good Mother";
        }
        return $"You were a {result}!";
    }

}
