using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGJ_enemy_up_down: MonoBehaviour { 

    private Rigidbody2D rb;
    public float SwitchTime;
    public float Speed;
    public bool moveUp = false;
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
        if (moveUp)
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);

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
}
