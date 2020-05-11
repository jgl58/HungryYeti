using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDown : MonoBehaviour
{
    public float delta = 1;  // Amount to move left and right from the start point
    public float speed = 3;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = transform.position;
        v.z -= (Time.deltaTime * speed);
        transform.position = v;
    }
}
