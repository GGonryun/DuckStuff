using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGameAchievements : MonoBehaviour
{
    public Text results;

    private void Start()
    {
        results.text = EndGameResultsData.instance.CreateResults();
    }
    public void LoadMenu()
    {
        Debug.Log("Loading back to main menu.");
        SceneManager.LoadScene("Main Menu");
    }
}
