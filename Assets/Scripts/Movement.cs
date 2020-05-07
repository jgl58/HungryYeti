using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour
{
    public Text puntuacionLabel;
    // Start is called before the first frame update
    private int refreshCounter = 5;

    private Touch touch;
    private Vector2 beginTouchPosition, endTouchPosition;

    void Start()
    {
        
    }

    int getColumnaPlayer(GameObject player)
    {
        //Columnas [Columna1,Columna2,Columna3,Columna4]
        float posX = player.transform.position.x;
        int columna = 0;

        if (posX >= -4f && posX < -2f)//Columna 1
        {
            columna = 0;
        }
        else if (posX >= -2f && posX < 0f)//Columna 2
        {
            columna = 1;
        }
        else if (posX >= 0f && posX < 2f)//Columna 3
        {
            columna = 2;
        }
        else//Columna 4
        {
            columna = 3;
        }
        return columna;
    }

    bool comprobarCaminoLado(bool lado)
    {
        //lado = true -> izquierda ||lado = false -> derecha
        //Columnas [Columna1,Columna2,Columna3,Columna4]
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int columna = getColumnaPlayer(player);
        Celda celdaSiguiente = GeneraSuelo.camino.First.Value;

        if (!lado)//miramos derecha
        {
            if (getColumnaPlayer(player) == 3) { return false; }
            return (celdaSiguiente.GetCelda(columna + 1) == BloquesType.Nieve);
        }
        else //miramos izquierda
        {
            if (getColumnaPlayer(player) == 0) { return false; }
            return (celdaSiguiente.GetCelda(columna - 1) == BloquesType.Nieve);
        }
        
    }


    bool comprobarCaminoArriba(GameObject player)
    {
        int columna = getColumnaPlayer(player);
        Celda celdaSiguiente = GeneraSuelo.camino.First.Next.Value;

        if (celdaSiguiente.GetCelda(columna) == BloquesType.Nieve)
        {
            GeneraSuelo.camino.RemoveFirst();
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Globals.estado == Globals.gameState.jugando){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            puntuacionLabel = GameObject.FindGameObjectWithTag("PointsLabel").GetComponent<Text>();
            if (comprobarCaminoArriba(player))
        {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector3 position = this.transform.position;
                position.z--;
                this.transform.position = position;
                refreshCounter--;
                if (refreshCounter == 0)
                {
                    refreshCounter = 5;
                    print("Cargamos nuevos bloques");


                    BloquesFactory.generateSuelo(5);
                }
                int puntuacion = Convert.ToInt32(puntuacionLabel.text) + 1;
                puntuacionLabel.text = string.Format("{0:0000}", puntuacion);
            }
   
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (comprobarCaminoLado(false))
            {
                player.transform.position = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
                
            }
           
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){

            if (comprobarCaminoLado(true))
            {
                player.transform.position = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z);
                
            }
        }

            if(Input.touchCount > 0){
                touch = Input.GetTouch(0);
                
                switch(touch.phase){
                    case TouchPhase.Began:
                        beginTouchPosition = touch.position;
                        break;
                    case TouchPhase.Ended:
                        endTouchPosition = touch.position;
                        if(beginTouchPosition == endTouchPosition){
                            Vector3 position = this.transform.position;
                            position.z--;
                            this.transform.position = position;
                            refreshCounter --;
                            if (refreshCounter == 0)
                            {
                                refreshCounter = 5;
                                print("Cargamos nuevos bloques");


                                BloquesFactory.generateSuelo(5);
                            }
                            int puntuacion = Convert.ToInt32(puntuacionLabel.text) + 1; 
                            puntuacionLabel.text = string.Format ("{0:0000}", puntuacion);
                        }else if(beginTouchPosition.x < endTouchPosition.x && player.transform.position.x < 3){
                            //Swipe a la derecha
                            player.transform.position = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
                        }else if(beginTouchPosition.x > endTouchPosition.x && player.transform.position.x > -3){
                            //Swipe a la izquierda
                            player.transform.position = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z);
                        }

                    break;    
                }
            }
        }
        
    }

   
}
