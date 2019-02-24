using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGJ_enemy_up_down_walk : MonoBehaviour
{

    private Rigidbody2D rb;
    public float SwitchTime;
    public float Speed;
    public bool moveUp = false;
    public float degreesPerSec;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine("SwitchDirection");
    }
    // Update is called once per frame
    void Update()
    {
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        if (moveUp)
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
           //Stop & Rotate Forwards.
        }
        else
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
            //Stop & Rotate Backwards.
        }

    }
    IEnumerator SwitchDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(SwitchTime);
            moveUp = !moveUp;
        }
    }
    IEnumerator SmallRotate()
    {
        while(true)
        {
            yield return new WaitForSeconds(SwitchTime);
            //rotate;
        }
    }
}
