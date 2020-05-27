using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public AudioSource source {get{return GetComponent<AudioSource>();}}
    public AudioClip crushSound;
    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 1.0f;
    private Vector3 startPos;
    bool goRight = true;
    void Start()
    {
        startPos = transform.position;
        if (startPos.x > 0)
        {
            goRight = false;
        }
        speed = speed * Random.Range(90, 130) / 100;

        gameObject.AddComponent<AudioSource> ();

        /*GameObject[] puntuaciones = GameObject.FindGameObjectsWithTag("Canvas");
        if(puntuaciones.Length > 0){
            int puntuacion = int.Parse(puntuaciones[0].GetComponent<HUD>().puntuacionLabel.text);
            speed *= (puntuacion / 100) == 0 ? 1 : (puntuacion / 100) + 0.5f;
        }*/
        
    }

    void Update()
    {
        Vector3 v = transform.position;
        if (!goRight)
        {
            if (transform.position.x <= -10.0f)
            {
                BloquesFactory.createCoche(gameObject.transform.parent.gameObject, (int)gameObject.transform.position.z,true);
                Destroy(gameObject);
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
            if (transform.position.x >= 10.0f)
            {
                BloquesFactory.createCoche(gameObject.transform.parent.gameObject, (int)gameObject.transform.position.z,false);
                Destroy(gameObject);  
            }
            else
            {
                v.x += (Time.deltaTime * speed);
            }
            v.z = transform.parent.position.z;
            transform.position = v;
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && transform.position.z == other.gameObject.transform.position.z){
            Juego.die();
            source.PlayOneShot(crushSound);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Camion" && transform.position.z == other.gameObject.transform.position.z){
            Juego.die();
            source.PlayOneShot(crushSound);
        }
    }
}
