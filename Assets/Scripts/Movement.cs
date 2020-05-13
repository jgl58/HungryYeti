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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Celda miCelda = GeneraSuelo.camino.First.Value;

        if (Globals.estado == Globals.gameState.jugando){
           
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (miCelda.comprobarCaminoArriba(player))
                {

                    Vector3 position = player.transform.position;
                    position.z++;
                    player.transform.position = position;

                    position = camera.transform.position;
                    position.z++;
                    camera.transform.position = position;

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
                        float x = endTouchPosition.x - beginTouchPosition.x;
                        float y = endTouchPosition.y - beginTouchPosition.y;

                        //beginTouchPosition == endTouchPosition
                        //Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0
                        if (checkTap())
                        {
                            if (miCelda.comprobarCaminoArriba(player))
                            {

                                Vector3 position = player.transform.position;
                                position.z++;
                                player.transform.position = position;

                                position = camera.transform.position;
                                position.z++;
                                camera.transform.position = position;

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
                        }else if(beginTouchPosition.x < endTouchPosition.x && player.transform.position.x < 3){
                            //Swipe a la derecha
                            //player.transform.position = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
                            player.transform.position = miCelda.moverDerecha(player);
                        }else if(beginTouchPosition.x > endTouchPosition.x && player.transform.position.x > -3){
                            //Swipe a la izquierda
                            //player.transform.position = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z);
                            player.transform.position = miCelda.moverIzquierda(player);
                        }

                    break;    
                }
            }
        }

        if (Globals.estoyTronco == false && miCelda.GetCelda(miCelda.getColumnaPlayer(player)) == BloquesType.Agua)
        {
            Globals.die();
        }


    }

    bool checkTap(){
        print(beginTouchPosition.x);
        print(endTouchPosition.x);
        if(beginTouchPosition.x + 7 > endTouchPosition.x && beginTouchPosition.x - 7  < endTouchPosition.x){
            return true;
        }
        return false;
    }

   
}
