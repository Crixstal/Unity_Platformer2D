﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 10.0f;
    [SerializeField]
    private uint life = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float jumpInput = Input.GetAxis("Jump");

        if (jumpInput > 0)
            rb.AddForce(new Vector3(0, 1, 0) * speed, ForceMode.Impulse);

        transform.Translate(new Vector3(horizontalInput, 0.0f, 0.0f) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            --life;

            if (life <= 0)
                Destroy(gameObject);
        }

        if (collision.gameObject.tag == "MovingPlatform")
            transform.SetParent(collision.transform); 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
            transform.SetParent(null);
    }
}