using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGJ_enemy_left_right : MonoBehaviour {
    private Rigidbody2D rb;
    public float SwitchTime;
    public float Speed; 
    public bool moveLeft = false;
    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }
	void Start()
    {
        StartCoroutine("SwitchDirection");
    }
	// Update is called once per frame
	void Update () {
        if (moveLeft)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

        }
        transform.Translate(Vector2.left * Speed * Time.deltaTime);

    }
    IEnumerator SwitchDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(SwitchTime);
            moveLeft = !moveLeft;
        }
    }
}
