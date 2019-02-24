using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    void Start(){}

    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Mouse click read, loading scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Scene");//Loads scene called SceneSwitch
        }
        else if (Input.GetKey(KeyCode.Mouse0) != false)
        {
            Debug.Log("Something is wrong with mouse click.");
        }
        //DontDestroyOnLoad(template);
      




    }
}