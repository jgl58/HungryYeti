using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour
{
    public static Text puntuacionLabel;
    // Start is called before the first frame update
    public int refreshCounter;

    private Touch touch;
    private Vector2 beginTouchPosition, endTouchPosition;

    void Start()
    {
        refreshCounter = BloquesFactory.BLOQUES_ITERACION;
        //puntuacionLabel = GameObject.FindGameObjectWithTag("PointsLabel").GetComponent<Text>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Globals.estado == Globals.gameState.jugando){
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            Celda miCelda = GeneraSuelo.camino.First.Value;
            
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (miCelda.comprobarCaminoArriba(player))
                    {

                        Vector3 position = this.transform.position;
                        position.z--;
                        this.transform.position = position;

                        refreshCounter--;
                        if (refreshCounter == 0)
                        {
                            refreshCounter = BloquesFactory.BLOQUES_ITERACION;
                            print("Cargamos nuevos bloques");


                            BloquesFactory.generateSuelo(BloquesFactory.BLOQUES_ITERACION);
                        }
                        int puntuacion = Convert.ToInt32(puntuacionLabel.text) + 1;
                        puntuacionLabel.text = string.Format("{0:0000}", puntuacion);
                    }
   
                }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
                player.transform.position = miCelda.moverDerecha(player);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){

                player.transform.position = miCelda.moverIzquierda(player);
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
                                refreshCounter = BloquesFactory.BLOQUES_ITERACION;
                                print("Cargamos nuevos bloques");


                                BloquesFactory.generateSuelo(BloquesFactory.BLOQUES_ITERACION);
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
