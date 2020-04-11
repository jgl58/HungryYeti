﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 1.0f;
    private Vector3 startPos;
    bool goRight = true;
    void Start()
    {
        startPos = transform.position;
        if (startPos.x == 7)
        {
            goRight = false;
        }
        speed = Random.Range(1.0f, 2.0f);
    }

    void Update()
    {
        Vector3 v = transform.position;
        if (goRight)
        {
            if (transform.position.x <= -6.0f)
            {
                v.x = 6.0f;

            }
            else
            {
                v.x -= (Time.deltaTime * speed);
            }
            v.z = transform.parent.position.z;
            transform.position = v;
        }
        else
        {
            if (transform.position.x >= 6.0f)
            {
                v.x = -6.0f;

            }
            else
            {
                v.x += (Time.deltaTime * speed);
            }
            v.z = transform.parent.position.z;
            transform.position = v;
        }
        
        
    }
}