using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoffee : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Duckling"))
        {
            Debug.Log("Collected Coffee");
            EndGameResultsData.instance.addict++;
            gameObject.SetActive(false);
        }
    }
}
