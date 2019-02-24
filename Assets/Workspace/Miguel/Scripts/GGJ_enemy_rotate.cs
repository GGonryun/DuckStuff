using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGJ_enemy_rotate : MonoBehaviour {
    private Rigidbody2D rb;
    private float rotAmount;
    private float curRot;
    public float degreesPerSec = 0.5f;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));

    }
}
