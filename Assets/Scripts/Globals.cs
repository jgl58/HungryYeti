﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Globals : MonoBehaviour
{
    public enum gameState {
        menu,
        jugando,
        perdido
    }

    public static bool firstTime = true;

    public static GameObject tituloImagen;
    public static GameObject gameOver;
    public static GameObject yourButton;

    private static GameObject hud;
 
    public static gameState estado = gameState.jugando;
 
    // Start is called before the first frame update
    void Start()
    {
        BloquesFactory.inicio = -10;
        BloquesFactory.generateSuelo(45);
        estado = gameState.menu;
        hud = (GameObject)Resources.Load("Prefabs/GameCanvas");
        Transform[] trs= GameObject.Find("/MenuPrincipal").GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == "TitleImage"){
                tituloImagen = t.gameObject;
            }
            if(t.name == "StartButton"){
                yourButton = t.gameObject;
            }
            if(t.name == "GameOver"){
                gameOver = t.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void die(){
        if(estado == gameState.jugando){
            estado = gameState.perdido;
            yourButton.gameObject.SetActive(true);
            gameOver.gameObject.SetActive(true);
        }
    }

    public static void start(){
        if(estado != gameState.jugando){
            //hud.GetComponent<HUD>().reset();
            tituloImagen.SetActive(false);
            gameOver.gameObject.SetActive(false);
            yourButton.gameObject.SetActive(false);
            GameObject[] canvases = GameObject.FindGameObjectsWithTag("Canvas");
            foreach(GameObject canvas in canvases){
                Destroy(canvas);
            }
            hud = Instantiate((GameObject)Resources.Load("Prefabs/GameCanvas"), new Vector3(0, 0, 0), Quaternion.identity);
            Movement.puntuacionLabel = hud.GetComponent<HUD>().puntuacionLabel;
            if(!firstTime){
                GameObject suelo =  GameObject.Find("Suelo");
                foreach (Transform child in suelo.transform){
                    Destroy(child.gameObject);   
                }
                GeneraSuelo.camino.Clear();
                BloquesFactory.inicio = -10;
                BloquesFactory.generateSuelo(45);
            } else {
                firstTime = false;
            }
            estado = Globals.gameState.jugando;
        }
    }
}
