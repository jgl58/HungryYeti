using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour
{

    public LeanTweenType saltoEasing;
    public static Text puntuacionLabel;
    // Start is called before the first frame update
    public int refreshCounter;
    private bool goUp = false;
    private bool goRight = false;
    private bool goLeft = false;

    private Touch touch;
    private Vector2 beginTouchPosition, endTouchPosition;

    private GameObject player;
    private GameObject camera;

    private bool isMoving = false;

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
                    if(!goUp){
                        goUp = true;
                        Vector3 nextPosition = player.transform.position;
                        nextPosition.z++;

                        Celda siguiente = GeneraSuelo.camino.First.Value;

                        bool estoyEnAgua = siguiente.GetCelda(siguiente.getColumnaPlayer(player)) == BloquesType.Agua;

                        if (estoyEnAgua)
                        {
                            if (Physics.CheckSphere(nextPosition, 0.3f))
                            {
                                print("Hay tronco");
                                Globals.estoyTronco = true;
                            }
                            else
                            {
                                print("Espero que sepas nadar");
                                Globals.estoyTronco = false;

                            }
                        }

                    //player.transform.position = nextPosition;
                    //StartCoroutine(desplazarCorrutina(nextPosition,0,player));

                    
                        player.gameObject.GetComponent<Animator>().ResetTrigger("Saltar");
                        player.gameObject.GetComponent<Animator>().SetTrigger("Saltar");
                        player.gameObject.LeanMove(nextPosition,0.25f).setEase(saltoEasing).setOnComplete(()=>{
                            player.gameObject.transform.position = nextPosition;
                            nextPosition = camera.transform.position;
                            nextPosition.z++;
                            camera.transform.position = nextPosition;
                            goUp = false;
                        });
                    
                        //StartCoroutine(desplazarCorrutina(nextPosition,0,camera));

                        if (estoyEnAgua && !Globals.estoyTronco)
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
   
            }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
                //player.transform.position = miCelda.moverDerecha(player);
                if(!goRight){
                    //Time.timeScale = 0.1f;
                    goRight = true;
                    Vector3 posicionNueva = miCelda.moverDerecha(player);
                    player.gameObject.GetComponent<Animator>().SetTrigger("Saltar");
                    player.gameObject.LeanMove(posicionNueva,0.25f).setEase(saltoEasing).setOnComplete(()=>{
                        player.gameObject.GetComponent<Animator>().ResetTrigger("Saltar");
                        player.gameObject.transform.position = posicionNueva;
                        goRight = false;
                    });
                }
                

                //LeanTween.cancel(player);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){

                //player.transform.position = miCelda.moverIzquierda(player);
                if(!goLeft){
                    goLeft = true;
                    //Time.timeScale = 0.1f;
                    Vector3 posicionNueva = miCelda.moverIzquierda(player);
                    player.gameObject.GetComponent<Animator>().SetTrigger("Saltar");
                    player.gameObject.LeanMove(posicionNueva,0.25f).setEase(saltoEasing).setOnComplete(()=>{
                        player.gameObject.GetComponent<Animator>().ResetTrigger("Saltar");
                        player.gameObject.transform.position = posicionNueva;
                        goLeft = false;
                    });
                }
                
                //player.gameObject.LeanMove(miCelda.moverIzquierda(player),1).setEaseInQuad();
                //StartCoroutine(desplazarCorrutina(miCelda.moverIzquierda(player),1,player));
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
                                if (Physics.CheckSphere(nextPosition, 0.5f))
                                {
                                    print("Hay tronco");
                                    Globals.estoyTronco = true;
                                }
                                else
                                {
                                    print("Espero que sepas nadar");
                                    Globals.estoyTronco = false;
                                }
                            }
                            player.transform.position = nextPosition;
                            //StartCoroutine(desplazarCorrutina(nextPosition,0,player));
                            
                            nextPosition = camera.transform.position;
                            nextPosition.z++;
                            camera.transform.position = nextPosition;
                            //StartCoroutine(desplazarCorrutina(nextPosition,0,camera));

                            if (siguiente.GetCelda(siguiente.getColumnaPlayer(player)) == BloquesType.Agua && !Globals.estoyTronco)
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
                        StartCoroutine(desplazarCorrutina(miCelda.moverDerecha(player),2,player));
                    }else if(beginTouchPosition.x > endTouchPosition.x && player.transform.position.x > -3){
                        //Swipe a la izquierda
                        //player.transform.position = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z);
                        player.transform.position = miCelda.moverIzquierda(player);
                        //StartCoroutine(desplazarCorrutina(miCelda.moverIzquierda(player),1,player));
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

    IEnumerator desplazarCorrutina(Vector3 destino, int direccion, GameObject obj){
        /*
        Delante = 0
        Izquierda = 1
        Derecha = 2
        */
        print("corutina");
        if(!isMoving && obj.transform.position != destino){
            isMoving = true;
            Vector3 origen = obj.gameObject.transform.position;
            float time = 0.0f;
            while(time < 1f){
                obj.transform.position = Vector3.Lerp(origen, destino, time);
                time += Time.deltaTime * 10f;
                yield return null;
            }
            isMoving = false;
        }
    }
    

   
}
