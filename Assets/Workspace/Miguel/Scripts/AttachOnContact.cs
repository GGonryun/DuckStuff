using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachOnContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Duckling"))
        {
            
            Duckling duckling = collision.gameObject.GetComponent<Duckling>();
            if (duckling.hasHat == false)
            {
                duckling.hasHat = true;
                EndGameResultsData.instance.totalHats++;
                transform.parent = duckling.hat.transform;
                this.transform.localPosition = Vector2.zero;
                this.transform.localRotation = Quaternion.identity;
                Debug.Log("Touched Duckling");
            }
        }
    }
}
