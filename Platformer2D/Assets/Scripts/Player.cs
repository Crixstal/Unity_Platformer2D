﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private float jumpForce = 6f;
    private bool isJumping;
    public int life = 5;
    Vector3 velocity;
    public int score = 0;
    public Vector3 checkpointPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.LookAt(transform.position + new Vector3(0, 0, horizontalInput));

        rb.velocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            rb.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * jumpForce, ForceMode.Impulse);
    }

    public bool isAlive()
    {
        if (life <= 0)
            return false;

        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.GetContact(0).normal == new Vector3(0f, 1f, 0f))
                ++score;

            else
            {
                --life;
                transform.position = new Vector3(checkpointPos.x, checkpointPos.y, transform.position.z);
            }
        }

        if (collision.gameObject.tag == "KillZone")
        {
            --life;
            transform.position = new Vector3(checkpointPos.x, checkpointPos.y, transform.position.z);
        }

        if (collision.gameObject.tag == "MovingPlatform")
            transform.SetParent(collision.transform);

        if (collision.gameObject.tag == "Point")
        {
            ++score;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Heart")
        {
            ++life;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isJumping = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
            transform.SetParent(null);

        isJumping = true;
    }
}