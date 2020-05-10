using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public enum gameState {
        menu,
        jugando,
        perdido
    }

    public static GameObject hud;
 
    public static gameState estado = gameState.jugando;
 
    // Start is called before the first frame update
    void Start()
    {
        estado = gameState.menu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void die(){
        if(estado == gameState.jugando){
            estado = gameState.menu;
            GameObject tituloLabel = null;
            GameObject tituloImagen = null;
            GameObject yourButton = null;
            Transform[] trs= GameObject.Find("/MenuPrincipal").GetComponentsInChildren<Transform>(true);
            foreach(Transform t in trs){
                if(t.name == "TitleImage"){
                    tituloImagen = t.gameObject;
                }
                if(t.name == "Titulo"){
                    tituloLabel = t.gameObject;
                }
                if(t.name == "StartButton"){
                    yourButton = t.gameObject;
                }
            }
            tituloImagen.gameObject.SetActive(true);
            tituloLabel.gameObject.SetActive(true);
            yourButton.gameObject.SetActive(true);
            /*GameObject[] canvases = GameObject.FindGameObjectsWithTag("Canvas");
            foreach(GameObject canvas in canvases){
                Destroy(canvas);
            }
            GameObject suelo =  GameObject.Find("Suelo");
            foreach (Transform child in suelo.transform){
                if(child.name != "Destroyer"){
                    Destroy(child.gameObject);
                }
            }
            BloquesFactory.inicio = -10;
            BloquesFactory.generateSuelo(45);*/
        }
    }
}
