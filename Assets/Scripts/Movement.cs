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
    private bool estoyTronco = false;

    private GameObject player;
    private GameObject camera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        refreshCounter = BloquesFactory.BLOQUES_ITERACION;
    }



    // Update is called once per frame
    void Update()
    {
        Celda miCelda = GeneraSuelo.camino.First.Value;

        if (Globals.estado == Globals.gameState.jugando){
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (miCelda.comprobarCaminoArriba(player))
                {
                    Vector3 nextPosition = player.transform.position;
                    nextPosition.z++;

                    Celda siguiente = GeneraSuelo.camino.First.Value;

                    bool estoyEnAgua = siguiente.GetCelda(siguiente.getColumnaPlayer(player)) == BloquesType.Agua;

                    if (estoyEnAgua)
                    {
                        if (Physics.CheckSphere(nextPosition, 0.1f))
                        {
                            print("Hay tronco");
                            estoyTronco = true;
                        }
                        else
                        {
                            print("Espero que sepas nadar");
                            estoyTronco = false;

                        }
                    }

                    player.transform.position = nextPosition;

                    nextPosition = camera.transform.position;
                    nextPosition.z++;
                    camera.transform.position = nextPosition;


                    if (estoyEnAgua && !estoyTronco)
                    {
                        Globals.die();
                    }
                    else
                    {
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
                        if (checkTap())
                        {
                            if (miCelda.comprobarCaminoArriba(player))
                            {
                                Vector3 nextPosition = player.transform.position;
                                nextPosition.z++;

                                Celda siguiente = GeneraSuelo.camino.First.Value;

                                if (siguiente.GetCelda(siguiente.getColumnaPlayer(player)) == BloquesType.Agua)
                                {
                                    if (Physics.CheckSphere(nextPosition, 0.1f))
                                    {
                                        print("Hay tronco");
                                        estoyTronco = true;
                                    }
                                    else
                                    {
                                        print("Espero que sepas nadar");
                                        estoyTronco = false;

                                    }
                                }
                                player.transform.position = nextPosition;

                                nextPosition = camera.transform.position;
                                nextPosition.z++;
                                camera.transform.position = nextPosition;

                                if (siguiente.GetCelda(siguiente.getColumnaPlayer(player)) == BloquesType.Agua && !estoyTronco)
                                {
                                    Globals.die();
                                }
                                else
                                {
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
    }

    bool checkTap(){
        print(beginTouchPosition.x);
        print(endTouchPosition.x);
        if(beginTouchPosition.x + 30 > endTouchPosition.x && beginTouchPosition.x - 30  < endTouchPosition.x){
            return true;
        }
        return false;
    }

   
}
