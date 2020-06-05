using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
        speed = speed * UnityEngine.Random.Range(90, 130) / 100;

        GameObject[] puntuaciones = GameObject.FindGameObjectsWithTag("Canvas");
        gameObject.AddComponent<AudioSource> ();
        if(puntuaciones.Length > 0){
            int puntuacion = int.Parse(puntuaciones[0].GetComponent<HUD>().puntuacionLabel.text);
            speed += ((int)Math.Truncate((float)(puntuacion/2000)) * 0.2f);
        }
        
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
        if(other.gameObject.tag == "Player"){
            
            if(Juego.powerUpState == Juego.PowerUpState.escudo)
            { 
                source.PlayOneShot(crushSound);
                Juego.powerUpState = Juego.PowerUpState.ninguno;
                Transform escudo = other.transform.Find("Escudo(Clone)");
                if (escudo != null)
                {
                    Juego.heartUI.SetActive(false);
                    Destroy(escudo.gameObject);
                }
            }
            else
            {
                if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE_COCHE))
                {
                    Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE_COCHE, 100.0);
                }

                if (Juego.estado == Juego.gameState.jugando)
                {
                    source.PlayOneShot(crushSound);
                    Juego.restartTriggers();
                    other.gameObject.GetComponent<Animator>().SetTrigger("Morir");
                    Juego.die();
                }
            }
        }
    }

}
