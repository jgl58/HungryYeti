﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        y += Time.deltaTime * 50;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (gameObject.tag == "Escudo" && 
                Juego.powerUpState == Juego.PowerUpState.ninguno)
            {
                print("Tengo escudo");
                gameObject.transform.parent = other.gameObject.transform;

                Vector3 powerupPosition = gameObject.transform.localPosition;
                powerupPosition.x = 0f;
                powerupPosition.y = 1.5f;
                powerupPosition.z = -1f;

                gameObject.transform.localPosition = powerupPosition;
                Juego.powerUpState = Juego.PowerUpState.escudo;
            }

            if (gameObject.tag == "double" &&
                Juego.doublePoints == false)
            {
                print("Dobles puntos");
                gameObject.SetActive(false);
                Juego.doublePointsUI.SetActive(true);
                //animacion dobles puntos

                Juego.doublePoints = true;

                //lanzamos el reset despues de 10 segundos

                Invoke("resetDoublePoints", 10.0f);
            }


        }
    }

    private void resetDoublePoints()
    {
        Juego.doublePoints = false;
        Juego.doublePointsUI.SetActive(false);
    }

   
}
