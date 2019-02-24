using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraDown : MonoBehaviour
{
    public bool lowerScreen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!lowerScreen)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                lowerScreen = true;
                StartCoroutine(DropScreen());
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                lowerScreen = false;
                StartCoroutine(RaiseScreen());
            }
        }
    }
    public IEnumerator RaiseScreen()
    {
        while (Camera.main.transform.position.y < 10.21)
        {
            yield return new WaitForSeconds(0.01f);
            Vector3 cachedPos = Camera.main.transform.position;
            Vector3 goalPos = new Vector3(Camera.main.transform.position.x, 11.0f, Camera.main.transform.position.z);
            Camera.main.transform.position = Vector3.Lerp(cachedPos, goalPos, 0.1f);
        }
    }
    public IEnumerator DropScreen()
    {
        while (Camera.main.transform.position.y > -14.5f)
        {
            yield return new WaitForSeconds(0.01f);
            Vector3 cachedPos = Camera.main.transform.position;
            Vector3 goalPos = new Vector3(Camera.main.transform.position.x, -15.0f, Camera.main.transform.position.z);
            Camera.main.transform.position = Vector3.Lerp(cachedPos, goalPos, 0.1f);
        }
    }
}
