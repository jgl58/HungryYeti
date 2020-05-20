using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour
{
    public enum rotationState
    {
        up,
        left,
        right
    }

    private rotationState rotationDirection = rotationState.up;

    public LeanTweenType saltoEasing;
    public static Text puntuacionLabel;
    // Start is called before the first frame update
    public int refreshCounter;
    private bool go = false;

    private Touch touch;
    private Vector2 beginTouchPosition, endTouchPosition;

    private GameObject player;
    private GameObject mainCamera;

    private bool estoyEnAgua = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        refreshCounter = BloquesFactory.BLOQUES_ITERACION;
    }



    // Update is called once per frame
    void Update()
    {
        Celda miCelda = Juego.camino.First.Value;

        if (Juego.estado == Juego.gameState.jugando){
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!go && player.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("idle") && miCelda.comprobarCaminoArriba(player))
                {
                    Vector3 nextPosition = player.transform.position;
                    nextPosition.z++;

                    //player.transform.position = nextPosition;
                    //StartCoroutine(desplazarCorrutina(nextPosition,0,player));
                    goUp(nextPosition);
                    //StartCoroutine(desplazarCorrutina(nextPosition,0,camera));
                   
                }
   
            }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
                if(!go && player.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("idle")){
                    goSide(miCelda, true);
                }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){
                if(!go && player.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("idle")){
                    goSide(miCelda, false);
                }
        }

        if(Input.touchCount > 0){
            touch = Input.GetTouch(0);
            
            switch(touch.phase){
                case TouchPhase.Began:
                    beginTouchPosition = touch.position;
                    break;

                case TouchPhase.Stationary:
                    if (!go && player.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("idle") && miCelda.comprobarCaminoArriba(player))
                    {
                        Vector3 nextPosition = player.transform.position;
                        nextPosition.z++;

                        goUp(nextPosition);

                    }
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    if (checkTap())
                    {
                        if (!go && player.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("idle") && miCelda.comprobarCaminoArriba(player))
                        {
                            Vector3 nextPosition = player.transform.position;
                            nextPosition.z++;

                            goUp(nextPosition);

                        }
                    }else if(beginTouchPosition.x < endTouchPosition.x && player.transform.position.x < 3){
                        if(!go && player.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("idle")){
                            goSide(miCelda, true);
                        }
                    }else if(beginTouchPosition.x > endTouchPosition.x && player.transform.position.x > -3){
                        if(!go && player.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("idle")){
                            goSide(miCelda, false);
                        }
                    }

                break;    
            }
        }
        }
    }

    bool checkTap(){
        print(beginTouchPosition.x);
        print(endTouchPosition.x);
        if(beginTouchPosition.x + 50 > endTouchPosition.x && beginTouchPosition.x - 50  < endTouchPosition.x){
            return true;
        }
        return false;
    }

    /*
    private bool isMoving = false;
    IEnumerator desplazarCorrutina(Vector3 destino, int direccion, GameObject obj){
        //Delante = 0
        //Izquierda = 1
        //Derecha = 2
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
    }*/

    /*
        nextPosition: la siguiente posicion
    */
    private void goUp(Vector3 nextPosition){
        go = true;
        player.gameObject.GetComponent<Animator>().ResetTrigger("SaltarAdelante");
        player.gameObject.GetComponent<Animator>().ResetTrigger("Saltar");
        player.gameObject.GetComponent<Animator>().SetTrigger("SaltarAdelante");
        player.gameObject.LeanMove(nextPosition,0.15f).setEase(saltoEasing).setOnComplete(()=>{
            player.gameObject.transform.position = nextPosition;
            go = false;
            Celda actual = Juego.camino.First.Value;

            estoyEnAgua = actual.GetCelda(actual.getColumnaPlayer(player)) == BloquesType.Agua;

            if (estoyEnAgua)
            {
                Collider[] colliders = Physics.OverlapSphere(nextPosition, 0.2f);

                foreach (Collider c in colliders)
                {
                    if (c.gameObject.tag != "Player")
                    {
                        print("Hay tronco");
                        Juego.estoyTronco = true;
                        break;
                    }
                    else
                    {
                        print("Espero que sepas nadar");
                        Juego.estoyTronco = false;
                    }
                }
            }

            if (estoyEnAgua && !Juego.estoyTronco)
            {
                Juego.die();
            }
            else
            {
                actual.recolocarPlayer(player);
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
        });
        if(rotationDirection != rotationState.up){
            player.gameObject.LeanRotate(new Vector3(0, 0, 0), 0.15f).setOnComplete(()=>{
                rotationDirection = rotationState.up;
            });
        }
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.z++;
        mainCamera.gameObject.LeanMove(cameraPosition,0.15f);
    }
    /*
        miCelda: celda en la que estoy
        derecha: bool si vamos a la derecha
    */
    private void goSide(Celda miCelda, bool derecha){
        go = true;
        //Time.timeScale = 0.1f;
        Vector3 posicionNueva = derecha ? miCelda.moverDerecha(player) : miCelda.moverIzquierda(player);
        player.gameObject.GetComponent<Animator>().ResetTrigger("SaltarAdelante");
        player.gameObject.GetComponent<Animator>().ResetTrigger("Saltar");
        player.gameObject.GetComponent<Animator>().SetTrigger("Saltar");
        player.gameObject.LeanMove(posicionNueva,0.25f).setEase(saltoEasing).setOnComplete(()=>{
            player.gameObject.transform.position = posicionNueva;
            go = false;
        });
        if((derecha && rotationDirection != rotationState.right) || (!derecha && rotationDirection != rotationState.left)){
            player.gameObject.LeanRotate(derecha ? new Vector3(0, 90, 0) : new Vector3(0, 270, 0) , 0.25f).setOnComplete(()=>{
                rotationDirection = derecha ? rotationState.right : rotationState.left;
            });
        }
    }


    

   
}
