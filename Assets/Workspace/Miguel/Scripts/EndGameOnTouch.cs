using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameOnTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("End Game");
        }
        if(collision.gameObject.CompareTag("Duckling"))
        {
            collision.gameObject.GetComponent<Duckling>().SubmitDuckling();
        }
    }
}
