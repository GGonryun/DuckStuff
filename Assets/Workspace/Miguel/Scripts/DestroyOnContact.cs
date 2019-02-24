using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public string[] enemies;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(string enemy in enemies)
        {
            if (collision.gameObject.CompareTag(enemy))
            {
                switch (enemy)
                {
                    case "Duckling":
                        collision.gameObject.GetComponent<Duckling>().OnDeath();
                        break;
                    case "Player":
                        collision.gameObject.GetComponent<PlayerController>().OnDeath();
                        break;

                }
            }
        }
        
    }
}
