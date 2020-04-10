using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 v = startPos;
        print(transform.position.x);
        if (transform.position.x >= 6.0f)
        {
            print("Hola");
            v.x = -6.0f;
        }
        else
        {
            v.x += delta * (Time.time * speed);
        }
        v.z = transform.parent.position.z;
        transform.position = v;
    }
}
